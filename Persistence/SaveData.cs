using System;
using System.Collections.Generic;
using Szakdolgozat.Persistence.Structures;

namespace Szakdolgozat.Persistence
{
    [Serializable]
    public class SaveData
    {
        public List<AlgorithmSaveBase> Algorithms;

        public List<PreferenceSave> Preferences;

        public List<UnitSave> Participants;

        public string Group1Name;

        public string Group2Name;
    }
}