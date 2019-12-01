using System;
using System.Collections.Generic;
using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Controls
{
    public class PreferenceCell : ViewModelBase
    {
        private int _selected;

        public int X { get; set; }

        public int Y { get; set; }

        public List<int> Preferences { get; set; }

        public int Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }

        public event EventHandler SelectedChanged;
    }
}
