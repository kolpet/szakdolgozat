using System;
using Szakdolgozat.Model;
using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Pages
{
    public class SetupViewModel : ViewModelBase, IPageTurn
    {
        private int _participantNumber;

        private string _group1Name;

        private string _group2Name;

        private SetupModel _model;

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
                    _participantNumber = _model.GetContext.TotalSize;
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
                    _group1Name = _model.GetContext.Group1Name;
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
                    _group2Name = _model.GetContext.Group2Name;
                    OnPropertyChanged("Group2Name");
                }
            }
        }

        public SetupViewModel()
        {
            _model = new SetupModel();

            ToProjectCommand = new DelegateCommand(param => OnToProjectCommand());
            ToParticipantsCommand = new DelegateCommand(param => OnToParticipantsCommand());

            Group1Name = _model.GetContext.Group1Name;
            Group2Name = _model.GetContext.Group1Name;
            ParticipantNumber = _model.GetContext.TotalSize;
        }

        public void RefreshPage()
        {
            _model.Initialize();
        }

        private void OnToProjectCommand()
        {
            PreviousPage?.Invoke(this, null);
        }

        private void OnToParticipantsCommand()
        {
            if(_model.IsValid)
                NextPage(this, null);
        }
    }
}
