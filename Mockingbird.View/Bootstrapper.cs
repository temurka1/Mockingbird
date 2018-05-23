using Mockingbird.BusinessLogic.Telemetry;

namespace Mockingbird.View
{
    using System.Windows;

    using Microsoft.Practices.Unity;
    using Prism.Unity;

    using ViewModels;
    using BusinessLogic;

    public class Bootstraper : UnityBootstrapper
    {
        /// <inheritdoc />
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<ITelemetryReceiver, TelemetryReceiver>();
            Container.RegisterType<MainViewModel>(new ContainerControlledLifetimeManager());
        }

        protected override DependencyObject CreateShell()
        {
            MainWindow window = Container.Resolve<MainWindow>();
            window.DataContext = Container.Resolve<MainViewModel>();

            return window;
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
