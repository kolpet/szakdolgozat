using System;
using System.Collections.Generic;
using System.Text;
using Szakdolgozat.Model.Structures;

namespace Szakdolgozat.Model
{
    public class ResultModel : ModelBase
    {
        private int _index;

        private Priorities Priorities { get => Context.Algorithms[_index].Algorithm.StableMarriage.Priorities; }

        public Solution Solution { get => Context.Algorithms[_index].Algorithm.Solution; }

        public ResultModel(int index)
        {
            _index = index;
        }

        public bool IsStablePair(Tuple<int, int> pair)
        {
            int p1 = pair.Item1;
            int p2 = pair.Item2;

            //Iterate through p1's more prefered candidates
            for(int posX = 0; posX < Priorities[p1].FindIndex(x => x == p2); posX++)
            {
                //More prefered by p1
                int pX = Priorities[p1][posX];
                //X's current pair
                int pY = Solution.GetPair(pX);
                //check if X prefers p1 more than Y
                if(Priorities[pX].FindIndex(x => x == p1) < Priorities[pX].FindIndex(x => x == pY))
                {
                    return false;
                }
            }
            //Iterate through p2's more prefered candidates
            for(int posX = 0; posX < Priorities[p2].FindIndex(x => x == p1); posX++)
            {
                //More prefered by p2
                int pX = Priorities[p2][posX];
                //X's current pair
                int pY = Solution.GetPair(pX);
                //check if X prefers p2 more than Y
                if(Priorities[pX].FindIndex(x => x == p2) < Priorities[pX].FindIndex(x => x == pY))
                {
                    return false;
                }
            }

            return true;
        }

        public double GetGroupHappiness(Tuple<int, int> pair)
        {
            return Priorities[pair.Item1].FindIndex(x => x == pair.Item2) +
                   Priorities[pair.Item2].FindIndex(x => x == pair.Item1) + 2;
        }

        public double GetEgalitarianGroupHappiness(Tuple<int, int> pair)
        {
            return Priorities[pair.Item1].FindIndex(x => x == pair.Item2) -
                   Priorities[pair.Item2].FindIndex(x => x == pair.Item1);
        }
    }
}
