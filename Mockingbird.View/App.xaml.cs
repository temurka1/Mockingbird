using System.Windows;

namespace Mockingbird.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Bootstraper bootstrapper = new Bootstraper();
            bootstrapper.Run();
        }
    }
}
