using System;
using System.Collections.Generic;
using System.Text;
using Szakdolgozat.Common;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.Persistence;

namespace Szakdolgozat.Model
{
    public interface IModelContext : IContext
    {
        PersistenceBase Persistence { get; set; }

        StableMarriage StableMarriage { get; set; }

        List<AlgorithmData> Algorithms { get; set; }

        Priorities Priorities { get; set; }

        List<Participant> Participants { get; set; }

        new bool SetupChanged { get; set; }

        new bool ParticipantsChanged { get; set; }

        new bool PreferencesChanged { get; set; }

        new bool AlgorithmsChanged { get; set; }

        new string Group1Name { get; set; }

        new string Group2Name { get; set; }

        new int TotalSize { get; set; }
    }
}
