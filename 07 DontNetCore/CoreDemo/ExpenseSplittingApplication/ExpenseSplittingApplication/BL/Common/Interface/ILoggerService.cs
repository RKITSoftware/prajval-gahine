namespace ExpenseSplittingApplication.BL.Common.Interface
{
    public interface ILoggerService
    {
        void Error(string message);

        void Information(string message);

        void Warning(string message);
    }
}
