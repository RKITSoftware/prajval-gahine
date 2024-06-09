using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO
{
    public class DTOEXC
    {
        /// <summary>
        /// Instance of DTOEXP01.
        /// </summary>
        [JsonProperty("expense")]
        public DTOEXP01 ObjDTOEXP01 { get; set; }

        /// <summary>
        /// Instance of DTOUXE01.
        /// </summary>
        [JsonProperty("contributions")]
        public List<DTOCNT01> LstDTOCNT01 { get; set; }

        /// <summary>
        /// Is expense shared equally ?
        /// </summary>
        [Required(ErrorMessage = "Expense share type is required")]
        public bool IsShareUnequal { get; set; }
    }
}
