using MeetingsSimulator.Models;
using MeetingsSimulator.Services;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsSimulator.Managers
{
    public class MeetingManager
    {
        private MeetingService service;
        public MeetingManager()
        {
            service = new MeetingService();
        }
        public void CreateMeeting()
        {
            List<string> variables = GetInputsByClassFields();
            
            var name = variables.ElementAt(0);
            var responsiblePerson = new UserModel(variables.ElementAt(1));
            var description = variables.ElementAt(2);
            var category = ToEnum(variables.ElementAt(3), Category.Short);
            var type = ToEnum(variables.ElementAt(4), Models.Type.InPerson);

            string[] startTimeParameters = variables[5].Split(' ');
            var startDate = ConvertToDateTime(startTimeParameters);

            string[] endTimeParameters = variables[6].Split(' ');
            var endDate = ConvertToDateTime(endTimeParameters);

            MeetingModel meeting = new MeetingModel(name, responsiblePerson, description, category, type, startDate, endDate);
            service.Create(meeting);

        }

        public void DeleteMeeting(string logedInUser, string id)
        {
            MeetingModel meeting = service.FindById(Int32.Parse(id));
            if(meeting != null && meeting.ResponsiblePerson.Name == logedInUser)
            {
                service.Delete(meeting.Id);
            }
        }

        public void AddAttendee(string user, string id, string time)
        {
            MeetingModel meeting = service.FindById(Int32.Parse(id));

            bool containsAttendee = meeting.Attendees.Any(a => a.Item1.Name == user);

            if (!containsAttendee)
            {
                service.AddAttendee(ConvertToDateTime(time.Split(' ')), new UserModel(user), meeting);
            }
            else
                Console.WriteLine("User is already added");
        }

        public void RemoveAttendee(string removableUser, string id)
        {
            MeetingModel meeting = service.FindById(Int32.Parse(id));
            if (meeting != null && meeting.ResponsiblePerson.Name != removableUser)
            {
                service.RemoveAttendee(new UserModel(removableUser), meeting.Id);
            }
        }

        public string Meetings() => ToString(service.Meetings());
        public string GetByDescription(string description) => ToString(service.GetByDescription(description));

        public string GetByResponsiblePerson(string responsiblePerson) => ToString(service.GetByResponsiblePerson(responsiblePerson));

        public string GetByCategory(string category) => ToString(service.GetByCategory(ToEnum(category, Category.Short)));

        public string GetByType(string type) => ToString(service.GetByType(ToEnum(type, Models.Type.InPerson)));

        public string GetByDates(string startDate, string endDate = null)
        {
            if(endDate != null)
                return ToString(service.GetByDates(ConvertToDateTime(startDate.Split(' ')), ConvertToDateTime(endDate.Split(' '))));
            else
                return ToString(service.GetByDates(ConvertToDateTime(startDate.Split(' '))));
        }
        

        public string GetByAttendees(string attendeesNum) => ToString(service.GetByAttendees(Int32.Parse(attendeesNum)));

        private DateTime ConvertToDateTime(string[] parameters)
        {
            var date = new DateTime(Int32.Parse(parameters[0]), Int32.Parse(parameters[1]), Int32.Parse(parameters[2]), Int32.Parse(parameters[3]), Int32.Parse(parameters[4]), 0);
            return date;
        }

        private List<string> GetInputsByClassFields()
        {
            
            PropertyInfo[] property_infos = typeof(MeetingModel).GetProperties(BindingFlags.Instance |
                       BindingFlags.Static |
                       BindingFlags.NonPublic |
                       BindingFlags.Public);


            List<string> variables = new List<string>();

            foreach (PropertyInfo field in property_infos.Skip(1).SkipLast(1))
            {
                var variable = GetUserInput(field);
                variables.Add(variable);
            }
            
            return variables;
        }
        private string GetUserInput(PropertyInfo property)
        {
            Console.WriteLine($"Please, enter meeting {property.Name}: ");
            var userInput = Console.ReadLine();
            return userInput;
        }

        public static TEnum ToEnum<TEnum>(string value, TEnum defaultValue) where TEnum : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            TEnum result;
            return Enum.TryParse(value, true, out result) ? result : defaultValue;
        }

        private string ToString(List<MeetingModel> meetings)
        {
            if (!meetings.Any())
                return "";

            var meetingsSb = new StringBuilder();
            
            foreach (MeetingModel meeting in meetings)
            {
                meetingsSb.Append(meeting.ToString());
            }
            return meetingsSb.ToString();
            
        }
    }
}
