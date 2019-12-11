using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdolgozat.ViewModel.Controls
{
    public class ParticipantRow
    {
        private string _name;

        public ParticipantRow(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if(value != _name)
                {
                    _name = value;
                    NameChanged?.Invoke(this, null);
                }
            }
        }

        public event EventHandler NameChanged;
    }
}
