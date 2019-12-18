using System.Collections.Generic;
using System.Linq;
using Szakdolgozat.Common;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.Persistence;

namespace Szakdolgozat.Model
{
    //Singleton
    public sealed class ModelContext : IModelContext
    {
        private bool _participantsChanged;

        private bool _preferencesChanged;

        private bool _algorithmsChanged;

        //Hidden by IContext
        public PersistenceBase Persistence { get; set; }

        public StableMarriage StableMarriage { get; set; }

        public List<AlgorithmData> Algorithms { get; set; }

        public Priorities Priorities { get; set; }

        public List<Participant> Participants { get; set; }

        //Visible from all interfaces
        public bool SetupChanged { get; set; }

        public bool ParticipantsChanged { get => _participantsChanged || SetupChanged; set => _participantsChanged = value; }

        public bool PreferencesChanged { get => _preferencesChanged || ParticipantsChanged; set => _preferencesChanged = value; }

        public bool AlgorithmsChanged { get => _algorithmsChanged || PreferencesChanged; set => _algorithmsChanged = value; }

        public IList<IAlgorithmData> GetAlgorithms { get => Algorithms.Select<AlgorithmData, IAlgorithmData>(x => x).ToList(); }

        public IPriorities GetPriorities { get => Priorities; }

        public IList<IParticipant> GetParticipants { get => Participants.Select<Participant, IParticipant>(x => x).ToList(); }

        public IEnumerable<IParticipant> Group1Participants { get => GetParticipants.Where(x => x.Group == MarriageGroup.Group1); }

        public IEnumerable<IParticipant> Group2Participants { get => GetParticipants.Where(x => x.Group == MarriageGroup.Group2); }

        public string Group1Name { get; set; }

        public string Group2Name { get; set; }

        public int TotalSize { get; set; }

        public int GroupSize { get => TotalSize / 2; }
    }
}
