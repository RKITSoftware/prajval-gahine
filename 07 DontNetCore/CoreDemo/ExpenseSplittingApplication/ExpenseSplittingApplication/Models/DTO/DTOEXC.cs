using ExpenseSplittingApplication.Models.POCO;

namespace ExpenseSplittingApplication.Models.DTO
{
    public class DTOEXC
    {
        /// <summary>
        /// Instance of DTOEXP01.
        /// </summary>
        public DTOEXP01 ObjDT8OEXP01 { get; set; }

        /// <summary>
        /// Instance of DTOUXE01.
        /// </summary>
        public List<DTOCNT01> LstDTOCNT01 { get; set; }

        /// <summary>
        /// Is expense shared equally ?
        /// </summary>
        public bool IsShareEqual { get; set; }
    }
}
