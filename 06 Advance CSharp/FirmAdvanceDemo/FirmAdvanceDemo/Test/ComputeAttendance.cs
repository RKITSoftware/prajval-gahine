using FirmAdvanceDemo.BL;
using System;
using static FirmAdvanceDemo.BL.BLATD01Handler;

namespace FirmAdvanceDemo.Test
{
    public class ComputeAttendance
    {
        public static void Run()
        {
            //BLATD01Handler handler = new BLATD01Handler();
            //handler._attendanceProcessingData = new AttendanceProcessingData();

            //DateTime date = new DateTime(2024, 1, 1);
            //for (int i = 1; i <= 31; i++)
            //{
            //    handler._attendanceProcessingData.LstInOutPunchesForDate = handler.GetInOutPunchesForDate(date);
            //    handler._attendanceProcessingData.LstAttendance = handler.ComputeAttendanceFromInOutPunch();
            //    handler.SaveAttendance();
            //    date = date.AddDays(1);
            //}
        }

        public static void Run2()
        {
            //BLATD01Handler handler = new BLATD01Handler();
            //handler._attendanceProcessingData = new AttendanceProcessingData();

            //DateTime date = new DateTime(2022, 1, 1);
            //DateTime now = DateTime.Now;
            //while (date <= now)
            //{
            //    handler._attendanceProcessingData.LstInOutPunchesForDate = handler.GetInOutPunchesForDate(date);
            //    handler._attendanceProcessingData.LstAttendance = handler.ComputeAttendanceFromInOutPunch();
            //    handler.SaveAttendance();
            //    date = date.AddDays(1);
            //}
        }
    }
}