﻿using ExpenseSplittingApplication.BL.Domain;
using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Common.Interface;
using ExpenseSplittingApplication.DL.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;
using ExpenseSplittingApplication.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace ExpenseSplittingApplication.BL.Master.Service
{
    public class BLEXP01Handler : IEXP01Service
    {
        private EXP01 _objExpense;
        private List<CNT01> _lstContribution;

        private IUtility _utility;
        private IDbConnectionFactory _dbFactory;
        private IDBExpenseContext _context;
        public EnmOperation Operation { get; set; }

        public BLEXP01Handler(IUtility utility, IDbConnectionFactory dbFactory, IDBExpenseContext context)
        {
            _utility = utility;
            _dbFactory = dbFactory;
            _context = context;
        }

        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Response DeleteValidation(int id)
        {
            throw new NotImplementedException();
        }

        public void PreSave(DTOEXC objDto)
        {
            _objExpense = new EXP01()
            {
                P01F02 = objDto.ObjDTOEXP01.P01F02,
                P01F03 = objDto.ObjDTOEXP01.P01F03,
                P01F04 = objDto.ObjDTOEXP01.P01F04,
                P01F05 = objDto.ObjDTOEXP01.P01F05,
                P01F98 = DateTime.Now,
            };

            double payeeAmount = Int32.MinValue;
            if (!objDto.IsShareUnequal)
            {
                payeeAmount = objDto.ObjDTOEXP01.P01F04 / objDto.LstDTOCNT01.Count;
            }

            _lstContribution = objDto.LstDTOCNT01.Select(dtoContribution => (new CNT01()
            {
                T01F03 = dtoContribution.T01F03,
                T01F04 = objDto.IsShareUnequal ? dtoContribution.T01F04 : payeeAmount,
                T01F05 = objDto.ObjDTOEXP01.P01F02 == dtoContribution.T01F03 ? true : false,
                T01F98 = DateTime.Now,
            })).ToList();
        }

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
            if(!objDto.LstDTOCNT01.Any(contribution => contribution.T01F03 == objDto.ObjDTOEXP01.P01F02))
            {
                response.IsError = true;
                response.HttpStatusCode = StatusCodes.Status409Conflict;
                response.Message = "Payer not in contributions.";

                return response;
            }

            // check payer userID exists??
            if (!_utility.UserIDExists(objDto.ObjDTOEXP01.P01F02))
            {
                response.IsError = true;
                response.HttpStatusCode = StatusCodes.Status404NotFound;
                response.Message = $"User ID: {objDto.ObjDTOEXP01.P01F01} not found.";

                return response;
            }

            // check all payee userID exists??
            List<int> lstUserID = objDto.LstDTOCNT01.Select(cont => cont.T01F03).ToList();
            List<int> lstUserIDNotInDB = _context.GetUserIdsNotInDatabase(lstUserID);
            if(lstUserIDNotInDB.Count > 0)
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
                if(total != objDto.ObjDTOEXP01.P01F04)
                {
                    response.IsError = true;
                    response.HttpStatusCode = StatusCodes.Status409Conflict;
                    response.Message = "Sum of payee to be paid amount didn't matched with payer paid amount.";

                    return response;
                }
            }

            return response;
        }

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

                        _lstContribution.ForEach(contribution =>
                        {
                            contribution.T01F02 = expenseID;
                        });

                        db.InsertAll<CNT01>(_lstContribution);

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

        public Response Validation()
        {
            throw new NotImplementedException();
        }

        public Response GetSettlementReport(int userID)
        {
            Response response = new Response();

            SettlementReport settlementReport = _context.GetSettlementReport(userID);

            response.Data = settlementReport;
            response.HttpStatusCode = StatusCodes.Status200OK;
            return response;
        }
    }
}
