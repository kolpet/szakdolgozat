using System;
using System.Collections.Generic;
using System.Linq;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.Persistence;

namespace Szakdolgozat.Model
{
    public class ParticipantsModel : ModelBase
    {
        public ParticipantsModel()
        {
            Context.Participants = new List<Participant>();
            Context.ParticipantsChanged = true;

            Initialize();
        }

        public void Initialize()
        {
            if(Context.SetupChanged)
            {
                Context.Participants.Clear();
                for(int i = 0; i < Context.GroupSize; i++)
                {
                    Context.Participants.Add(new Participant(i, "", MarriageGroup.Group1));
                    Context.Participants.Add(new Participant(i + Context.GroupSize, "", MarriageGroup.Group2));
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

        public void SaveParticipants(ref SaveData data)
        {

        }

        public void LoadParticipants(SaveData data)
        {
            var idList = Context.Participants.Select(x => x.ID);
            if(idList.Distinct().Count() != idList.Count())
            {
               
            }
        }
    }
}
