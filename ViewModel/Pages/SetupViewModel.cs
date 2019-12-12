using System;
using Szakdolgozat.Common;
using Szakdolgozat.Model;
using Szakdolgozat.Persistence;
using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Pages
{
    public class SetupViewModel : ViewModelBase, IPageTurn
    {
        private SetupModel _model;

        private IContext _context;
        
        private int _participantNumber;

        private string _group1Name;

        private string _group2Name;

        public DelegateCommand ToProjectCommand { get; private set; }

        public DelegateCommand ToParticipantsCommand { get; private set; }

        public event EventHandler NextPage;

        public event EventHandler PreviousPage;

        public int ParticipantNumber
        {
            get => _participantNumber;
            set
            {
                if(value != _participantNumber)
                {
                    try
                    {
                        _model.ChangeParticipantNumber(value);
                    }
                    catch { }
                    _participantNumber = _context.TotalSize;
                    OnPropertyChanged("ParticipantNumber");
                }
            }
        }

        public string Group1Name {
            get => _group1Name;
            set
            {
                if(value != _group1Name)
                {
                    try
                    {
                        _model.ChangeGroup1Name(value);
                    }
                    catch { }
                    _group1Name = _context.Group1Name;
                    OnPropertyChanged("Group1Name");
                }
            }
        }

        public string Group2Name {
            get => _group2Name;
            set
            {
                if(value != _group2Name)
                {
                    try
                    {
                        _model.ChangeGroup2Name(value);
                    }
                    catch { }
                    _group2Name = _context.Group2Name;
                    OnPropertyChanged("Group2Name");
                }
            }
        }

        public SetupViewModel(SetupModel model, IContext context)
        {
            _model = model;
            _context = context;

            ToProjectCommand = new DelegateCommand(param => OnToProjectCommand());
            ToParticipantsCommand = new DelegateCommand(param => OnToParticipantsCommand());

            _model.Initialize();
        }

        public void RefreshPage()
        {
            Group1Name = _context.Group1Name;
            Group2Name = _context.Group2Name;
            ParticipantNumber = _context.TotalSize;
            OnPropertyChanged("Group1Name");
            OnPropertyChanged("Group2Name");
            OnPropertyChanged("ParticipantNumber");
        }

        public void Load()
        {
            _model.Load();
            RefreshPage();
        }

        private void OnToProjectCommand()
        {
            PreviousPage?.Invoke(this, null);
        }

        private void OnToParticipantsCommand()
        {
            try
            {
                if(_model.IsValid)
                    NextPage?.Invoke(this, null);
            }
            catch { }
        }
    }
}
