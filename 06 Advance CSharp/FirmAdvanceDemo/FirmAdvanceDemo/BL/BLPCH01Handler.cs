using FirmAdvanceDemo.DB;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;

namespace FirmAdvanceDemo.BL
{
    public class BLPCH01Handler : BLResource<PCH01>
    {
        /// <summary>
        /// Instance of PCH01 model
        /// </summary>
        private PCH01 _objPCH01;

        private DBPCH01Context _context;

        public EnmOperation Operation { get; set; }

        /// <summary>
        /// Default constructor for BLPunch, initializes PCH01 instance
        /// </summary>
        public BLPCH01Handler()
        {
            _context = new DBPCH01Context();
        }

        public Response Prevalidate(DTOPCH01 objDTOPCH01)
        {
            Response response = new Response();

            // validate employee id
            int employeeId = objDTOPCH01.H01F02;
            int employeeCount = objDTOPCH01.H01F02;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                employeeCount = (int)db.Count<EMP01>(employee => employee.P01F01 == employeeId);
            }

            if (employeeCount == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Employee not found with id: {employeeId}";

                return response;
            }

            if (Operation == EnmOperation.E)
            {
                int punchId = objDTOPCH01.H01F01;
                int punchCount;

                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    punchCount = (int)db.Count<PCH01>(punch => punch.P01F01 == punchId);
                }

                if (punchCount == 0)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Punch not found with id: {punchId}";

