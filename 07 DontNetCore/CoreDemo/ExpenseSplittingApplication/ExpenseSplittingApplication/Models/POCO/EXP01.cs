﻿using ServiceStack.DataAnnotations;

namespace ExpenseSplittingApplication.Models.POCO
{
    public class EXP01
    {
        /// <summary>
        /// Expense ID
        /// </summary>
        [PrimaryKey]
        public int P01F01 { get; set; }

        /// <summary>
        /// Payer User ID
        /// </summary>

        public int P01F02 { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string P01F03 { get; set; }

        /// <summary>
        /// Amount paid
        /// </summary>
        public double P01F04{ get; set; }

        /// <summary>
        /// Expense creation datetime
        /// </summary>
        public DateTime P01F098 { get; set; }

        /// <summary>
        /// Expense last modified datetime
        /// </summary>
        public DateTime? P01F099 { get; set; }
    }
}