using System;
using System.Runtime.Serialization;

namespace Szakdolgozat.Persistence
{
    [Serializable]
    internal class PersistenceException : Exception
    {
        public PersistenceException()
        {
        }

        public PersistenceException(string message) : base(message)
        {
        }
    }
}