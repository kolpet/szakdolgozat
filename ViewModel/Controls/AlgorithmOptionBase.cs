using System;
using Szakdolgozat.ViewModel.Events;
using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Controls
{
    public abstract class AlgorithmOptionBase : ViewModelBase
    {
        private string _name;

        private int _index;

        public string Name
        {
            get => _name;
            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                    OnChanged();
                }
            }
        }

        public int Index
        {
            get => _index;
            set
            {
                if(_index != value)
                {
                    _index = value;
                    OnPropertyChanged("Index");
                }
            }
        }

        public event EventHandler<AlgorithmOptionChangedEventArgs> Changed;

        public AlgorithmOptionBase(string name, int index)
        {
            _name = name;
            _index = index;
        }

        public void OnChanged()
        {
            Changed?.Invoke(this, new AlgorithmOptionChangedEventArgs(Index));
        }
    }
}
