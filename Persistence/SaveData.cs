using System;
using System.Collections.Generic;
using Szakdolgozat.Persistence.Structures;

namespace Szakdolgozat.Persistence
{
    [Serializable]
    public class SaveData
    {
        public List<AlgorithmSaveBase> Algorithms;

        public Dictionary<int, List<int>> Preferences;

        public List<UnitSave> Participants;

        public string Group1Name;

        public string Group2Name;
    }
}