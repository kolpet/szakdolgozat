using System.Collections.Generic;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.Persistence;

namespace Szakdolgozat.Model
{
    public interface IModelContext
    {
        bool SetupChanged { get; }

        bool ParticipantsChanged { get; }

        bool PreferencesChanged { get; }

        bool AlgorithmsChanged { get; }

        List<AlgorithmData> Algorithms { get; }

        Priorities Priorities { get;}

        List<Participant> Participants { get;}

        IEnumerable<Participant> Group1Participants { get; }

        IEnumerable<Participant> Group2Participants { get; }

        string Group1Name { get; }

        string Group2Name { get; }

        int TotalSize { get; }

        int GroupSize { get;  }
    }
}