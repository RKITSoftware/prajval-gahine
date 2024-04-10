using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;

namespace FirmAdvanceDemo.BL
{
    public class BLDepartment : BLResource<DPT01>
    {
        /// <summary>
        /// Instance of DPT01 model
        /// </summary>
        private DPT01 _objDPT01;

        /// <summary>
        /// Default constructor for BLDepartment, initializes DPT01 instance
        /// </summary>
        public BLDepartment()
        {
            _objDPT01 = new DPT01();
        }

        /// <summary>
        /// Method to convert DTODPT01 instance to DPT01 instance
        /// </summary>
        /// <param name="objDTODPT01">Instance of DTODPT01</param>
        private void Presave(DTODPT01 objDTODPT01)
        {
            _objDPT01 = objDTODPT01.ConvertModel<DPT01>();
        }

        /// <summary>
        /// Method to validate the DPT01 instance
        /// </summary>
        /// <returns>True if DPT01 instance is valid else false</returns>
        private bool Validate()
        {
            return true;
        }

        /// <summary>
        /// Method to Add (Create) a new record of dpt01 table in DB
        /// </summary>
        private void Add()
        {

        }

        /// <summary>
        /// Method to Update (Modify) an existing record dpt01 table in DB
        /// </summary>
        private void Update()
        {

        }
    }
}