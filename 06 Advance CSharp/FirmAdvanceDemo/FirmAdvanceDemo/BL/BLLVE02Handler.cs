using FirmAdvanceDemo.DB;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utility;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using static FirmAdvanceDemo.Utility.Constants;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Handles business logic related to Leave operations.
    /// </summary>
    public class BLLVE02Handler
    {
        /// <summary>
        /// Instance of LVE02 model.
        /// </summary>
        private LVE02 _objLVE02;

        /// <summary>
        /// Factory for creating OrmLite database connections.
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Context for Leave handler.
        /// </summary>
        private readonly DBLVE02Context _objDBLVE02Context;

        /// <summary>
        /// Operation type for Leave handling.
        /// </summary>
        public EnmOperation Operation;

        /// <summary>
        /// Default constructor for BLLVE02Handler.
        /// </summary>
        public BLLVE02Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
            _objDBLVE02Context = new DBLVE02Context();
        }

        /// <summary>
        /// Method to validate the provided DTOLVE02 instance before processing.
        /// </summary>
        /// <param name="objDTOLVE02">DTOLVE02 instance containing leave data.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response Prevalidate(DTOLVE02 objDTOLVE02)
        {
            Response response = new Response();

            // check employee ID in either A or E
            bool isEmployeeExists;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                isEmployeeExists = db.Exists<EMP01>(new { e01f01 = objDTOLVE02.E02F02 });
            }

            if (!isEmployeeExists)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Employee not found with id: {objDTOLVE02.E02F02}";

                return response;
            }

            if (Operation == EnmOperation.E)
            {
                // check leave ID in case of E
                bool isLeaveExists;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    isLeaveExists = db.Exists<LVE02>(new { e02f01 = objDTOLVE02.E02F01 });
                }
                if (!isLeaveExists)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Leave not found for id: {objDTOLVE02.E02F01}";

                    return response;
                }
            }
            return response;
        }

        /// <summary>
        /// Method to convert DTOLVE02 instance to LVE02 instance.
        /// </summary>
        /// <param name="objDTOLVE02">Instance of DTOLVE02.</param>
        public void Presave(DTOLVE02 objDTOLVE02)
        {
            _objLVE02 = objDTOLVE02.ConvertModel<LVE02>();
            DateTime now = DateTime.Now;
            
            if (Operation == EnmOperation.A)
            {
                _objLVE02.E02F01 = 0;
                if (_objLVE02.E02F03 <= now)
                {
                    _objLVE02.E02F06 = EnmLeaveStatus.H;
                }
                else
                {
                    _objLVE02.E02F06 = EnmLeaveStatus.P;
                }
                _objLVE02.E02F08 = DateTime.Now;
            }
            else
            {
                _objLVE02.E02F09 = DateTime.Now;
            }
        }

        /// <summary>
        /// Validates the leave data before saving.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response Validate()
        {
            Response response = new Response();
            
            if(Operation == EnmOperation.E)
            {
                LVE02 objLVE02Existsing;

                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    objLVE02Existsing = db.SingleById<LVE02>(_objLVE02.E02F01);
                }

                // leave can only be edited when its status is pending
                if (objLVE02Existsing.E02F06 != EnmLeaveStatus.P)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.Forbidden;
                    response.Message = $"Leave {_objLVE02.E02F01} is not in pending state.";

                    return response;
                }

                // furture leave date must be greater than or equal to current date
                if (objLVE02Existsing.E02F06 == EnmLeaveStatus.P && _objLVE02.E02F03 < DateTime.Now)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.Forbidden;
                    response.Message = $"A future leave cannot be dated less than current date.";

                    return response;
                }

                // historic leave date must be less than or equal to creation date
                if(objLVE02Existsing.E02F06 == EnmLeaveStatus.H && _objLVE02.E02F03 >= objLVE02Existsing.E02F08)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.Forbidden;
                    response.Message = $"A historic leave cannot be dated more than it's creation date.";

                    return response;
                }
            }
            return response;
        }

        /// <summary>
        /// Saves the leave data.
        /// </summary>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Save()
        {
            Response response = new Response();

            if (Operation == EnmOperation.A)
            {
                if (!_objDBLVE02Context.RequestLeave(_objLVE02))
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.Conflict;
                    response.Message = "Leave request already exists within the specified range.";

                    return response;
                }

                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(_objLVE02);
                }

                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "Leave request submitted.";

                return response;
            }
            else
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    db.Update<LVE02>(_objLVE02);
                }
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = $"Leave: {_objLVE02.E02F01} updated";

                return response;
            }
        }

        //check378 - get leave by leave status
        /// <summary>
        /// Method to get leave count of an employee for a specific month-year.
        /// </summary>
        /// <param name="EmployeeId">Employee ID.</param>
        /// <param name="month">Leave month.</param>
        /// <param name="year">Leave year.</param>
        /// <returns>A response containing the leave count.</returns>
        public Response GetLeaveCountByEmployeeIdAnMonthYear(int EmployeeId, int month, int year)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    DateTime MonthFirstDate = new DateTime(year, month, 1);
                    DateTime MonthLastDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                    SqlExpression<LVE02> sqlExp = db.From<LVE02>()
                        .Where(l => l.E02F02 == EmployeeId && l.E02F06 == EnmLeaveStatus.A)
                        .And(l => l.E02F03 >= MonthFirstDate && l.E02F03 <= MonthLastDate)
                        .Or(
                            "(ADDDATE(E01F04, INTERVAL (E01F05 - 1) DAY) >= {0} AND ADDDATE(E01F04, INTERVAL (E01F05 - 1) DAY) <= {1})",
                            MonthFirstDate.ToString("yyyy-MM-dd"),
                            MonthLastDate.ToString("yyyy-MM-dd")
                     );
                    string v = $"sum(CASE WHEN E01F04 < '{MonthFirstDate:yyyy-MM-dd}' THEN DATEDIFF(ADDDATE(E01F04, INTERVAL(E01F05 - 1) DAY), '{MonthFirstDate:yyyy-MM-dd}') + 1 WHEN ADDDATE(E01F04, INTERVAL(E01F05 - 1) DAY) > '{MonthLastDate:yyyy-MM-dd}' THEN DATEDIFF(E01F04, '{MonthLastDate:yyyy-MM-dd}') + 1 ELSE DATEDIFF(ADDDATE(E01F04, INTERVAL(E01F05 - 1) DAY), E01F04) + 1 END) AS Leave_Count";
                    sqlExp.SelectExpression = v;

                    int leaveCount = db.Scalar<int>(sqlExp);

                    return new Response()
                    {
                        IsError = true,
                        Message = $"Leave count for employee with employeeId: {EmployeeId} for monht/year: {month}/{year}",
                        Data = new { LeaveCount = leaveCount }
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    IsError = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public Response PrevalidateUpdateLeaveStatus(int leaveID, EnmLeaveStatus toLeaveStatus)
        {
            Response response = new Response();
            if(toLeaveStatus != EnmLeaveStatus.A || toLeaveStatus != EnmLeaveStatus.R)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.Forbidden;
                response.Message = $"Update leave status must be Approve or Reject.";

                return response;
            }

            bool isLeaveExists;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                isLeaveExists = db.Exists<LVE02>(new { e02f01 = leaveID });
            }
            if (!isLeaveExists)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Leave not found for id: {leaveID}.";

                return response;
            }
            return response;
        }

        public Response UpdateLeaveStatus(int leaveID, EnmLeaveStatus toLeaveStatus)
        {
            Response response = new Response();

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.Update<LVE02>(new { e02f06 = toLeaveStatus }, where: leave => leave.E02F01 == leaveID);
            }

            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"Leave {leaveID} updated successfully.";

            return response;
        }

        /// <summary>
        /// Method to retrieve all leave records.
        /// </summary>
        /// <returns>A response containing the retrieved leave data.</returns>
        public Response RetrieveLeave()
        {
            Response response = new Response();
            DataTable dtLeave = _objDBLVE02Context.FetchLeave();
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No leaves found";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }

        /// <summary>
        /// Method to retrieve a specific leave record by its ID.
        /// </summary>
        /// <param name="leaveID">Leave ID.</param>
        /// <returns>A response containing the retrieved leave data.</returns>
        public Response RetrieveLeave(int leaveID)
        {
            Response response = new Response();
            DataTable dtLeave = _objDBLVE02Context.FetchLeave(leaveID);
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "Leave: {leaveID} not found";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }

        /// <summary>
        /// Method to retrieve leave records by their status.
        /// </summary>
        /// <param name="leaveStatus">Leave Status.</param>
        /// <returns>A response containing the retrieved leave data.</returns>
        public Response RetrieveLeaveByStatus(EnmLeaveStatus leaveStatus)
        {
            Response response = new Response();
            DataTable dtLeave = _objDBLVE02Context.FetchLeaveByStatus(leaveStatus);
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No leaves found";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }

        /// <summary>
        /// Method to retrieve leave records for a specific employee.
        /// </summary>
        /// <param name="employeeId">Employee ID.</param>
        /// <returns>A response containing the retrieved leave data.</returns>
        public Response RetrieveLeaveByEmployee(int employeeId)
        {
            Response response = new Response();
            DataTable dtLeave = _objDBLVE02Context.FetchLeaveByEmployee(employeeId);
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No leaves found for employee: {employeeId}";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }


        /// <summary>
        /// Method to retrieve leave records for a specific month and year.
        /// </summary>
        /// <param name="year">Year.</param>
        /// <param name="month">Month.</param>
        /// <returns>A response containing the retrieved leave data.</returns>
        public Response RetrieveLeaveByMonthYear(int year, int month)
        {
            Response response = new Response();
            DataTable dtLeave = _objDBLVE02Context.FetchLeaveByMonth(year, month);
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No leaves found for month: {year}/{month}.";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }

        /// <summary>
        /// Method to retrieve leave records for a specific date.
        /// </summary>
        /// <param name="date">Date.</param>
        /// <returns>A response containing the retrieved leave data.</returns>
        public Response RetrieveLeaveForDate(DateTime date)
        {
            Response response = new Response();
            DataTable dtLeave = _objDBLVE02Context.FetchLeaveForDate(date);
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No leaves found for date: {date.ToString(GlobalDateFormat)}.";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }

        /// <summary>
        /// Method to retrieve leave records for a specific employee and month-year.
        /// </summary>
        /// <param name="employeeId">Employee ID.</param>
        /// <param name="year">Year.</param>
        /// <param name="month">Month.</param>
        /// <returns>A response containing the retrieved leave data.</returns>
        public Response RetrieveLeaveByEmployeeAndMonthYear(int employeeId, int year, int month)
        {
            Response response = new Response();
            //DataTable dtLeave = _objDBLVE02Context.FetchLeaveByEmployeeAndMonthYear(employeeId, year, month);
            DataTable dtLeave = _objDBLVE02Context.FetchLeaveGeneral(employeeId, year, month);
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No leaves found for employee {employeeId} for {year}/{month}.";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }

        /// <summary>
        /// Method to retrieve leave records for a specific employee and year.
        /// </summary>
        /// <param name="employeeID">Employee ID.</param>
        /// <param name="year">Year.</param>
        /// <returns>A response containing the retrieved leave data.</returns>
        public Response RetrieveLeaveByEmployeeAndYear(int employeeID, int year)
        {
            Response response = new Response();
            DataTable dtLeave = _objDBLVE02Context.FetchLeaveByEmployeeAndYear(employeeID, year);
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No leaves found for employee {employeeID} for {year}.";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }

        public Response ValidateEmployeeLeave(int leaveID)
        {
            // check if leave exists
            // if yes then get employee id
            // then compare it with employee id in items

            Response response = new Response();
            int employeeID;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                employeeID = db.Scalar<LVE02, int>(leave => leave.E02F02, leave => leave.E02F01 == leaveID);
            }

            if (employeeID == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Leave {leaveID} not found.";

                return response;
            }
            response = GeneralUtility.ValidateAccess(employeeID);
            return response;
        }

        /// <summary>
        /// Method to validate leave deletion.
        /// </summary>
        /// <param name="leaveID">Leave ID.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response ValidateDelete(int leaveID)
        {
            Response response = new Response();

            int count;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                count = (int)db.Count<LVE02>(leave => leave.E02F01 == leaveID);
            }
            if (count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Leave with ID {leaveID} not found.";

                return response;
            }

            // if employee then check employeeID associated with leaveID
            if (!GeneralUtility.IsAdmin())
            {
                int leaveEmployeeID;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    leaveEmployeeID = db.Scalar<LVE02, int>(leave => leave.E02F02, leave => leave.E02F01 == leaveID);
                }
                if (!GeneralUtility.IsAuthorizedEmployee(_objLVE02.E02F02))
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.Conflict;
                    response.Message = $"You are not authorized to access employee {leaveEmployeeID}.";

                    return response;
                }
            }
            return response;
        }

        /// <summary>
        /// Method to delete a leave record.
        /// </summary>
        /// <param name="leaveID">Leave ID.</param>
        /// <returns>A response indicating the outcome of the delete operation.</returns>
        public Response Delete(int leaveID)
        {
            Response response = new Response();

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<LVE02>(leaveID);
            }

            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"Leave with ID {leaveID} deleted.";

            return response;
        }

        public Response ValidateUpdateLeaveStatus(int leaveID, EnmLeaveStatus toLeaveStatus)
        {
            Response response = new Response();

            // check if toLeaveStatus is
            if (toLeaveStatus == EnmLeaveStatus.P)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                response.Message = "Leave request cannot be reverted to pending status.";

                return response;
            }

            // check if leave exists ?
            bool isLeaveExists;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                isLeaveExists = db.Exists<LVE02>(new { e02f01 = leaveID });
            }
            if (!isLeaveExists)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Leave {leaveID} not found.";

                return response;
            }

            // check if existing leave status is not historic
            EnmLeaveStatus leaveStatus;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                leaveStatus = db.Scalar<LVE02, EnmLeaveStatus>(leave => leave.E02F06, leave => leave.E02F01 == leaveID);
            }

            if (leaveStatus == EnmLeaveStatus.H)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                response.Message = $"Leave status cannot be changed for historical records.";

                return response;
            }
            return response;
        }

        public Response SaveLeaveStatus(int leaveID, EnmLeaveStatus toLeaveStatus)
        {
            Response response = new Response();

            IEnumerable<Claim> claims = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims;
            // get user ID from identity
            int userID = int.Parse(claims.Where(c => c.Type == "userID")
                   .Select(c => c.Value).SingleOrDefault());

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.Update<LVE02>(new { e02f06 = toLeaveStatus,  e02f07 = userID }, where: leave => leave.E02F01 == leaveID);
            }

            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"Leave {leaveID} status updated to {toLeaveStatus}.";
            return response;
        }
    }
}