namespace ExpenseSplittingApplication.BL.Common.Interface
{
    /// <summary>
    /// Interface for logging service operations.
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        void Error(string message);

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <param name="message">The information message to log.</param>
        void Information(string message);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        void Warning(string message);
    }
}
