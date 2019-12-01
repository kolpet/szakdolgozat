using System;
using System.Collections.Generic;
using System.Linq;
using Szakdolgozat.Model.Structures;

namespace Szakdolgozat.Model
{
    public class PreferencesModel : ModelBase
    {
        public PreferencesModel()
        {
            Context.Priorities = new Priorities(new Dictionary<int, UnitSet>());
            Context.PreferencesChanged = true;

            Initialize();
        }

        public void Initialize()
        {
            if(Context.ParticipantsChanged)
            {
                Context.Priorities.Clear();
                foreach(Participant participant in Context.Group1Participants)
                {
                    Context.Priorities[participant.ID] = new UnitSet(Context.Group2Participants.Select(x => x.ID).ToList());
                }
                foreach(Participant participant in Context.Group2Participants)
                {
                    Context.Priorities[participant.ID] = new UnitSet(Context.Group1Participants.Select(x => x.ID).ToList());
                }
                Context.ParticipantsChanged = false;
            }
        }

        public void Validate()
        {
            foreach(KeyValuePair<int, UnitSet> option in Context.Priorities)
            {
                if(option.Value.Distinct().Count() != Context.GroupSize)
                {
                    OnModelError("A preferencia listában nem szerepelhet egy résztvevő többször!");
                }
            }
        }

        public void EditPreference(int id, int pos, int value)
        {
            Context.Priorities[id][pos] = value;
            Context.PreferencesChanged = true;
        }

        public void Randomize()
        {
            //TODO: Clean this up
            Random random = new Random();
            foreach(int key in Context.Priorities.Keys.ToList())
            {
                Context.Priorities[key] = new UnitSet(Context.Priorities[key].OrderBy(x => random.Next()));
            }
            Context.PreferencesChanged = true;
        }
    }
}
