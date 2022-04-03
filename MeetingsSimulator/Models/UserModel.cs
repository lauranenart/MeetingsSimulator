using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsSimulator.Models
{
    public class UserModel
    {
        public int Id { get; private set; }
        public string Name { get; set; }

        static int nextId = 0;

        public UserModel(string Name)
        {
            Id = nextId++;
            this.Name = Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