                    return response;
                }
            }
            return response;
        }

        /// <summary>
        /// Method to convert DTOPCH01 instance to PCH01 instance
        /// </summary>
        /// <param name="objDTOPCH01">Instance of DTOPCH01</param>
        public void Presave(DTOPCH01 objDTOPCH01)
        {
            _objPCH01 = objDTOPCH01.ConvertModel<PCH01>();
            if (Operation == EnmOperation.A)
            {
                _objPCH01.P01F01 = 0;
                _objPCH01.H01F03 = EnmPunchType.U;
                _objPCH01.H01F04 = DateTime.Now;
            }
            else
            {
                _objPCH01.H01F05 = DateTime.Now;
            }
        }

        /// <summary>
        /// Method to validate the PCH01 instance
        /// </summary>
        /// <returns>True if PCH01 instance is valid else false</returns>
        public Response Validate()
        {
            Response response = new Response();
            // nothing to validate for PCH01 as of now
            return response;
        }

        public Response Save()
        {
            Response response = new Response();

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (Operation == EnmOperation.A)
                {
                    db.Insert<PCH01>(_objPCH01);

                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.Message = $"Punched created for employee-id: {_objPCH01.H01F02}";

                    return response;
                }
                else
                {
                    db.Update<PCH01>(_objPCH01);

                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.Message = $"Punched updated with id: {_objPCH01.P01F01}";

                    return response;
                }
            }
        }

        [Obsolete]
        private void RemoveMisPunch(List<PCH01> lstPunch)
        {
            TimeSpan timeBuffer = new TimeSpan(0, 0, 10);    // 1 minute buffer
            for (int i = 1; i < lstPunch.Count; i++)
            {
                if (lstPunch[i - 1].H01F02 == lstPunch[i].H01F02)
                {
                    if (lstPunch[i].H01F04.Subtract(lstPunch[i - 1].H01F04) <= timeBuffer)
                    {
                        lstPunch.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        [Obsolete]
        private List<PCH01> PopulateAttendanceFromPunch(List<PCH01> lstPunch, ATD01[] lstAttendance)
        {
            DateTime date = lstPunch[0].H01F04.Date;

            List<PCH01> lstUnpairPunch = new List<PCH01>();

            int workHourIndex = 0;
            double tempWorkHour = 0;
            for (int i = 0; i < lstPunch.Count; i += 2)
            {
                if (i < lstPunch.Count - 1 && lstPunch[i].H01F02 == lstPunch[i + 1].H01F02)
                {
                    int EmployeeId = lstPunch[i].H01F02;
                    tempWorkHour += lstPunch[i + 1].H01F04.Subtract(lstPunch[i].H01F04).TotalHours;
                    if (i + 2 == lstPunch.Count || EmployeeId != lstPunch[i + 2].H01F02)
                    {
                        // next punch is of different employee => so sum up the work-hour
                        lstAttendance[workHourIndex++] = new ATD01
                        {
                            D01F02 = EmployeeId,
                            D01F03 = date,
                            D01F04 = tempWorkHour
                        };
                        tempWorkHour = 0;
                    }
                }
                else
                {
                    lstUnpairPunch.Add(lstPunch[i]);

                    lstAttendance[workHourIndex++] = new ATD01
                    {
                        D01F02 = lstPunch[i].H01F02,
                        D01F03 = date,
                        D01F04 = tempWorkHour
                    };
                    tempWorkHour = 0;
                    i--;
                }
            }
            if (lstUnpairPunch.Count == 0)
            {
                return null;
            }
            return lstUnpairPunch;
        }

        [Obsolete]
        public bool isPunchLegitimate(PCH01 punch)
        {
            return punch.H01F03 != EnmPunchType.M && punch.H01F03 != EnmPunchType.A;
        }

        private List<ATD01> ComputeAttendance(List<PCH01> lstPunch)
        {
            if (lstPunch == null || lstPunch.Count == 0 || lstPunch.Count == 1)
            {
                return null;
            }

            List<ATD01> lstAttendance = new List<ATD01>();

            DateTime date = lstPunch[0].H01F04.Date;

            int size = lstPunch.Count;
            double tempWorkHpur = 0;

            int lastPunchInIndex = -1;
            for (int i = 0; i < size; i++)
            {
                if (lstPunch[i].H01F03 == EnmPunchType.I)
                {
                    lastPunchInIndex = i;
                }
                else if (lstPunch[i].H01F03 == EnmPunchType.O)
                {
                    tempWorkHpur += lstPunch[i].H01F04.Subtract(lstPunch[lastPunchInIndex].H01F04).TotalHours;
                }

                // check if next employee punch is of different employee ?
                if (tempWorkHpur > 0 && (i == size - 1 || lstPunch[i].H01F02 != lstPunch[i + 1].H01F02))
                {
                    int EmployeeId = lstPunch[i].H01F02;
                    lstAttendance.Add(new ATD01 { D01F02 = EmployeeId, D01F03 = date, D01F04 = tempWorkHpur });
                    tempWorkHpur = 0;
                }
            }

            if (lstAttendance.Count == 0)
            {
                return null;
            }
            return lstAttendance;
        }


        private void MarkMistakenlyPunch(List<PCH01> lstPunch)
        {
            if (lstPunch == null || lstPunch.Count == 0 || lstPunch.Count == 1)
            {
                return;
            }

            TimeSpan timeBuffer = new TimeSpan(0, 0, 10);    // 1 minute buffer
            for (int i = 0; i < lstPunch.Count; i++)
            {
                if (i != 0 && lstPunch[i - 1].H01F02 == lstPunch[i].H01F02)
                {
                    if (lstPunch[i].H01F04.Subtract(lstPunch[i - 1].H01F04) <= timeBuffer)
                    {
                        //lstPunch.RemoveAt(i);
                        lstPunch[i].H01F03 = EnmPunchType.M;
                    }
                }
            }
        }

        private void MarkAmbiguousPunch(List<PCH01> lstPunch)
        {
            if (lstPunch == null || lstPunch.Count == 0)
            {
                return;
            }
            if (lstPunch.Count == 1)
            {
                lstPunch[0].H01F03 = EnmPunchType.A;
                return;
            }
            int size = lstPunch.Count;

            int tempEmployeeId = 0;
            int tempEmployeeStartIndex = 0;
            int tempEmployeelLegitimatePunchCount = 0;

            for (int i = 0; i < size; i++)
            {
                if (i == 0)
                {
                    tempEmployeeId = lstPunch[0].H01F02;
                    tempEmployeelLegitimatePunchCount++;
                }
                else if (lstPunch[i].H01F03 != EnmPunchType.M && lstPunch[i].H01F02 == tempEmployeeId)
                {
                    tempEmployeelLegitimatePunchCount++;
                }

                // check if next punch is of different employee
                if (i == size - 1 || lstPunch[i].H01F02 != lstPunch[i + 1].H01F02)
                {
                    if (tempEmployeelLegitimatePunchCount % 2 != 0)
                    {
                        for (int j = tempEmployeeStartIndex; j <= i; j++)
                        {
                            if (lstPunch[j].H01F03 == EnmPunchType.U)
                            {
                                lstPunch[j].H01F03 = EnmPunchType.A;
                            }
                        }
                    }

                    // set up things for next employee punch(es)
                    if (i != size - 1)
                    {
                        tempEmployeeId = lstPunch[i + 1].H01F02;
                        tempEmployeeStartIndex = i + 1;
                        tempEmployeelLegitimatePunchCount = 0;
                    }
                }
            }
        }

        private void MarkInOutPunch(List<PCH01> lstPunch)
        {
            if (lstPunch == null || lstPunch.Count == 0)
            {
                return;
            }

            int size = lstPunch.Count;
            bool tempIsPunchedIn = false;
            for (int i = 0; i < size; i++)
            {
                if
                (lstPunch[i].H01F03 != EnmPunchType.M
                    && lstPunch[i].H01F03 != EnmPunchType.A)
                {
                    if (tempIsPunchedIn == false)
                    {
                        lstPunch[i].H01F03 = EnmPunchType.I;
                        tempIsPunchedIn = true;
                    }
                    else
                    {
                        lstPunch[i].H01F03 = EnmPunchType.O;
                        tempIsPunchedIn = false;
                    }
                }
            }
        }

        private void UpdatePunchType(List<PCH01> lstPunch)
        {
            MarkMistakenlyPunch(lstPunch);
            MarkAmbiguousPunch(lstPunch);
            MarkInOutPunch(lstPunch);
        }

        public Response GenerateAttendance(DateTime date)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    SqlExpression<PCH01> sqlExp = db.From<PCH01>()
                        .And("Date(H01F04) = Date({0})", date)
                        .OrderBy<PCH01>(punch => new { punch.H01F02, punch.H01F04 });
                    List<PCH01> lstPunch = db.Select<PCH01>(sqlExp);

                    UpdatePunchType(lstPunch);

                    List<ATD01> lstAttendance = ComputeAttendance(lstPunch);

                    // update todays punches type
                    //db.UpdateAll<PCH01>(lstPunch);

                    // update todays attendances
                    //db.InsertAll<ATD01>(lstAttendance);

                    return new Response()
                    {
                        IsError = true,
                        Message = $"Punches for Date: {date.ToString("yyyy-MM-dd")}",
                        Data = new { lstAttendance = lstAttendance, lstPunch = lstPunch.Select(punch => new { Id = punch.P01F01, EmployeeId = punch.H01F02, Time = punch.H01F03, Type = punch.H01F03.ToString() }) }
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
                }; ;
            }
        }
    }
}