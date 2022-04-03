using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsSimulator.Models
{
    public enum Category { CodeMonkey, Hub, Short, TeamBuilding }
    public enum Type { Live, InPerson }
    public class MeetingModel
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public UserModel ResponsiblePerson { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public Type Type { get; set; }
        public DateTime StartDate {  get; set; }
        public DateTime EndDate { get; set; }

        public List<(UserModel, DateTime)> Attendees {  get; set; }

        static int NextId = 0;

        public MeetingModel(string Name, UserModel ResponsiblePerson, string Description, Category Category, Type Type, DateTime StartDate, DateTime EndDate)
        {
            Id = NextId++;
            this.Name = Name;
            this.ResponsiblePerson = ResponsiblePerson;
            this.Description = Description;
            this.Category = Category;
            this.Type = Type;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            Attendees = new List<(UserModel, DateTime)>();
            Attendees.Add((ResponsiblePerson, StartDate));
        }

        public override string ToString()
        {
            return "Name: " + Name + "\n" +
                "Responsible person: " + ResponsiblePerson.ToString() + "\n" +
                "Description: " + Description + "\n" +
                "Category: " + Category + "\n" +
                "Type: " + Type + "\n" +
                "StartDate: " + StartDate + "\n" +
                "EndDate: " + EndDate + "\n" +
                "Attendees: " + string.Join(",", Attendees) + "\n";
        }
    }
}
