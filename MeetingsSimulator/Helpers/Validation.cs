using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MeetingsSimulator.Helpers
{
    public class Validation
    {
        public bool ValidateName(string name)
        {
            Regex nameRegex = new Regex("^[A-Za-z ]+$");
            return nameRegex.IsMatch(name) ? true : false;
        }
    }
}
