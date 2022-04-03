using MeetingsSimulator.Managers;
using MeetingsSimulator.Services;

namespace MeetingsSimulator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Visma's internal meetings";

            UserManager userManager = new UserManager();
            userManager.GetUser();
            Console.WriteLine("Successfully logged in!\n");

            userManager.StartApplication();
        }
    }
}