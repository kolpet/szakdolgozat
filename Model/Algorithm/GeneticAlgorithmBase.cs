using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szakdolgozat.Model.Structures;

namespace Szakdolgozat.Model.Algorithm
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">The type of the fitness</typeparam>
    public abstract class GeneticAlgorithmBase<T> : AlgorithmBase
    {
        /// <summary>
        /// The population of species
        /// </summary>
        protected List<Species<T>> _population;

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
        protected override sealed void CalculateMethod()
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
        protected abstract void Initialization();

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
    }
}
