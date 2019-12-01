using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Controls
{
    public class AlgorithmOptionGenetic : AlgorithmOptionBase, IAlgorithmOptionElement, IGeneticSettings
    {
        private double _absoluteSelection;

        private double _selectionRate;

        private double _mutationChance;

        private double _stablePairWeight;

        private double _groupHappinessWeight;

        private double _egalitarianHappinessWeight;

        private int _size;

        private int _generations;

        public double AbsoluteSelection
        {
            get => _absoluteSelection;
            set
            {
                if(_absoluteSelection != value)
                {
                    _absoluteSelection = value;
                    OnPropertyChanged("AbsoluteSelection");
                    OnChanged();
                }
            }
        }

        public double SelectionRate
        {
            get => _selectionRate;
            set
            {
                if(_selectionRate != value)
                {
                    _selectionRate = value;
                    OnPropertyChanged("SelectionRate");
                    OnChanged();
                }
            }
        }

        public double MutationChance
        {
            get => _mutationChance;
            set
            {
                if(_mutationChance != value)
                {
                    _mutationChance = value;
                    OnPropertyChanged("MutationChance");
                    OnChanged();
                }
            }
        }

        public double StablePairWeight
        {
            get => _stablePairWeight;
            set
            {
                if(_stablePairWeight != value)
                {
                    _stablePairWeight = value;
                    OnPropertyChanged("StablePairWeight");
                    OnChanged();
                }
            }
        }

        public double GroupHappinessWeight
        {
            get => _groupHappinessWeight;
            set
            {
                if(_groupHappinessWeight != value)
                {
                    _groupHappinessWeight = value;
                    OnPropertyChanged("GroupHappinessWeight");
                    OnChanged();
                }
            }
        }

        public double EgalitarianHappinessWeight
        {
            get => _egalitarianHappinessWeight;
            set
            {
                if(_egalitarianHappinessWeight != value)
                {
                    _egalitarianHappinessWeight = value;
                    OnPropertyChanged("EgalitarianHappinessWeight");
                    OnChanged();
                }
            }
        }

        public int Size
        {
            get => _size;
            set
            {
                if(_size != value)
                {
                    _size = value;
                    OnPropertyChanged("Size");
                    OnChanged();
                }
            }
        }

        public int Generations
        {
            get => _generations;
            set
            {
                if(_generations != value)
                {
                    _generations = value;
                    OnPropertyChanged("Generations");
                    OnChanged();
                }
            }
        }

        public AlgorithmOptionGenetic(string name, int index, IGeneticSettings settings) : base(name, index)
        {
            _selectionRate = settings.SelectionRate * 100;
            _absoluteSelection = settings.AbsoluteSelection * 100;
            _mutationChance = settings.MutationChance * 100;
            _stablePairWeight = settings.StablePairWeight;
            _groupHappinessWeight = settings.GroupHappinessWeight;
            _egalitarianHappinessWeight = settings.EgalitarianHappinessWeight;
            _size = settings.Size;
            _generations = settings.Generations;
        }

        public AlgorithmOptionGenetic AcceptGetGeneticOption(IAlgorithmOptionVisitor visitor)
        {
            return visitor.GetGeneticOption(this);
        }

        public void AcceptReduceIndex(IAlgorithmOptionVisitor visitor)
        {
            Index--;
        }
    }
}
