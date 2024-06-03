using ExpenseSplittingApplication.BL.Common.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;

namespace ExpenseSplittingApplication.BL.Master.Interface
{
    public interface IEXP01Service : ICommonService<DTOEXC>
    {
        public Response GetSettlementReport(int userID);
    }
}
