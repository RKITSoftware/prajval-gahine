namespace ExpenseSplittingApplication.BL.Common.Interface
{
    public interface ILoggerService
    {
        void Error(Exception ex);

        void Error(string message);

        void Information(string message);

        void Warning(string message);
    }
}
