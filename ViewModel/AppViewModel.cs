using System;
using System.Windows;
using Microsoft.Win32;
using System.Collections.Generic;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Events;
using Szakdolgozat.Persistence;
using Szakdolgozat.ViewModel.Pages;
using Szakdolgozat.ViewModel.Structures;
using System.IO;

namespace Szakdolgozat.ViewModel
{
    public class AppViewModel : ViewModelBase
    {
        private List<IPageTurn> _pages;

        private IPageTurn _currentPage;

        private AppModel _appModel;

        public DelegateCommand NewCommand { get; private set; }

        public DelegateCommand SaveCommand { get; private set; }

        public DelegateCommand SaveAsCommand { get; private set; }

        public DelegateCommand LoadCommand { get; private set; }

        public AppViewModel(): this(0)
        {

        }

        public AppViewModel(int defaultPage)
        {
            _appModel = new AppModel(new XmlFilePersistence());
            _pages = new List<IPageTurn>();

            NewCommand = new DelegateCommand(param => OnNewCommand());
            SaveCommand = new DelegateCommand(param => OnSave());
            SaveAsCommand = new DelegateCommand(param => OnSaveAs());
            LoadCommand = new DelegateCommand(param => OnLoad());

            ModelBase.ModelError += new EventHandler<ModelErrorEventArgs>(Model_ErrorMessage);

            NewProject(defaultPage);    
        }

        public void NewProject(int defaultPage)
        { 
            ProjectViewModel projectViewModel = new ProjectViewModel();
            SetupViewModel setupViewModel = new SetupViewModel();
            ParticipantsViewModel participantsViewModel = new ParticipantsViewModel();
            PreferencesViewModel preferencesViewModel = new PreferencesViewModel();
            AlgorithmViewModel algorithmViewModel = new AlgorithmViewModel();
            RunViewModel runViewModel = new RunViewModel();

            _pages.Clear();
            _pages.Add(projectViewModel);
            _pages.Add(setupViewModel);
            _pages.Add(participantsViewModel);
            _pages.Add(preferencesViewModel);
            _pages.Add(algorithmViewModel);
            _pages.Add(runViewModel);

            foreach(IPageTurn page in _pages)
            {
                page.NextPage += new EventHandler(Context_NextPage);
                page.PreviousPage += new EventHandler(Context_PreviousPage);
            }

            //_context.ReadyChanged += new EventHandler(Context_ReadyChanged);

            CurrentPage = _pages[defaultPage];
        }

        private void Model_ErrorMessage(object sender, ModelErrorEventArgs e)
        {
            //TODO: implement message popup
            //MessageBox.Show(e.Message, "Hiba!");
        }

        private void Context_NextPage(object sender, EventArgs e)
        {
            IPageTurn page = (IPageTurn)sender;

            for(int i = 0; i < _pages.Count - 1; i++)
            {
                if(_pages[i] == page)
                {
                    ChangeViewModel(_pages[i + 1]);
                }
            }
        }

        private void Context_PreviousPage(object sender, EventArgs e)
        {
            IPageTurn page = (IPageTurn)sender;

            for(int i = 1; i < _pages.Count; i++)
            {
                if(_pages[i] == page)
                {
                    ChangeViewModel(_pages[i - 1]);
                }
            }
        }

        private void ChangeViewModel(IPageTurn viewModel)
        {
            if (_pages.Contains(viewModel))
            {
                CurrentPage = viewModel;
                CurrentPage.RefreshPage();
            }
        }

        //public List<ViewModelBase> Pages { get => _pages; }

        public IPageTurn CurrentPage 
        {
            get => _currentPage;
            set 
            {
                if(_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged("CurrentPage");
                }
            }
        }

        private void OnNewCommand()
        {
            NewProject(0);
        }

        private void OnSave()
        {
            if(_appModel.IsSaved)
            {
                _appModel.SaveData();
            }
            else
            {
                OnSaveAs();
            }
        }

        private void OnSaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Fájl mentése...";
            saveFileDialog.Filter = "Szöveges fájl|*.txt|Excel fájl|.cvs";
            saveFileDialog.ShowDialog();

            if(saveFileDialog.FileName != "")
            {
                _appModel.SaveAsData(Path.GetFullPath(saveFileDialog.FileName));
            }
        }

        private void OnLoad()
        {

        }
    }
}
