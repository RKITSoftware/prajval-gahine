using FinalDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalDemo.Service
{
    public  class BLLeave
    {
        public static List<LeaveRequest> lstLeave = new List<LeaveRequest>
        {
            new LeaveRequest { RequestId = 1, EmployeeName = "John Doe", StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 1, 5), Status = "Pending" },
            new LeaveRequest { RequestId = 2, EmployeeName = "Jane Doe", StartDate = new DateTime(2024, 2, 1), EndDate = new DateTime(2024, 2, 5), Status = "Approved" },
            new LeaveRequest { RequestId = 3, EmployeeName = "Bob Smith", StartDate = new DateTime(2024, 3, 1), EndDate = new DateTime(2024, 3, 5), Status = "Rejected" }
        };

            //lstLeave.Add(new LeaveRequest { RequestId = 1, EmployeeName = "John Doe", StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 1, 5), Status = "Pending" });        //static BLLeave()
            //{
            //    lstLeave = new List<LeaveRequest>();
            //    lstLeave.Add(new LeaveRequest { RequestId = 1, EmployeeName = "John Doe", StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 1, 5), Status = "Pending" });
            //    lstLeave = new List<LeaveRequest> {
            //    new LeaveRequest { RequestId = 1, EmployeeName = "John Doe", StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 1, 5), Status = "Pending" },
            //    new LeaveRequest { RequestId = 2, EmployeeName = "Jane Doe", StartDate = new DateTime(2024, 2, 1), EndDate = new DateTime(2024, 2, 5), Status = "Approved" },
            //    new LeaveRequest { RequestId = 3, EmployeeName = "Bob Smith", StartDate = new DateTime(2024, 3, 1), EndDate = new DateTime(2024, 3, 5), Status = "Rejected" }
            //};
            //}

        public static List<LeaveRequest> GetAllLeave()
        {
            return lstLeave;
        }
        public static LeaveRequest GetLeaveRequestById(int id)
        {
            return lstLeave.FirstOrDefault(x => x.RequestId == id);
        }
        public static LeaveRequest SubmitLeaveRequest(LeaveRequest objLeave)
        {
            objLeave.RequestId = lstLeave.Count + 1;
            objLeave.Status = "Pending";
            lstLeave.Add(objLeave);
            int index = lstLeave.IndexOf(objLeave);

            return lstLeave[index];
        }
        public static string ApproveRejectLeaveRequest(int requestId , string newStatus)
        {
            LeaveRequest leaveRequest = lstLeave.FirstOrDefault(x => x.RequestId == requestId);
            if (leaveRequest == null)
                return "NotFound";

            leaveRequest.Status = newStatus;
            return "Approved";
        }

    }
}
