using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;

namespace ExpenseSplittingApplication.BL.Master.Service
{
    public class BLEXP01Handler : IEXP01Service
    {
        public EnmOperation Operation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Response DeleteValidation(int id)
        {
            throw new NotImplementedException();
        }

        public void PreSave(DTOEXC objDto)
        {
            throw new NotImplementedException();
        }

        public Response PreValidation(DTOEXC objDto)
        {
            throw new NotImplementedException();
        }

        public Response Save()
        {
            throw new NotImplementedException();
        }

        public Response Validation()
        {
            throw new NotImplementedException();
        }
    }
}
