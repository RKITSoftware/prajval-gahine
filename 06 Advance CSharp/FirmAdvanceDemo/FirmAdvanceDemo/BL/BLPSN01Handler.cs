using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;

namespace FirmAdvanceDemo.BL
{
    public class BLPSN01Handler : BLResource<PSN01>
    {
        /// <summary>
        /// Instance of PSN01 model
        /// </summary>
        private PSN01 _objPSN01;

        /// <summary>
        /// Default constructor for BLPosition, initializes PSN01 instance
        /// </summary>
        public BLPSN01Handler()
        {
            _objPSN01 = new PSN01();
        }

        /// <summary>
        /// Method to convert DTOPSN01 instance to PSN01 instance
        /// </summary>
        /// <param name="objDTOPSN01">Instance of DTOPSN01</param>
        private void Presave(DTOPSN01 objDTOPSN01)
        {
            _objPSN01 = objDTOPSN01.ConvertModel<PSN01>();
        }

        /// <summary>
        /// Method to validate the PSN01 instance
        /// </summary>
        /// <returns>True if PSN01 instance is valid else false</returns>
        private bool Validate()
        {
            return true;
        }

        /// <summary>
        /// Method to Add (Create) a new record of psn01 table in DB
        /// </summary>
        private void Add()
        {

        }

        /// <summary>
        /// Method to Update (Modify) an existing record psn01 table in DB
        /// </summary>
        private void Update()
        {

        }

    }
}