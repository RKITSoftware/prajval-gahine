using ExpenseSplittingApplication.Models;

namespace ExpenseSplittingApplication.BL.Common.Interface
{
    public interface ICommonService<T>
    {
        EnmOperation Operation { get; set; }

        Response PreValidation(T objDto);

        void PreSave(T objDto);

        Response Validation();

        Response Save();

        Response DeleteValidation(int id);

        Response Delete(int id);
    }
}
