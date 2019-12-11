using System;
using System.Windows;
using Szakdolgozat.View.Windows;
using Szakdolgozat.ViewModel;
using Szakdolgozat.ViewModel.Events;
using Szakdolgozat.ViewModel.Windows;

namespace Szakdolgozat.View
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

            context.NewResultWindow += new EventHandler<NewResultWindowEventArgs>(OpenNewResultWindow);

            app.DataContext = context;
            app.Show();
        }

        private void OpenNewResultWindow(object sender, NewResultWindowEventArgs e)
        {
            ResultView view = new ResultView();
            ResultViewModel context = new ResultViewModel(e.Context, e.Result);

            view.DataContext = context;
            view.Show();
        }
    }
}
