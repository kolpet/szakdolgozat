using Szakdolgozat.Common;

namespace Szakdolgozat.Model.Structures
{
    public class Participant : IParticipant
    {
        public Participant(int id, string name, MarriageGroup group)
        {
            ID = id;
            Name = name;
            Group = group;
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public MarriageGroup Group { get; set; }
    }
}