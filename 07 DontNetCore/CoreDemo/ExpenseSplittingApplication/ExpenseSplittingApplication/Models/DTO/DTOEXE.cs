using ExpenseSplittingApplication.Models.POCO;

namespace ExpenseSplittingApplication.Models.DTO
{
    public class DTOEXE
    {
        /// <summary>
        /// Instance of DTOEXP01.
        /// </summary>
        public DTOEXP01 ObjDTOEXP01 { get; set; }

        /// <summary>
        /// Instance of DTOUXE01.
        /// </summary>
        public DTOUXE01 ObjDTOUXE01 { get; set; }

        /// <summary>
        /// Is expense shared equally ?
        /// </summary>
        public bool IsShareEqual { get; set; }
    }
}
