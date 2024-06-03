using System.Data;

namespace ExpenseSplittingApplication.DL.Interface
{
    public interface IDBUserContext
    {
        public DataTable GetAll();
    }
}
