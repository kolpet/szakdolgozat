using System;
using System.Collections.Generic;
using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Controls
{
    public class PreferenceCell : ViewModelBase
    {
        private int _selectedIndex;

        public int X { get; set; }

        public int Y { get; set; }

        public List<int> Preferences { get; set; }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if(_selectedIndex != value)
                {
                    _selectedIndex = value;
                    SelectedChanged?.Invoke(this, null);
                    OnPropertyChanged("SelectedIndex");
                }
            }
        }

        public event EventHandler SelectedChanged;
    }
}
