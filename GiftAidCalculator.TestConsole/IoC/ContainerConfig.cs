using Autofac;

namespace GiftAidCalculator
{
    using Context.TaxRate;
    using Service;
    namespace TestConsole.IoC
    {
        public static class ContainerConfig
        {
            public static IContainer Configure()
            {
                var builder = new ContainerBuilder();

                builder.RegisterType<Application>().As<IApplication>();
                builder.RegisterType<TaxRateStore>().As<ITaxRateStore>();
                builder.RegisterType<GiftAidCalculatorService>().As<IGiftAidCalculatorService>();

                return builder.Build();
            }
        }
    }
}
