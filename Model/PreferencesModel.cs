using System;
using System.Collections.Generic;
using System.Linq;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.Persistence;
using Szakdolgozat.Persistence.Structures;

namespace Szakdolgozat.Model
{
    public class PreferencesModel : ModelBase
    {
        public PreferencesModel(IModelContext context) : base(context)
        {
            Context.Priorities = new Priorities(new Dictionary<int, UnitSet>());
        }

        public void Initialize()
        {
            Context.PreferencesChanged = true;
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
            Random random = new Random();
            foreach(int key in Context.Priorities.Keys.ToList())
            {
                Context.Priorities[key] = new UnitSet(Context.Priorities[key].OrderBy(x => random.Next()));
            }
            Context.PreferencesChanged = true;
        }
        
        public void Load()
        {
            Context.PreferencesChanged = true;
            Context.Priorities.Clear();
            foreach(PreferenceSave preference in Context.Persistence.Data.Preferences)
            {
                Context.Priorities[preference.Id] = new UnitSet(preference.Preferences);
            }
            Context.ParticipantsChanged = false;
        }
    }
}
