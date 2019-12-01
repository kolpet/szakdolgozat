using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szakdolgozat.Model.Structures;

namespace Szakdolgozat.Model.Algorithm
{
    public abstract class GeneticAlgorithmBase<T> : AlgorithmBase
    {
        /// <summary>
        /// The population of species
        /// </summary>
        protected List<Species> _population;

        /// <summary>
        /// A random number generator
        /// </summary>
        protected Random _random;

        /// <summary>
        /// The settings of the genetic algorithm
        /// </summary>
        public GeneticSettings Settings { get;  set; }

        /// <summary>
        /// Creates a new genetic algorithm to solve a stable marriage problem.
        /// </summary>
        /// <param name="stableMarriage">The stable marriage to be solved</param>
        /// <param name="settings">The settings of the genetic algorithm</param>
        public GeneticAlgorithmBase(StableMarriage stableMarriage, GeneticSettings settings) : base(stableMarriage)
        {
            Settings = settings;
            _random = new Random();
        }

        /// <summary>
        /// Solves the stable marriage problem with genetic algorithm
        /// </summary>
        protected override void CalculateMethod()
        {
            Initialization();

            for(int i = 0; i < Settings.Generations && Condition(); i++)
            {
                Selection();
                CrossoverAndMutate();
            }

            _solution = _population[0].Genes;
        }

        /// <summary>
        /// Initializes the population of Species
        /// </summary>
        /// <returns>async Task</returns>
        private void Initialization()
        {
            _population = new List<Species>();
            for(int i = 0; i < Settings.Size; i++)
            {
                UnitSet participants1 = new UnitSet(_stableMarriage.Units1.OrderBy(x => _random.Next()));
                UnitSet participants2 = new UnitSet(_stableMarriage.Units2.OrderBy(x => _random.Next()));

                //Randomized solution
                Solution genes = new Solution(participants1.Zip(participants2, (x, y) => new Tuple<int, int>(x, y)).ToList());
                T fitness = CalculateFitness(genes);

                _population.Add(new Species()
                {
                    Genes = genes,
                    Fitness = fitness
                });
            }

            _population.OrderByDescending(x => x.Fitness);
        }

        /// <summary>
        /// Condition for how long should
        /// </summary>
        /// <returns></returns>
        protected abstract bool Condition();

        /// <summary>
        /// Through random selection, deletes a set amount of Species, favoring more fit species
        /// </summary>
        protected abstract void Selection();

        /// <summary>
        /// Repopulates the population by crossing over surviving species and mutates them by chance
        /// </summary>
        protected abstract void CrossoverAndMutate();

        /// <summary>
        /// Calculates the fitness of a solution depending on settings
        /// </summary>
        /// <typeparam name="T">Type of fitness</typeparam>
        /// <param name="solution">The genes</param>
        /// <returns>The fitness</returns>
        protected abstract T CalculateFitness(Solution solution);

        /// <summary>                     
        /// The Species is what the genetic algorithm produces and tries to solve the problem with
        /// </summary>
        protected class Species
        {
            /// <summary>
            /// The "genes" are the result solution of the genetic algorithm
            /// </summary>
            public Solution Genes { get; set; }

            /// <summary>
            /// The fitness of the genes
            /// </summary>
            public T Fitness { get; set; }
        }
    }
}
