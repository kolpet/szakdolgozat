using System;

namespace Szakdolgozat.Model.Events
{
    public class AlgorithmEventArgs : EventArgs
    {
        public int Index { get; private set; }

        public int StablePairs { get; private set; }

        public double GroupHappiness { get; private set; }

        public double EgalitarianHappiness { get; private set; }

        public AlgorithmEventArgs(int index) : this(index, 0, 0, 0) { }

        public AlgorithmEventArgs(int index, int stablePairs, double groupHappiness, double egalitarianHappiness)
        {
            Index = index;
            StablePairs = stablePairs;
            GroupHappiness = groupHappiness;
            EgalitarianHappiness = egalitarianHappiness;
        }
    }
}
