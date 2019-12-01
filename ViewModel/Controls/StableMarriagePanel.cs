using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Controls
{
    public class StableMarriagePanel : ViewModelBase
    {
        private string _state;

        private double _time;

        private double _stablePairs;

        private double _groupHappiness;

        private double _egalitarianHappiness;

        private bool _runable;

        private bool _done;

        public string Name { get; private set; }

        public int Index { get; private set; }

        public string State
        {
            get => _state;
            set
            {
                if(_state != value)
                {
                    _state = value;
                    OnPropertyChanged("State");
                }
            }
        }

        public double Time
        {
            get => _time;
            set
            {
                if(_time != value)
                {
                    _time = value;
                    OnPropertyChanged("Time");
                }
            }
        }

        public double StablePairs
        {
            get => _stablePairs;
            set
            {
                if(_stablePairs != value)
                {
                    _stablePairs = value;
                    OnPropertyChanged("StablePairs");
                }
            }
        }

        public double GroupHappiness
        {
            get => _groupHappiness;
            set
            {
                if(_groupHappiness != value)
                {
                    _groupHappiness = value;
                    OnPropertyChanged("GroupHappiness");
                }
            }
        }

        public double EgalitarianHappiness
        {
            get => _egalitarianHappiness;
            set
            {
                if(_egalitarianHappiness != value)
                {
                    _egalitarianHappiness = value;
                    OnPropertyChanged("EgalitarianHappiness");
                }
            }
        }

        public bool Runable
        {
            get => _runable;
            set
            {
                if(_runable != value)
                {
                    _runable = value;
                    OnPropertyChanged("Runable");
                }
            }
        }

        public bool Done
        {
            get => _done;
            set
            {
                if(_done != value)
                {
                    _done = value;
                    OnPropertyChanged("Done");
                }
            }
        }

        public StableMarriagePanel(string name, int index)
        {
            Name = name;
            Index = index;
        }
    }
}
