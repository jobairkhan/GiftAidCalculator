using Autofac;

namespace GiftAidCalculator.TestConsole
{
    class Program
	{
		static void Main(string[] args)
		{
            var container = IoC.ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Run(args);
            }            
		}
	}
}
