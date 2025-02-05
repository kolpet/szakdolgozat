﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Model.Structures;

namespace Szakdolgozat.Model.Evaluation
{
    /// <summary>
    /// A class to calculate the egalitarian group happiness cost of a solution 
    /// </summary>
    public class EgalitarianHappinessEvaluation : EvaluationBase<double>
    {
        /// <summary>
        /// Returns the egalitarian group happiness cost of the solution
        /// </summary>
        /// <param name="stableMarriage">The stable marriage</param>
        /// <param name="solution">The solution</param>
        /// <returns>The egalitarian group happiness cost</returns>
        protected override double EvaluateMethod(StableMarriage stableMarriage, Solution solution)
        {
            double happiness = 0;

            foreach (Tuple<int, int> pair in solution)
            {
                happiness += stableMarriage.Priorities[pair.Item1].FindIndex(x => x == pair.Item2) -
                             stableMarriage.Priorities[pair.Item2].FindIndex(x => x == pair.Item1);
            }

            return Math.Abs(happiness / stableMarriage.GroupSize);
        }
    }
}
