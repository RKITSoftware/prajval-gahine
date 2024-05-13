using FirmAdvanceDemo.DB;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utility;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Handles business logic related to punch operations.
    /// </summary>
    public class BLPCH01Handler
    {

        #region Private Struct
        /// <summary>
        /// Represents the data needed for processing punch entries.
        /// </summary>
        private struct PunchProcessingData
        {
            /// <summary>
            /// The date of the punch entries being processed.
            /// </summary>
            public DateTime dateOfPunch;

            /// <summary>
            /// The list of all punch entries for the specified date.
            /// </summary>
            public List<PCH01> LstAllPunch;

            /// <summary>
            /// The list of in-out punch entries for the specified date.
            /// </summary>
            public List<PCH01> LstInOutPunch;
        }
        #endregion

        #region Private Fields
        /// <summary>
        /// Instance of PCH01 model.
        /// </summary>
        private PCH01 _objPCH01;

        /// <summary>
        /// Context for Punch handler.
        /// </summary>
        private readonly DBPCH01Context _objDBPCH01Context;

        /// <summary>
        /// Factory for creating OrmLite database connections.
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Represents a list of punch entries that are ambiguous and require further processing.
        /// </summary>
        private List<PCH01> _lstAmbigousPunch;

        /// <summary>
        /// The data for processing punch entries.
        /// </summary>
        private PunchProcessingData _punchProcessingData;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the operation type for punch handling.
        /// </summary>
        public EnmOperation Operation { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor for BLPCH01Handler.
        /// </summary>
        public BLPCH01Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
            _objDBPCH01Context = new DBPCH01Context();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Validates the provided DTOPCH01 instance before processing.
        /// </summary>
        /// <param name="objDTOPCH01">DTOPCH01 instance containing punch data.</param>
        /// <returns>A response indicating the validation result.</returns> 
        public Response Prevalidate(DTOPCH01 objDTOPCH01)
        {
            Response response = new Response();

            // validate employee id
            int employeeID = objDTOPCH01.H01F02;
            int employeeCount = objDTOPCH01.H01F02;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                employeeCount = (int)db.Count<EMP01>(employee => employee.P01F01 == employeeID);
            }

            if (employeeCount == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Employee not found with id: {employeeID}.";

                return response;
            }
            return response;
        }

        /// <summary>
        /// Sets the data for the punch before saving it.
        /// </summary>
        /// <param name="objDTOPCH01">DTOPCH01 instance containing punch data.</param>
        public void Presave(DTOPCH01 objDTOPCH01)
        {
            DateTime now = DateTime.Now;
            _objPCH01 = objDTOPCH01.ConvertModel<PCH01>();
            if (Operation == EnmOperation.A)
            {
                _objPCH01.H01F01 = 0;
                _objPCH01.H01F02 = (int)HttpContext.Current.Items["employeeID"];
                _objPCH01.H01F03 = now;
                _objPCH01.H01F04 = EnmPunchType.U;
                _objPCH01.H01F06 = now;
            }
            else
            {
                _objPCH01.H01F07 = now;
            }
        }

        /// <summary>
        /// Prepares a virtual punch entry for saving.
        /// </summary>
        /// <param name="objDTOPCH01">The DTO representing the virtual punch entry.</param>
        public void PresaveVirtualPunch(DTOPCH01 objDTOPCH01)
        {
            _objPCH01 = objDTOPCH01.ConvertModel<PCH01>();
            _objPCH01.H01F04 = EnmPunchType.A;
            _objPCH01.H01F06 = DateTime.Now;

            string query = string.Format(@"
                                        SELECT
                                            h01f01,
                                            h01f03,
                                            h01f04
                                        FROM
                                            pch01
                                        WHERE
                                            h01f02 = {0} AND
                                            DATE(h01f03) = '{1}' AND
                                            h01f04 = '{2}'
                                        ORDER BY
                                            h01f03",
                                        _objPCH01.H01F02,
                                        _objPCH01.H01F03.ToString(Constants.GlobalDateFormat),
                                        EnmPunchType.A);

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _lstAmbigousPunch = db.Query<PCH01>(query).ToList();
            }

            // insert the incoming punch poco to the above sorted list
            _lstAmbigousPunch.Add(_objPCH01);
            _lstAmbigousPunch.Sort((x, y) => DateTime.Compare(x.H01F03, y.H01F03));
            MarkAmbiguousAsInOutPunch(_lstAmbigousPunch);
        }



        /// <summary>
        /// Saves the virtual punch entry.
        /// </summary>
        /// <returns>A response indicating the success of the save operation.</returns>
        public Response SaveVirtualPunch()
        {
            Response response = new Response();
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.SaveAll<PCH01>(_lstAmbigousPunch);
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"Ambiguous punches resolved for employee {_objPCH01.H01F02} for {_objPCH01.H01F03.ToString(Constants.GlobalDateFormat)}";
            return response;
        }

        /// <summary>
        /// Prepares unprocessed punches for processing for the specified date.
        /// </summary>
        /// <param name="date">The date of the unprocessed punches.</param>
        public void PresaveProcessUnprocessPunchesForDate(DateTime date)
        {
            _punchProcessingData = new PunchProcessingData
            {
                dateOfPunch = date,
                LstAllPunch = GetUnprocessedPunchesForDate(date)
            };
            ProcessUnprocessedPunches();
        }

        /// <summary>
        /// Validates the processed punches.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response ValidateProcessedPunches()
        {
            Response response = new Response();
            if (_punchProcessingData.LstAllPunch.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No unprocessed punch found for date {_punchProcessingData.dateOfPunch.ToString(Constants.GlobalDateFormat)}.";
            }
            return response;
        }

        /// <summary>
        /// Saves the processed punches.
        /// </summary>
        /// <returns>A response indicating the success of the save operation.</returns>
        public Response SaveProcessedPunches()
        {
            Response response = new Response();
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.UpdateAll<PCH01>(_punchProcessingData.LstAllPunch);
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"Punch type updated for date: {_punchProcessingData.dateOfPunch.ToString(Constants.GlobalDateFormat)}.";
            return response;
        }

        /// <summary>
        /// Validates the punch data before saving.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response Validate()
        {
            Response response = new Response();
            // nothing to validate for PCH01 as of now
            return response;
        }

        /// <summary>
        /// Saves the punch data.
        /// </summary>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Save()
        {
            Response response = new Response();

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (Operation == EnmOperation.A)
                {
                    int punchID = (int)db.Insert<PCH01>(_objPCH01, selectIdentity: true);

                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.Message = $"Punch {punchID} created for employee {_objPCH01.H01F02}";

                    return response;
                }
                else
                {
                    db.Update<PCH01>(_objPCH01);

                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.Message = $"Punch {_objPCH01.H01F01} updated. ";

                    return response;
                }
            }
        }

        /// <summary>
        /// Retrieves punch records.
        /// </summary>
        /// <returns>A response containing the retrieved punch data.</returns>
        public Response RetrievePunch()
        {
            Response response = new Response();
            DataTable dtPunch = _objDBPCH01Context.FetchPunch();

            if (dtPunch.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No user found.";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtPunch;
            return response;
        }

        /// <summary>
        /// Retrieves a specific punch record by punch ID.
        /// </summary>
        /// <param name="punchId">The ID of the punch record to retrieve.</param>
        /// <returns>A response containing the retrieved punch data.</returns>
        public Response RetrievePunch(int punchId)
        {
            Response response = new Response();
            DataTable dtPunch = _objDBPCH01Context.FetchPunch(punchId);

            if (dtPunch.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Punch {punchId} not found.";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtPunch;
            return response;
        }

        /// <summary>
        /// Retrieves the punch entries for a specific employee for the specified month and year.
        /// </summary>
        /// <param name="employeeID">The ID of the employee.</param>
        /// <param name="year">The year of the punch entries.</param>
        /// <param name="month">The month of the punch entries.</param>
        /// <returns>A response containing the retrieved punch entries.</returns>
        public Response RetrievePunchForEmployeeByMonth(int employeeID, int year, int month)
        {
            Response response = new Response();
            DataTable dtPunch = _objDBPCH01Context.FetchPunchForEmployeeByMonth(employeeID, year, month);

            if (dtPunch.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No Punch found for employee {employeeID} for {year}/{month}.";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtPunch;
            return response;
        }

        /// <summary>
        /// Retrieves the ambiguous punch entries for the specified date.
        /// </summary>
        /// <param name="date">The date of the ambiguous punch entries.</param>
        /// <returns>A response containing the retrieved ambiguous punch entries.</returns>
        public Response RetrieveAmbiguousPunch(DateTime date)
        {
            Response response = new Response();
            DataTable dtPunch = _objDBPCH01Context.FetchAmbiguousPunch(date);

            if (dtPunch.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No ambiguous punch found for {date.ToString(Constants.GlobalDateFormat)}.";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtPunch;
            return response;
        }

        /// <summary>
        /// Prevalidates a virtual punch entry before processing.
        /// </summary>
        /// <param name="objDTOPCH01">The DTO representing the virtual punch entry.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response PrevalidateVirtualPunch(DTOPCH01 objDTOPCH01)
        {
            Response response = Prevalidate(objDTOPCH01);
            if (!response.IsError)
            {
                // check if employee has any ambigugous punch for the given date?
                bool isAmbigous;
                string query = string.Format(@"
                                        SELECT
                                            COUNT(h01f01)
                                        FROM
                                            pch01
                                        WHERE
                                            h01f02 = {0} AND
                                            DATE(h01f03) = '{1}' AND
                                            h01f04 = '{2}'
                                        ",
                                        objDTOPCH01.H01F02,
                                        objDTOPCH01.H01F03.ToString(Constants.GlobalDateFormat),
                                        EnmPunchType.A);
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    isAmbigous = db.Scalar<int>(query) > 0;
                }

                if (!isAmbigous)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.Conflict;
                    response.Message = $"Employee {objDTOPCH01.H01F02} donot have any ambiguous punch for {objDTOPCH01.H01F03.ToString(Constants.GlobalDateFormat)}";

                    return response;
                }
            }
            return response;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Marks ambiguous punch entries as in-out punches.
        /// </summary>
        /// <param name="lstAllPunch">The list of all punch entries.</param>
        private void MarkAmbiguousAsInOutPunch(List<PCH01> lstAllPunch)
        {
            bool isEmployeePunchIn = false;
            foreach (PCH01 punch in lstAllPunch)
            {
                if (isEmployeePunchIn)
                {
                    punch.H01F04 = EnmPunchType.O;
                }
                else
                {
                    punch.H01F04 = EnmPunchType.I;
                }
                punch.H01F07 = DateTime.Now;
                isEmployeePunchIn = !isEmployeePunchIn;
            }
        }
        /// <summary>
        /// Retrieves the unprocessed punches for the specified date.
        /// </summary>
        /// <param name="date">The date of the unprocessed punches.</param>
        /// <returns>The list of unprocessed punches for the specified date.</returns>
        private List<PCH01> GetUnprocessedPunchesForDate(DateTime date)
        {
            List<PCH01> lstPunch;
            string query = string.Format(@"
                                    SELECT
                                        h01f01,
                                        h01f02,
                                        h01f03,
                                        h01f04
                                    FROM
                                        pch01
                                    WHERE
                                        h01f04 = '{0}' AND
                                        Date(h01f03) = '{1}' AND
                                        h01f05 = 0
                                    ORDER BY
                                        h01f02, h01f03",
                                        EnmPunchType.U,
                                        date.ToString(Constants.GlobalDateFormat));

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                lstPunch = db.Query<PCH01>(query).ToList();
            }
            return lstPunch;
        }
        /// <summary>
        /// Updates the punch type based on certain criteria.
        /// </summary>
        private void ProcessUnprocessedPunches()
        {
            MarkDuplicatePunch();
            MarkAmbiguousPunch();
            MarkInOutPunch();
        }
        /// <summary>
        /// Marks punches that are mistakenly recorded.
        /// </summary>
        private void MarkDuplicatePunch()
        {
            List<PCH01> lstAllPunch = _punchProcessingData.LstAllPunch;
            TimeSpan timeBuffer = new TimeSpan(0, 0, 10);    // 10 second buffer
            int size = lstAllPunch.Count;
            for (int i = 0; i < size - 1; i++)
            {
                PCH01 currPunch = lstAllPunch[i];
                PCH01 nextPunch = lstAllPunch[i + 1];
                if (currPunch.H01F02 == nextPunch.H01F02 && nextPunch.H01F03.Subtract(currPunch.H01F03) <= timeBuffer)
                {
                    currPunch.H01F04 = EnmPunchType.D;
                    currPunch.H01F07 = DateTime.Now;
                }
            }
        }
        /// <summary>
        /// Marks punches that are ambiguous or unclear.
        /// </summary>
        private void MarkAmbiguousPunch()
        {
            List<PCH01> lstAllPunch = _punchProcessingData.LstAllPunch;
            int startIndex = 0;
            int endIndex = 0;
            int size = lstAllPunch.Count;
            int uPunchForEmployeeCount = 0;

            for (int i = 0; i < size; i++)
            {
                PCH01 currPunch = lstAllPunch[i];
                PCH01 nextPunch = (i + 1 >= size) ? null : lstAllPunch?[i + 1];

                if (currPunch.H01F04 == EnmPunchType.U)
                {
                    uPunchForEmployeeCount++;
                }

                if (nextPunch == null || currPunch.H01F02 != nextPunch.H01F02)
                {
                    if (uPunchForEmployeeCount % 2 != 0)
                    {
                        for (int j = startIndex; j <= endIndex; j++)
                        {
                            if (lstAllPunch[j].H01F04 == EnmPunchType.U)
                            {
                                lstAllPunch[j].H01F04 = EnmPunchType.A;
                                lstAllPunch[j].H01F07 = DateTime.Now;
                            }
                        }
                    }
                    uPunchForEmployeeCount = 0;
                    startIndex = i + 1;
                    endIndex = startIndex;
                }
                else
                {
                    endIndex++;
                }
            }
        }
        /// <summary>
        /// Marks punches as In/Out based on certain criteria.
        /// </summary>
        private void MarkInOutPunch()
        {
            List<PCH01> lstAllPunch = _punchProcessingData.LstAllPunch;

            bool isEmployeePunchIn = false;
            foreach (PCH01 punch in lstAllPunch)
            {
                if (punch.H01F04 == EnmPunchType.U)
                {
                    if (isEmployeePunchIn)
                    {
                        punch.H01F04 = EnmPunchType.O;
                    }
                    else
                    {
                        punch.H01F04 = EnmPunchType.I;
                    }
                    punch.H01F07 = DateTime.Now;
                    isEmployeePunchIn = !isEmployeePunchIn;
                }
            }
        }

        #endregion
    }
}