using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Model.Structures
{
    /// <summary>
    /// Holds a complete setup for a StableMarriage
    /// </summary>
    public class StableMarriage
    {
        private UnitSet _units1;
        private UnitSet _units2;
        private Priorities _priorities;

        /// <summary>
        /// Creates a stable marriage
        /// </summary>
        /// <param name="units1">First group of participants</param>
        /// <param name="units2">Second group of participants</param>
        /// <param name="priorities">Priorities of all participants</param>
        public StableMarriage(UnitSet units1, UnitSet units2, Priorities priorities)
        {
            _units1 = units1;
            _units2 = units2;
            _priorities = priorities;

            Check();
        }

        /// <summary>
        /// Check for errors in structure
        /// </summary>
        private void Check()
        {
            //Sets
            if (_units1.Count() == 0 || _units2.Count() == 0)
            {
                throw new ModelStructureException("The groups can't be empty!");
            }
            if (_units1.Count() != _units2.Count())
            {
                throw new ModelStructureException("The groups can't be different sizes!");
            }
            if (_units1.Intersect(_units2).Count() > 0)
            {
                throw new ModelStructureException("The groups can't intersect!");
            }

            //Priorities
            foreach (KeyValuePair<int, UnitSet> pair in _priorities)
            {
                if ((_units1.Contains(pair.Key) && Units2.Intersect(pair.Value).Count() != GroupSize) ||
                    (Units2.Contains(pair.Key) && _units1.Intersect(pair.Value).Count() != GroupSize))
                {
                    throw new ModelStructureException("Priorities need to contain everyone once from the other group!");
                }
            }
        }

        /// <summary>
        /// Get the first group of participants
        /// </summary>
        public UnitSet Units1 { get => _units1; private set => _units1 = value; }

        /// <summary>
        /// Get the second group of participants
        /// </summary>
        public UnitSet Units2 { get => _units2; private set => _units2 = value; }

        /// <summary>
        /// Get the priorities of participants
        /// </summary>
        public Priorities Priorities { get => _priorities; private set => _priorities = value; }

        /// <summary>
        /// Get the total amount of the participants
        /// </summary>
        public int TotalSize { get { return _units1.Count() + _units2.Count(); } }

        /// <summary>
        /// Get the size of the groups of participants
        /// </summary>
        public int GroupSize { get { return _units1.Count(); } }
    }
}
