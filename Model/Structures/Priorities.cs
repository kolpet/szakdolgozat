using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Model.Structures
{
    /// <summary>
    /// Contains a set of priorities for a set of participants. Equivalent to Dictionary<int, ParticipantSet>
    /// </summary>
    public class Priorities : Dictionary<int, UnitSet>
    {
        /// <summary>
        /// Create a new set of priorities
        /// </summary>
        /// <param name="dictionary">The dictionary of priorities</param>
        public Priorities(IDictionary<int, UnitSet> dictionary) : base(dictionary)
        {
        }

        /// <summary>
        /// Check for errors in structure
        /// </summary>
        private void Check()
        { 
            foreach (KeyValuePair<int, UnitSet> pair in this)
            {
                if (pair.Value.Contains(pair.Key))
                {
                    throw new ModelStructureException("A participant can't have itself in its priorities");
                }
                if (pair.Value.GroupBy(x => x).Any(x => x.Count() > 1))
                {
                    throw new ModelStructureException("A priority list can't have duplicates!");
                }
            }
        }
    }
}
