using Microsoft.Win32;
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
        private AppView _view;

        private AppViewModel _viewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _view = new AppView();
            _viewModel = new AppViewModel();

            _viewModel.NewResultWindow += new EventHandler<NewResultWindowEventArgs>(NewResultWindow);
            _viewModel.NewSaveFileDialog += new EventHandler<FileDialogEventArgs>(NewSaveFileDialog);
            _viewModel.NewOpenFileDialog += new EventHandler<FileDialogEventArgs>(NewOpenFileDialog);

            _view.DataContext = _viewModel;
            _view.Show();
        }

        private void NewOpenFileDialog(object sender, FileDialogEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = e.Title,
                Filter = e.Filter,
                RestoreDirectory = e.RestoreDirectory,
                InitialDirectory = e.InitialDirectory,
            };

            if(openFileDialog.ShowDialog() == true)
            {
                _viewModel.Load(openFileDialog.FileName);
            }
        }

        private void NewSaveFileDialog(object sender, FileDialogEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = e.Title,
                FileName = e.FileName,
                Filter = e.Filter,
                RestoreDirectory = e.RestoreDirectory,
                InitialDirectory = e.InitialDirectory,
            };

            if(saveFileDialog.ShowDialog() == true)
            {
                _viewModel.Save(saveFileDialog.FileName);
            }
        }

        private void NewResultWindow(object sender, NewResultWindowEventArgs e)
        {
            ResultView view = new ResultView();
            ResultViewModel viewModel = new ResultViewModel(e);

            view.DataContext = viewModel;
            view.Show();
        }
    }
}
