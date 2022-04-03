using MeetingsSimulator.Helpers;
using MeetingsSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsSimulator.Managers
{
    public class UserManager
    {
        private Validation validation;
        public UserModel user;

        public UserManager()
        {
            validation = new Validation();
        }
        public UserModel GetUser()
        {
            if(user == null)
                user = new UserModel(GetInput());
            return user;
        }

        public void StartApplication()
        {

            MeetingManager meetingManager = new MeetingManager();
            var toContinue = true;

            while (toContinue)
            {
                Console.WriteLine(GetMethodsList());
                var userInput = Console.ReadLine();

                if (userInput.Equals("Exit"))
                    break;

                System.Type thisType = meetingManager.GetType();
                MethodInfo theMethod = thisType.GetMethod(userInput);

                var output = theMethod.Invoke(meetingManager, GetParametersByMethod(theMethod));

                if (theMethod.ReturnType.Equals(typeof(System.String)))
                    Console.WriteLine(output);
                
            }
        }

        private object[] GetParametersByMethod(MethodInfo theMethod)
        {
            if (theMethod.GetParameters().Count() == 0)
                return null;

            List<string> parametersList = new List<string>();

            for (int i = 0; i < theMethod.GetParameters().Count(); i++)
            {
                if (theMethod.GetParameters()[i].Name.Equals("logedInUser"))
                    parametersList.Add(GetUser().Name);
                else
                {
                    Console.WriteLine($"Please, enter meeting {theMethod.GetParameters()[i].Name}: ");
                    var input = Console.ReadLine();
                    parametersList.Add(input);
                }
            }

            return parametersList.Cast<object>().ToArray();
        }
        
        private string GetMethodsList()
        {
            string methodsList = "Type a method: \n" +
                "[Exit] - Exit\n" +
                "[CreateMeeting] - Create new meeting\n" +
                "[DeleteMeeting] - Delete meeting\n" +
                "[AddAttendee] - Add attendee to meeting\n" +
                "[RemoveAttendee] - Remove attendee from the meeting\n" +
                "[Meetings] - Get all meetings\n" +
                "[GetByDescription] - Get all meetings by description\n" +
                "[GetByResponsiblePerson] - Get all meetings by responsible person\n" +
                "[GetByCategory] - Get all meetings by category\n" +
                "[GetByType] - Get all meetings by type\n" +
                "[GetByDates] - Get all meetings by dates\n" +
                "[GetByAttendees] - Get all meetings by attendees\n";
            return methodsList;
        }

        private string GetInput()
        {
            bool isLoggedIn = false;
            var name = "";

            while (!isLoggedIn)
            {
                Console.WriteLine("Enter your name to login: ");
                name = Console.ReadLine();
                if (validation.ValidateName(name))
                    isLoggedIn = true;
            }
            return name;
        }
    }
}
