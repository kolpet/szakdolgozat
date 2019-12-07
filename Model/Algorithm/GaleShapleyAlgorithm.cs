using System;
using System.Collections.Generic;
using System.Linq;
using Szakdolgozat.Model.Structures;

namespace Szakdolgozat.Model.Algorithm
{
    public class GaleShapleyAlgorithm : AlgorithmBase, IAlgorithmElement
    {
        /// <summary>
        /// Participants who are still not assigned
        /// </summary>
        private Queue<int> _waiting;

        /// <summary>
        /// Iterators for each participant's priority list
        /// </summary>
        private Dictionary<int, IEnumerator<int>> _iterators;

        /// <summary>
        /// Each participant's currently assigned pair
        /// </summary>
        private Dictionary<int, int?> _stored;

        /// <summary>
        /// Creates a new Gale-Shapley algorithm to solve a stable marriage problem
        /// </summary>
        /// <param name="stableMarriage">The stable marriage to be solved</param>
        public GaleShapleyAlgorithm(StableMarriage stableMarriage) : base(stableMarriage)
        {

        }
        
        /// <summary>
        /// Solves the stable marriage problem with the Gale-Shapley algorithm
        /// </summary>
        protected override void CalculateMethod()
        {
            Setup();
            Algorithm();

            _solution = new Solution();
            foreach(KeyValuePair<int, int?> pair in _stored)
            {
                _solution.Add(new Tuple<int, int>(pair.Key, pair.Value ?? -1));
            }
        }

        /// <summary>
        /// Setup for the algorithm
        /// </summary>
        private void Setup()
        {
            _waiting = new Queue<int>();
            _iterators = new Dictionary<int, IEnumerator<int>>();
            _stored = new Dictionary<int, int?>();

            //Fill waiting list with first group
            foreach (int participant in _stableMarriage.Units1)
            {
                _waiting.Enqueue(participant);
                _iterators[participant] = _stableMarriage.Priorities[participant].GetEnumerator();
                _iterators[participant].MoveNext();
            }
            //Fill stored list with second group's pairs (none currently)
            foreach (int participant in _stableMarriage.Units2)
            {
                _stored[participant] = null;
            }
        }

        /// <summary>
        /// The Gale-Shapley algorithm modified for programming
        /// </summary>
        private void Algorithm()
        {
            //Currently tested participant
            int candidate;
            //To be tested with candidate participant
            int target;
            //Current pair of target
            int current;
            while (_waiting.Count > 0)
            {
                candidate = _waiting.Dequeue();
                target = _iterators[candidate].Current;

                if (_stored[target] == null) //Noone claimed target yet
                {
                    _stored[target] = candidate;
                }
                else
                {
                    current = _stored[target] ?? -1; //Cannot be null
                    if (_stableMarriage.Priorities[target].IndexOf(candidate) < _stableMarriage.Priorities[target].IndexOf(current)) //Candidate is more preferred than current
                    {
                        _stored[target] = candidate;
                        _iterators[current].MoveNext();
                        _waiting.Enqueue(current);
                    }
                    else
                    {
                        _iterators[candidate].MoveNext();
                        _waiting.Enqueue(candidate);
                    }
                }
            }
        }

        public void Accept(IAlgorithmVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
