﻿using System;
using System.Collections.Generic;
using System.IO;
using Szakdolgozat.Common;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Events;
using Szakdolgozat.Persistence;
using Szakdolgozat.ViewModel.Events;
using Szakdolgozat.ViewModel.Pages;
using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel
{
    public class AppViewModel : ViewModelBase
    {
        private List<IPageTurn> _pages;

        private IPageTurn _currentPage;

        private AppModel _appModel;

        private IContext _context;

        public DelegateCommand NewCommand { get; private set; }

        public DelegateCommand SaveCommand { get; private set; }

        public DelegateCommand SaveAsCommand { get; private set; }

        public DelegateCommand LoadCommand { get; private set; }

        public event EventHandler<NewResultWindowEventArgs> NewResultWindow;

        public event EventHandler<FileDialogEventArgs> NewOpenFileDialog;

        public event EventHandler<FileDialogEventArgs> NewSaveFileDialog;

        public event EventHandler<string> NewMessageBox;

        public AppViewModel(): this(0)
        {

        }

        public AppViewModel(int defaultPage)
        {
            _appModel = new AppModel(new BinaryFilePersistence());
            _pages = new List<IPageTurn>();
            _context = _appModel.GetContext;

            NewCommand = new DelegateCommand(param => OnNewCommand());
            SaveCommand = new DelegateCommand(param => OnSave());
            SaveAsCommand = new DelegateCommand(param => OnSaveAs());
            LoadCommand = new DelegateCommand(param => OnLoad());

            ModelBase.ModelError += new EventHandler<ModelErrorEventArgs>(Model_ErrorMessage);

            NewProject(defaultPage);    
        }

        public void NewProject(int defaultPage)
        { 
            ProjectViewModel projectViewModel = new ProjectViewModel(_context);
            SetupViewModel setupViewModel = new SetupViewModel(_appModel.NewSetupModel(), _context);
            ParticipantsViewModel participantsViewModel = new ParticipantsViewModel(_appModel.NewParticipantsModel(), _context);
            PreferencesViewModel preferencesViewModel = new PreferencesViewModel(_appModel.NewPreferencesModel(), _context);
            AlgorithmViewModel algorithmViewModel = new AlgorithmViewModel(_appModel.NewAlgorithmModel(), _context);
            RunViewModel runViewModel = new RunViewModel(_appModel.NewRunModel(), _context);

            runViewModel.CheckSolution += new EventHandler<int>(OnNewResultWindow);

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
            NewMessageBox?.Invoke(this, e.Message);
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

        public void Save(string path)
        {
            _appModel.SaveAsData(Path.GetFullPath(path));
        }

        public void Load(string path)
        {
            _appModel.LoadData(Path.GetFullPath(path));
            foreach(IPageTurn page in _pages)
            {
                page.Load();
            }
        }

        private void OnNewResultWindow(object sender, int e)
        {
            NewResultWindow?.Invoke(this, new NewResultWindowEventArgs(_appModel.NewResultModel(e), _context, e));
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
            NewSaveFileDialog?.Invoke(this, new FileDialogEventArgs
            {
                Title = "Fájl mentése...",
                FileName = DateTime.Now.ToFileTime().ToString(),
                Filter = "Projekt fájl (.save)|*.save",
                RestoreDirectory = true,
                InitialDirectory = Path.GetFullPath(_appModel.SaveDirectory)
            });
        }

        private void OnLoad()
        {
            NewOpenFileDialog?.Invoke(this, new FileDialogEventArgs
            {
                Title = "Fájl betöltése...",
                Filter = "Projekt fájl (.save)|*.save",
                RestoreDirectory = true,
                InitialDirectory = Path.GetFullPath(_appModel.SaveDirectory)
            });
        }
    }
}
