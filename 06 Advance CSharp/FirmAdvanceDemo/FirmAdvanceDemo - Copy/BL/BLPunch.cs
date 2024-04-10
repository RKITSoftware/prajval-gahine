using FirmAdvanceDemo.Models;
using FirmAdvanceDemo.Utitlity;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.BL
{
    public class BLPunch : BLResource<PCH01>
    {
        [Obsolete]
        private static void RemoveMisPunch(List<PCH01> lstPunch)
        {
            TimeSpan timeBuffer = new TimeSpan(0, 0, 10);    // 1 minute buffer
            for (int i = 1; i < lstPunch.Count; i++)
            {
                if (lstPunch[i - 1].h01f02 == lstPunch[i].h01f02)
                {
                    if (lstPunch[i].h01f03.Subtract(lstPunch[i - 1].h01f03) <= timeBuffer)
                    {
                        lstPunch.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        [Obsolete]
        private static List<PCH01> PopulateAttendanceFromPunch(List<PCH01> lstPunch, ATD01[] lstAttendance)
        {
            DateTime date = lstPunch[0].h01f03.Date;

            List<PCH01> lstUnpairPunch = new List<PCH01>();

            int workHourIndex = 0;
            double tempWorkHour = 0;
            for (int i = 0; i < lstPunch.Count; i += 2)
            {
                if (i < lstPunch.Count - 1 && lstPunch[i].h01f02 == lstPunch[i + 1].h01f02)
                {
                    int EmployeeId = lstPunch[i].h01f02;
                    tempWorkHour += lstPunch[i + 1].h01f03.Subtract(lstPunch[i].h01f03).TotalHours;
                    if (i + 2 == lstPunch.Count || EmployeeId != lstPunch[i + 2].h01f02)
                    {
                        // next punch is of different employee => so sum up the work-hour
                        lstAttendance[workHourIndex++] = new ATD01
                        {
                            d01f02 = EmployeeId,
                            d01f03 = date,
                            d01f04 = tempWorkHour
                        };
                        tempWorkHour = 0;
                    }
                }
                else
                {
                    lstUnpairPunch.Add(lstPunch[i]);

                    lstAttendance[workHourIndex++] = new ATD01
                    {
                        d01f02 = lstPunch[i].h01f02,
                        d01f03 = date,
                        d01f04 = tempWorkHour
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
        public static bool isPunchLegitimate(PCH01 punch)
        {
            return punch.h01f04 != null && punch.h01f04 != PunchType.Mistaken && punch.h01f04 != PunchType.Ambiguous;
        }

        private static List<ATD01> ComputeAttendance(List<PCH01> lstPunch)
        {
            if (lstPunch == null || lstPunch.Count == 0 || lstPunch.Count == 1)
            {
                return null;
            }

            List<ATD01> lstAttendance = new List<ATD01>();

            DateTime date = lstPunch[0].h01f03.Date;

            int size = lstPunch.Count;
            double tempWorkHpur = 0;

            int lastPunchInIndex = -1;
            for (int i = 0; i < size; i++)
            {
                if (lstPunch[i].h01f04 == PunchType.In)
                {
                    lastPunchInIndex = i;
                }
                else if (lstPunch[i].h01f04 == PunchType.Out)
                {
                    tempWorkHpur += lstPunch[i].h01f03.Subtract(lstPunch[lastPunchInIndex].h01f03).TotalHours;
                }

                // check if next employee punch is of different employee ?
                if (tempWorkHpur > 0 && (i == size - 1 || lstPunch[i].h01f02 != lstPunch[i + 1].h01f02))
                {
                    int EmployeeId = lstPunch[i].h01f02;
                    lstAttendance.Add(new ATD01 { d01f02 = EmployeeId, d01f03 = date, d01f04 = tempWorkHpur });
                    tempWorkHpur = 0;
                }
            }

            if (lstAttendance.Count == 0)
            {
                return null;
            }
            return lstAttendance;
        }


        private static void MarkMistakenlyPunch(List<PCH01> lstPunch)
        {
            if (lstPunch == null || lstPunch.Count == 0 || lstPunch.Count == 1)
            {
                return;
            }

            TimeSpan timeBuffer = new TimeSpan(0, 0, 10);    // 1 minute buffer
            for (int i = 0; i < lstPunch.Count; i++)
            {
                if (i != 0 && lstPunch[i - 1].h01f02 == lstPunch[i].h01f02)
                {
                    if (lstPunch[i].h01f03.Subtract(lstPunch[i - 1].h01f03) <= timeBuffer)
                    {
                        //lstPunch.RemoveAt(i);
                        lstPunch[i].h01f04 = PunchType.Mistaken;
                    }
                }
            }
        }

        private static void MarkAmbiguousPunch(List<PCH01> lstPunch)
        {
            if (lstPunch == null || lstPunch.Count == 0)
            {
                return;
            }
            if (lstPunch.Count == 1)
            {
                lstPunch[0].h01f04 = PunchType.Ambiguous;
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
                    tempEmployeeId = lstPunch[0].h01f02;
                    tempEmployeelLegitimatePunchCount++;
                }
                else if (lstPunch[i].h01f04 != PunchType.Mistaken && lstPunch[i].h01f02 == tempEmployeeId)
                {
                    tempEmployeelLegitimatePunchCount++;
                }

                // check if next punch is of different employee
                if (i == size - 1 || lstPunch[i].h01f02 != lstPunch[i + 1].h01f02)
                {
                    if (tempEmployeelLegitimatePunchCount % 2 != 0)
                    {
                        for (int j = tempEmployeeStartIndex; j <= i; j++)
                        {
                            if (lstPunch[j].h01f04 == null)
                            {
                                lstPunch[j].h01f04 = PunchType.Ambiguous;
                            }
                        }
                    }

                    // set up things for next employee punch(es)
                    if (i != size - 1)
                    {
                        tempEmployeeId = lstPunch[i + 1].h01f02;
                        tempEmployeeStartIndex = i + 1;
                        tempEmployeelLegitimatePunchCount = 0;
                    }
                }
            }
        }

        private static void MarkInOutPunch(List<PCH01> lstPunch)
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
                (lstPunch[i].h01f04 != PunchType.Mistaken
                    && lstPunch[i].h01f04 != PunchType.Ambiguous)
                {
                    if (tempIsPunchedIn == false)
                    {
                        lstPunch[i].h01f04 = PunchType.In;
                        tempIsPunchedIn = true;
                    }
                    else
                    {
                        lstPunch[i].h01f04 = PunchType.Out;
                        tempIsPunchedIn = false;
                    }
                }
            }
        }

        private static void UpdatePunchType(List<PCH01> lstPunch)
        {
            MarkMistakenlyPunch(lstPunch);
            MarkAmbiguousPunch(lstPunch);
            MarkInOutPunch(lstPunch);
        }

        public static ResponseStatusInfo EvaluatePunch(DateTime date)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    SqlExpression<PCH01> sqlExp = db.From<PCH01>()
                        .And("Date(h01f03) = Date({0})", date)
                        .OrderBy<PCH01>(punch => new { punch.h01f02, punch.h01f03 });
                    List<PCH01> lstPunch = db.Select<PCH01>(sqlExp);

                    UpdatePunchType(lstPunch);

                    List<ATD01> lstAttendance = ComputeAttendance(lstPunch);

                    // update todays punches type
                    //db.UpdateAll<PCH01>(lstPunch);

                    // update todays attendances
                    //db.InsertAll<ATD01>(lstAttendance);

                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Punches for Date: {date.ToString("yyyy-MM-dd")}",
                        Data = new { lstAttendance = lstAttendance, lstPunch = lstPunch.Select(punch => new { Id = punch.Id, EmployeeId = punch.h01f02, Time = punch.h01f03, Type = punch.h01f04.ToString() }) }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseStatusInfo()
                {
                    IsRequestSuccessful = false,
                    Message = ex.Message,
                    Data = null
                }; ;
            }
        }
    }



    public enum PunchType : byte
    {
        In,
        Out,
        Mistaken,
        Ambiguous,
        VirtualIn,
        VirtualOut,
    }
}