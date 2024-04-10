using CustomDIContainer.logger;

namespace CustomDIContainer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Container container = new Container();

            container.Register<ILogger, FileLogger>();
            //container.Register<ILogger>(new FileLogger());
            container.RegisterSingleton<FileLogger>(() => new FileLogger());

            ILogger logger = (ILogger)container.GetInstance(typeof(ILogger));

            logger.Write("Good Morning");
        }
    }
}