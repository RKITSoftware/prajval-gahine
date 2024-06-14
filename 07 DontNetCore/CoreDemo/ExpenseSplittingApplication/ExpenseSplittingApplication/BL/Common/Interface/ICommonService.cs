using ExpenseSplittingApplication.Models;

namespace ExpenseSplittingApplication.BL.Common.Interface
{
    /// <summary>
    /// Generic interface for common service operations.
    /// </summary>
    /// <typeparam name="T">The type of DTO (Data Transfer Object) handled by the service.</typeparam>
    public interface ICommonService<T>
    {
        /// <summary>
        /// Gets or sets the operation type for the service.
        /// </summary>
        EnmOperation Operation { get; set; }

        /// <summary>
        /// Performs pre-validation logic on the provided DTO.
        /// </summary>
        /// <param name="objDto">The DTO object to validate.</param>
        /// <returns>A <see cref="Response"/> object indicating the result of pre-validation.</returns>
        Response PreValidation(T objDto);

        /// <summary>
        /// Performs pre-save logic on the provided DTO.
        /// </summary>
        /// <param name="objDto">The DTO object to perform pre-save operations on.</param>
        void PreSave(T objDto);

        /// <summary>
        /// Performs validation logic.
        /// </summary>
        /// <returns>A <see cref="Response"/> object indicating the result of validation.</returns>
        Response Validation();

        /// <summary>
        /// Saves the changes made to the DTO.
        /// </summary>
        /// <returns>A <see cref="Response"/> object indicating the result of the save operation.</returns>
        Response Save();
    }
}
