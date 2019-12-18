using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Model.Structures;

namespace Szakdolgozat.Model.Evaluation
{
    /// <summary>
    /// A class to evaluate a solution with a set method
    /// </summary>
    /// <typeparam name="Score">The type of the evaluation's result</typeparam>
    public abstract class EvaluationBase<Score>
    {
        /// <summary>
        /// Evaluates the solution with the set method
        /// </summary>
        /// <param name="stableMarriage">The stable marriage</param>
        /// <param name="solution">The solution</param>
        /// <returns>The result</returns>
        public Score Evaluate(StableMarriage stableMarriage, Solution solution)
        {
            return EvaluateMethod(stableMarriage, solution);
        }

        /// <summary>
        /// The method for evaluating the solution
        /// </summary>
        /// <param name="stableMarriage">The stable marriage</param>
        /// <param name="solution">The solution</param>
        /// <returns>The result</returns>
        protected abstract Score EvaluateMethod(StableMarriage stableMarriage, Solution solution);
    }
}
