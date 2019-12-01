using System;
using System.Collections.ObjectModel;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Pages
{
    public class ParticipantsViewModel : ViewModelBase, IPageTurn
    {
        private ParticipantsModel _model;

        public DelegateCommand EditParticipantCommand { get; private set; }

        public DelegateCommand ToSetupCommand { get; private set; }

        public DelegateCommand ToPreferencesCommand { get; private set; }

        public ObservableCollection<Participant> Group1Participants { get; set; }

        public ObservableCollection<Participant> Group2Participants { get; set; }

        public Participant SelectedParticipant { get; set; }

        public string Group1Name { get; set; }

        public string Group2Name { get; set; }

        public event EventHandler NextPage;

        public event EventHandler PreviousPage;

        public ParticipantsViewModel()
        {
            _model = new ParticipantsModel();

            EditParticipantCommand = new DelegateCommand(param => OnEditParticipant());
            ToSetupCommand = new DelegateCommand(param => OnToSetupCommand());
            ToPreferencesCommand = new DelegateCommand(param => OnToPreferencesCommand());

            RefreshPage();
        }

        public void RefreshPage()
        {
            if(_model.GetContext.SetupChanged)
            {
                _model.Initialize();

                Group1Participants = new ObservableCollection<Participant>();
                Group2Participants = new ObservableCollection<Participant>();
                Group1Name = _model.GetContext.Group1Name;
                Group2Name = _model.GetContext.Group2Name;

                foreach(Participant participant in _model.GetContext.Group1Participants)
                {
                    Group1Participants.Add(participant);
                }
                foreach(Participant participant in _model.GetContext.Group2Participants)
                {
                    Group2Participants.Add(participant);
                }

                OnPropertyChanged("Group1Name");
                OnPropertyChanged("Group2Name");
                OnPropertyChanged("Group1Participants");
                OnPropertyChanged("Group2Participants");
            }
        }

        private void OnEditParticipant()
        {
            _model.EditParticipant(SelectedParticipant.ID, SelectedParticipant.Name);
        }

        private void OnToSetupCommand()
        {
            PreviousPage?.Invoke(this, null);
        }

        private void OnToPreferencesCommand()
        {
            try
            {
                _model.Validate();
                NextPage?.Invoke(this, null);
            } catch { }
        }
    }
}
