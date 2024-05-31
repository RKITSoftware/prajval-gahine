using ExpenseSplittingApplication.BL.Common.Interface;
using ExpenseSplittingApplication.Models.DTO;

namespace ExpenseSplittingApplication.BL.Master.Interface
{
    public interface IUSR01Service : ICommonService<DTOUSR01>
    {
        object? GetAll();
    }
}
