using System;
using System.Collections.Generic;
using System.Text;
using Szakdolgozat.Model.Structures;

namespace Szakdolgozat.Model.Algorithm
{
    /// <summary>
    /// The Species is what the genetic algorithm produces and tries to solve the problem with
    /// </summary>
    /// <typeparam name="T">The type of fitness</typeparam>
    public class Species<T>
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
