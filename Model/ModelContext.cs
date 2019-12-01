using System.Collections.Generic;
using System.Linq;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.Persistence;

namespace Szakdolgozat.Model
{
    //Singleton
    public sealed class ModelContext : IModelContext
    {
        //Hidden by interface
        public IPersistence Persistence { get; set; }

        public StableMarriage StableMarriage { get; set; }

        public bool SetupChanged { get; set; }

        public bool ParticipantsChanged { get; set; }

        public bool PreferencesChanged { get; set; }

        public bool AlgorithmsChanged { get; set; }

        //Visible from interface
        public List<AlgorithmData> Algorithms { get; set; }

        public Priorities Priorities { get; set; }

        public List<Participant> Participants { get; set; }

        public IEnumerable<Participant> Group1Participants { get => Participants.Where(x => x.Group == MarriageGroup.Group1); }

        public IEnumerable<Participant> Group2Participants { get => Participants.Where(x => x.Group == MarriageGroup.Group2); }

        public string Group1Name { get; set; }

        public string Group2Name { get; set; }

        public int TotalSize { get; set; }

        public int GroupSize { get => TotalSize / 2; }
    }
}
