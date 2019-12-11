using System;
using System.Collections.Generic;
using System.Linq;
using Szakdolgozat.Common;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.Persistence;
using Szakdolgozat.Persistence.Structures;

namespace Szakdolgozat.Model
{
    public class ParticipantsModel : ModelBase
    {
        public ParticipantsModel()
        {
            Context.Participants = new List<Participant>();
        }

        public void Initialize()
        {
            if(Context.SetupChanged)
            {
                Context.ParticipantsChanged = true;
                Context.Participants.Clear();
                int i = 0;
                for(; i < Context.GroupSize; i++)
                {
                    Context.Participants.Add(new Participant(i, "", MarriageGroup.Group1));
                }
                for(; i < Context.TotalSize; i++)
                {
                    Context.Participants.Add(new Participant(i, "", MarriageGroup.Group2));
                }
                Context.SetupChanged = false;
            }
        }

        public void Validate()
        {
            if(Context.Group1Participants.Count() != Context.Group2Participants.Count())
            {
                OnModelError("A két csoport létszáma meg kell, hogy egyezzen!");
            }
        }

        public void EditParticipant(int id, string name)
        {
            Context.Participants.Single(x => x.ID == id).Name = name;
            Context.ParticipantsChanged = true;
        }

        public void Load()
        {
            Context.ParticipantsChanged = true;
            Context.Participants.Clear();
            foreach(UnitSave unit in Context.Persistence.Data.Participants)
            {
                Context.Participants.Add(new Participant(unit.Id, unit.Name, unit.Group));
            }
            Context.SetupChanged = false;
        }
    }
}
