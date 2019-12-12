using System.Collections.Generic;

namespace Szakdolgozat.Common
{
    public interface IContext
    {
        bool SetupChanged { get; }

        bool ParticipantsChanged { get; }

        bool PreferencesChanged { get; }

        bool AlgorithmsChanged { get; }

        IList<IAlgorithmData> GetAlgorithms { get; }

        IPriorities GetPriorities { get; }

        IList<IParticipant> GetParticipants { get; }

        IEnumerable<IParticipant> Group1Participants { get; }

        IEnumerable<IParticipant> Group2Participants { get; }

        string Group1Name { get; }

        string Group2Name { get; }

        int TotalSize { get; }

        int GroupSize { get; }
    }
}