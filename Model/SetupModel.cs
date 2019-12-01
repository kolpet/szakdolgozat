using Szakdolgozat.Persistence;

namespace Szakdolgozat.Model
{
    public class SetupModel : ModelBase
    {
        public bool IsValid { get; private set; }

        public SetupModel()
        {
            Context.SetupChanged = true;
        }

        public void Initialize()
        {
            Context.Group1Name = "Férfi";
            Context.Group2Name = "Nő";
            Context.TotalSize = 10;

            IsValid = true;
        }

        public void ChangeGroup1Name(string name)
        {
            IsValid = false;
            if(name == "")
            {
                OnModelError("A csoport neve nem lehet üres!");
            }
            if(name == Context.Group2Name)
            {
                OnModelError("A csoport nevek nem egyezhetnek meg!");
            }
            IsValid = true;
            Context.Group1Name = name;
            Context.SetupChanged = true;
        }

        public void ChangeGroup2Name(string name)
        {
            IsValid = false;
            if(name == "")
            {
                OnModelError("A csoport neve nem lehet üres!");
            }
            if(name == Context.Group1Name)
            {
                OnModelError("A csoport nevek nem egyezhetnek meg!");
            }
            IsValid = true;
            Context.Group2Name = name;
            Context.SetupChanged = true;
        }

        public void ChangeParticipantNumber(int number)
        {
            IsValid = false;
            if(number < 2)
            {
                OnModelError("Legalább 2 résztvevőnek lennie kell!");
            }
            if(number % 2 != 0)
            {
                OnModelError("A résztvevők száma páros kell, hogy legyen!");
            }
            IsValid = true;
            Context.TotalSize = number;
            Context.SetupChanged = true;
        }

        public void Save(ref SaveData data)
        {

        }

        public void Load(SaveData data)
        {

        }
    }
}
