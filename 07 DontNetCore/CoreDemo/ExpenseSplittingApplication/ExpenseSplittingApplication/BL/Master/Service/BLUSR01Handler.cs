using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;

namespace ExpenseSplittingApplication.BL.Master.Service
{
    public class BLUSR01Handler : IUSR01Service
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

        public void PreSave(DTOUSR01 objDto)
        {
            throw new NotImplementedException();
        }

        public Response PreValidation(DTOUSR01 objDto)
        {
            // add
            // username already exists?
            bool isUsernameExists = 

            // edit
            // userId exists
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
