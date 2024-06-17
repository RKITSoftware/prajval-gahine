using ExpenseSplittingApplication.BL.Common.Interface;
using ExpenseSplittingApplication.BL.Common.Service;
using ExpenseSplittingApplication.BL.Domain;
using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Common.Helper;
using ExpenseSplittingApplication.DL.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;
using ExpenseSplittingApplication.Models.POCO;
using Microsoft.AspNetCore.Http;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ExpenseSplittingApplication.BL.Master.Service
{
    /// <summary>
    /// Handles expense-related operations including creation, deletion, validation, and settlement.
    /// </summary>
    public class BLEXP01Handler : IEXP01Service
    {
        /// <summary>
        /// Represents an instance of the EXP01 class, which handles expense-related data.
        /// </summary>
        private EXP01 _objExpense;

        /// <summary>
        /// Stores a list of contributions (CNT01 instances) related to expenses.
        /// </summary>
        private List<CNT01> _lstContribution;

        /// <summary>
        /// Factory for creating database connections.
        /// </summary>
        private IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Context interface for interacting with the expense-related database.
        /// </summary>
        private IDBExpenseContext _context;

        /// <summary>
        /// Service for logging messages related to expense operations.
        /// </summary>
        private ILoggerService _loggerService;

        /// <summary>
        /// Gets or sets the current operation to be performed.
        /// </summary>
        public EnmOperation Operation { get; set; }

        /// <summary>
        /// Gets or sets the list of contributions.
        /// </summary>
        public List<CNT01> LstContribution { get => _lstContribution; set => _lstContribution = value; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BLEXP01Handler"/> class.
        /// </summary>
        /// <param name="utility">The utility service.</param>
        /// <param name="dbFactory">The database connection factory.</param>
        /// <param name="context">The expense context.</param>
        /// <param name="loggerService">The logging service.</param>
        public BLEXP01Handler(IDBExpenseContext context, UserLoggerService loggerService)
        {
            _dbFactory = EsaOrmliteConnectionFactory.ConnectionFactory;
            _context = context;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Prepares the expense entity for saving based on the provided DTO.
        /// </summary>
        /// <param name="objDto">The DTO containing expense data.</param>
        public void PreSave(DTOEXC objDto)
        {
            DateTime now = DateTime.Now;

            _objExpense = new EXP01()
            { 
                P01F02 = objDto.ObjDTOEXP01.P01F02, 
                P01F03 = objDto.ObjDTOEXP01.P01F03, 
                P01F04 = objDto.ObjDTOEXP01.P01F04, 
                P01F05 = objDto.ObjDTOEXP01.P01F05 ?? now, 
                P01F98 = DateTime.Now,
            };

            double payeeAmount = Int32.MinValue;
            if (!objDto.IsShareUnequal)
            {
                payeeAmount = objDto.ObjDTOEXP01.P01F04 / objDto.LstDTOCNT01.Count;
            }

            LstContribution = objDto.LstDTOCNT01.Select(dtoContribution => (new CNT01()
            {
                T01F03 = dtoContribution.T01F03,
                T01F04 = objDto.IsShareUnequal ? dtoContribution.T01F04 : payeeAmount,
                T01F05 = objDto.ObjDTOEXP01.P01F02 == dtoContribution.T01F03 ? true : false,
                T01F98 = now,
            })).ToList();
        }

        /// <summary>
        /// Checks if all contribution IDs in the list are unique.
        /// </summary>
        /// <param name="lstContribution">The list of contributions to check.</param>
        /// <returns>True if all IDs are unique, otherwise false.</returns>
        public bool AreAllIdsUnique(List<DTOCNT01> lstContribution)
        {
            HashSet<int> seenIDs = new HashSet<int>();
            foreach (DTOCNT01 obj in lstContribution)
            {
                if (!seenIDs.Add(obj.T01F03))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Performs pre-validation checks before saving expense data.
        /// </summary>
        /// <param name="objDto">The DTO containing expense data.</param>
        /// <returns>A response indicating the result of the pre-validation.</returns>
        public Response PreValidation(DTOEXC objDto)
        {
            Response response = new Response();

            //add
            // check does contribution are of unique users??
            if (!AreAllIdsUnique(objDto.LstDTOCNT01))
            {
                response.IsError = true;
                response.HttpStatusCode = StatusCodes.Status409Conflict;
                response.Message = "Duplicate user detected in contributions.";

                return response;
            }

            // check if payer is present in contribution list??
            if (!objDto.LstDTOCNT01.Any(contribution => contribution.T01F03 == objDto.ObjDTOEXP01.P01F02))
            {
                response.IsError = true;
                response.HttpStatusCode = StatusCodes.Status409Conflict;
                response.Message = "Payer not in contributions.";

                return response;
            }

            // check payer userID exists??
            if (Utility.UserIDExists(objDto.ObjDTOEXP01.P01F02))
            {
                response.IsError = true;
                response.HttpStatusCode = StatusCodes.Status404NotFound;
                response.Message = $"User ID: {objDto.ObjDTOEXP01.P01F02} not found.";

                return response;
            }

            // check all payee userID exists??
            List<int> lstUserID = objDto.LstDTOCNT01.Select(cont => cont.T01F03).ToList();
            List<int> lstUserIDNotInDB = _context.GetUserIdsNotInDatabase(lstUserID);
            if (lstUserIDNotInDB.Count > 0)
            {
                response.IsError = true;
                response.HttpStatusCode = StatusCodes.Status404NotFound;
                response.Message = "One of provided payee id not found.";

                return response;
            }

            // if unequal share then check share of each contributer = payer paid amount
            if (objDto.IsShareUnequal)
            {
                double total = objDto.LstDTOCNT01.Aggregate(0.0, (acc, curr) => acc + curr.T01F04);
                if (total != objDto.ObjDTOEXP01.P01F04)
                {
                    response.IsError = true;
                    response.HttpStatusCode = StatusCodes.Status409Conflict;
                    response.Message = "Sum of payee to be paid amount didn't matched with payer paid amount.";

                    return response;
                }
            }

            return response;
        }

        /// <summary>
        /// Saves the expense data to the database.
        /// </summary>
        /// <returns>A response indicating the result of the save operation.</returns>
        public Response Save()
        {
            Response response = new Response();

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction tnx = db.OpenTransaction())
                {
                    try
                    {
                        int expenseID = (int)db.Insert<EXP01>(_objExpense, selectIdentity: true);

                        LstContribution.ForEach(contribution =>
                        {
                            contribution.T01F02 = expenseID;
                        });

                        db.InsertAll<CNT01>(LstContribution);

                        tnx.Commit();
                    }
                    catch
                    {
                        tnx.Rollback();
                        throw;
                    }
                }
            }
            response.HttpStatusCode = StatusCodes.Status200OK;
            response.Message = "Expense saved successfully.";
            return response;
        }

        /// <summary>
        /// Performs validation checks before saving expense data.
        /// </summary>
        /// <returns>A response indicating the result of the validation.</returns>
        public Response Validation()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves the settlement report for the specified user.
        /// </summary>
        /// <param name="userID">The ID of the user for whom the settlement report is generated.</param>
        /// <returns>A response containing the settlement report.</returns>
        public Response GetSettlementReport(int userID)
        {
            Response response = new Response();

            SettlementReport settlementReport = _context.GetSettlementReport(userID);

            response.Data = settlementReport;
            response.HttpStatusCode = StatusCodes.Status200OK;
            return response;
        }

        /// <summary>
        /// Settles dues between two users.
        /// </summary>
        /// <param name="userID">The ID of the user initiating the settlement.</param>
        /// <param name="payableUserId">The ID of the user to whom dues are payable.</param>
        /// <param name="amount">The amount to settle.</param>
        /// <returns>A response indicating the result of the settlement operation.</returns>
        public Response SettleDues(int userID, int payableUserId, double amount)
        {
            Response response = new Response();

            if (amount < 0)
            {
                response.IsError = true;
                response.Message = $"Cannot setlle due, amount cannot be minus.";
                response.HttpStatusCode = StatusCodes.Status409Conflict;

                return response;
            }

            if (userID == payableUserId)
            {
                response.IsError = true;
                response.Message = $"Authenticated user id and payable userid are same.";
                response.HttpStatusCode = StatusCodes.Status409Conflict;

                return response;
            }

            // check ids against db
            if (Utility.UserIDExists(userID))
            {
                response.IsError = true;
                response.Message = $"userid {userID} not found";
                response.HttpStatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            if (Utility.UserIDExists(payableUserId))
            {
                response.IsError = true;
                response.Message = $"payable userid {payableUserId} not found";
                response.HttpStatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            // check amount against db
            double amountDB = _context.GetDueAmount(userID, payableUserId);

            if (amountDB != amount)
            {
                response.IsError = true;
                response.Message = $"Incorrect amount {amount}, please recheck due amount.";
                response.HttpStatusCode = StatusCodes.Status409Conflict;

                return response;
            }

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.Update<CNT01>(updateOnly: new { t01f05 = true }, where: (cnt) => cnt.T01F05 == false && (cnt.T01F02 == userID && cnt.T01F03 == payableUserId) || (cnt.T01F02 == payableUserId && cnt.T01F03 == userID));
            }

            response.Message = $"Due amount {amountDB} against user {payableUserId} settled successfully";
            response.HttpStatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}
