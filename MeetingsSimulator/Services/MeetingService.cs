using MeetingsSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsSimulator.Services
{
    public class MeetingService
    {
        private MainService service;
        public MeetingService()
        {
            service = new MainService();
        }

        public List<MeetingModel> Meetings() => service.Get<MeetingModel>(@"meetings.json");
        public void Create(MeetingModel meeting) => service.Create(meeting, @"meetings.json");
        public void Delete(int id) => service.Delete<MeetingModel>(GetIndex(id), @"meetings.json");
        public int GetIndex(int id) => Meetings().FindIndex(m => m.Id == id);

        public List<MeetingModel> GetByDescription(string description) => Meetings().Where(m => m.Description.Equals(description)).ToList();

        public List<MeetingModel> GetByResponsiblePerson(string responsiblePerson) => Meetings().Where(m => m.ResponsiblePerson.Name == responsiblePerson).ToList();

        public List<MeetingModel> GetByCategory(Category category) => Meetings().Where(m => m.Category.Equals(category)).ToList();

        public List<MeetingModel> GetByType(Models.Type type) => Meetings().Where(m => m.Type.Equals(type)).ToList();

        public List<MeetingModel> GetByDates(DateTime startDate, DateTime? endDate = null)
        {
            endDate = endDate ?? DateTime.MaxValue; 
            var meetingsByDates = Meetings().Where(m => m.StartDate >= startDate && m.EndDate < endDate).ToList();
            return meetingsByDates;
        }

        public List<MeetingModel> GetByAttendees(int attendeesNum) => Meetings().Where(m => m.Attendees.Count > attendeesNum).ToList();

        public void AddAttendee(DateTime meetingTime, UserModel user, MeetingModel meeting) => meeting.Attendees.Add((user, meetingTime));
        
        public void RemoveAttendee(UserModel user, int id)
        {
            MeetingModel meeting = FindById(id);
            if (user != meeting.ResponsiblePerson)
            {
                meeting.Attendees.RemoveAll(a => a.Item1 == user);
            }
            else
                Console.WriteLine("Access denied. You are not responsible to modify");
        }
        public MeetingModel FindById(int id)
        {
            MeetingModel meeting = Meetings().Where(m => m.Id == id).First();
            return meeting;
        }
    }
}
