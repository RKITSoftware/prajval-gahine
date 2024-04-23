﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FirmAdvanceDemo.Models.DTO.DataAnnotations
{
    /// <summary>
    /// Custom validation attribute to validate if a DateTime value is less than or equal to the current date.
    /// </summary>
    public class LessThanCurrentDateTimeAttribute : ValidationAttribute
    {
        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>True if the value is valid; otherwise, false.</returns>
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d <= DateTime.Now.Date;
        }
    }
}
