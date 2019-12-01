using System.Windows;
using Szakdolgozat.View;
using Szakdolgozat.ViewModel;

namespace Szakdolgozat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppView app = new AppView();
            AppViewModel context = new AppViewModel();
            app.DataContext = context;
            app.Show();
        }
    }
}
