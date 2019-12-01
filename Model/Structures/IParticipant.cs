using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdolgozat.Model.Structures
{
    public interface IParticipant
    {
        int ID { get; }

        string Name { get; }

        MarriageGroup Group { get; }
    }
}
