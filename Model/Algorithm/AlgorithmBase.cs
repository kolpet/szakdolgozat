using Szakdolgozat.Model.Evaluation;
using Szakdolgozat.Model.Structures;

namespace Szakdolgozat.Model.Algorithm
{
    /// <summary>
    /// Interface for a Stable Marriage Algorithm class
    /// </summary>
    public abstract class AlgorithmBase
    {
        /// <summary>
        /// The stored stable marriage
        /// </summary>
        protected StableMarriage _stableMarriage;

        /// <summary>
        /// The solution
        /// </summary>
        protected Solution _solution;

        /// <summary>
        /// Creates an algorithm to solve the stable marriage problem
        /// </summary>
        /// <param name="stableMarriage">The stable marriage to be solved</param>
        public AlgorithmBase(StableMarriage stableMarriage)
        {
            _stableMarriage = stableMarriage;
        }

        /// <summary>
        /// Calculates the solution for a Stable Marriage
        /// </summary>
        public void Calculate() {
            if(_stableMarriage == null)
            {
                throw new AlgorithmException("Stable Marriage not defined");
            }

            CalculateMethod();
        }

        /// <summary>
        /// Calculation method for finding a Solution for a Stable Marriage
        /// Needs to be implemented by inheriting class
        /// </summary>
        protected abstract void CalculateMethod();

        /// <summary>
        /// Evaluates the solution found by the algorithm by a given evaluation method
        /// </summary>
        /// <typeparam name="Score">The type of scoring the evaluation uses</typeparam>
        /// <param name="evaluationMethod">The evaluation method used to score the solution</param>
        /// <returns>The score given by the evaluation method</returns>
        public Score Evaluate<Score>(IEvaluation<Score> evaluationMethod)
        {
            if(_solution == null)
            {
                throw new AlgorithmException("Stable Marriage not calculated yet");
            }

            return evaluationMethod.Evaluate(_stableMarriage, _solution);
        }

        /// <summary>
        /// Property of stored stable marriage
        /// </summary>
        public StableMarriage StableMarriage { get => _stableMarriage; private set => _stableMarriage = value; }

        /// <summary>
        /// Property of stored stable marriage
        /// </summary>
        public Solution Solution { get => _solution; private set => _solution = value; }
    }
}
