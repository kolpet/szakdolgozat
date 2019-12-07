using System;
using Szakdolgozat.Persistence;

namespace Szakdolgozat.ViewModel.Pages
{
    public interface IPageTurn
    {
        event EventHandler NextPage;

        event EventHandler PreviousPage;

        void RefreshPage();

        void Load();
    }
}