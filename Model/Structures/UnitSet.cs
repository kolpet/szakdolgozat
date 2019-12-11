using System.Collections.Generic;

namespace Szakdolgozat.Model.Structures
{
    /// <summary>
    /// Holds a set of the participants in a stable marriage. Equivalent of List<int>
    /// </summary>
    public class UnitSet : List<int>
    {
        /// <summary>
        /// Create a new set of participants from a list
        /// </summary>
        /// <param name="collection">List of participants</param>
        public UnitSet(IEnumerable<int> collection) : base(collection) { }

        public List<int> GetList() { return this; }
    }
}
