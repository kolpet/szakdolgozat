namespace Szakdolgozat.Model.Structures
{
    /// <summary>
    /// Settings for the genetic algorithm
    /// </summary>
    public class GeneticSettings : IGeneticSettings
    {
        private double _absoluteSelection;

        private double _selectionRate;

        private double _mutationChance;

        private int _size;

        private int _generations;
        
        public double AbsoluteSelection {
            set {
                if (value >= 0 && value <= 1 && value + _selectionRate <= 1)
                {
                    _absoluteSelection = value;
                }
            }
            get {
                return _absoluteSelection;
            }
        }

        public double SelectionRate {
            set {
                if (value >= 0 && value <= 1 && value + _absoluteSelection <= 1)
                {
                    _selectionRate = value;
                }
            }
            get {
                return _selectionRate;
            }
        }

        public double MutationChance {
            set {
                if (value >= 0 && value <= 1)
                {
                    _mutationChance = value;
                }
            }
            get {
                return _mutationChance;
            }
        }

        public double StablePairWeight { get; set; }

        public double GroupHappinessWeight { get; set; }

        public double EgalitarianHappinessWeight { get; set; }

        public int Size {
            get => _size;
            set {
                if (value > 0)
                {
                    _size = value;
                }
            }
        }

        public int Generations {
            get => _generations;
            set {
                if (value > 0)
                {
                    _generations = value;
                }
            }
        }
    }
}
