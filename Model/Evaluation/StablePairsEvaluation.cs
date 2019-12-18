using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Model.Structures;

namespace Szakdolgozat.Model.Evaluation
{
    /// <summary>
    /// A class to calculate the amount of stable pairs in a solution
    /// </summary>
    public class StablePairsEvaluation : EvaluationBase<int>
    {
        /// <summary>
        /// Returns the amount of stable pairs in a solution
        /// </summary>
        /// <param name="stableMarriage">The stable marriage</param>
        /// <param name="solution">The solution</param>
        /// <returns>The amount of stable pairs</returns>
        protected override int EvaluateMethod(StableMarriage stableMarriage, Solution solution)
        {
            int correct = 0;

            for(int i = 0; i < stableMarriage.GroupSize; i++)
            {
                if(IsStablePair(stableMarriage.Priorities, solution, i))
                {
                    correct++;
                }
            }

            return correct;
        }
        
        /// <summary>
        /// Checks if a pair in the solution is a stable pair
        /// </summary>
        /// <param name="priorities">The priorities of the stable marriage</param>
        /// <param name="solution">The solution</param>
        /// <param name="pair">The pair</param>
        /// <returns>Conditional for the pair being stable in the solution</returns>
        private bool IsStablePair(Priorities priorities, Solution solution, int pair)
        {
            int p1 = solution[pair].Item1;
            int p2 = solution[pair].Item2;

            //Iterate through p1's more prefered candidates
            for(int posX = 0; posX < priorities[p1].FindIndex(x => x == p2); posX++)
            {
                //More prefered by p1
                int pX = priorities[p1][posX];
                //X's current pair
                int pY = solution.GetPair(pX);
                //check if X prefers p1 more than Y
                if(priorities[pX].FindIndex(x => x == p1) < priorities[pX].FindIndex(x => x == pY))
                {
                    return false;
                }
            }
            //Iterate through p2's more prefered candidates
            for (int posX = 0; posX < priorities[p2].FindIndex(x => x == p1); posX++)
            {
                //More prefered by p2
                int pX = priorities[p2][posX];
                //X's current pair
                int pY = solution.GetPair(pX);
                //check if X prefers p2 more than Y
                if (priorities[pX].FindIndex(x => x == p2) < priorities[pX].FindIndex(x => x == pY))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
