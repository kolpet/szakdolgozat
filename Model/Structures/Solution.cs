using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Model.Structures
{
    /// <summary>
    /// Contains the solution to a stable marriage. Equivalent to List<Tuple<int, int>>
    /// </summary>
    public class Solution : List<Tuple<int, int>>
    {
        /// <summary>
        /// Create a new empty solution
        /// </summary>
        public Solution() : base() { }

        public Solution(List<Tuple<int, int>> container) : base(container) { }

        /// <summary>
        /// Get a pair of a stored participant
        /// </summary>
        /// <param name="key">Id of participant</param>
        /// <returns>Pair of participant</returns>
        public int GetPair(int key)
        {
            foreach (Tuple<int, int> pair in this)
            {
                if (pair.Item1 == key)
                {
                    return pair.Item2;
                }
                if (pair.Item2 == key)
                {
                    return pair.Item1;
                }
            }

            throw new KeyNotFoundException();
        }
    }
}
