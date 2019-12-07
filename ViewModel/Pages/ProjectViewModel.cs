using System;
using Szakdolgozat.Persistence;
using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Pages
{
    public class ProjectViewModel : ViewModelBase, IPageTurn
    {

        public DelegateCommand ToSetupCommand { get; private set; }
        
        public event EventHandler NextPage;

        public event EventHandler PreviousPage;

        public event EventHandler DefaultPage;

        public ProjectViewModel()
        {
            ToSetupCommand = new DelegateCommand(param => OnToSetupCommand());
        }

        public void RefreshPage()
        {

        }

        public void Load()
        {
            RefreshPage();
        }

        private void OnToSetupCommand()
        {
            NextPage?.Invoke(this, null);
        }
    }
}
