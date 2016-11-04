using ApplicationBoot;
using ChatRobot;
using Microsoft.Practices.ServiceLocation;
using System;


namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Init.InitializeApplication();
            var chatRobot = ServiceLocator.Current.GetInstance<IChatRobot>();
            do
            {
                while (!Console.KeyAvailable)
                {
                    string input = Console.ReadLine();
                    var response = chatRobot.GetResponse(input);
                    Console.WriteLine(response);
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);




            //SessionHandler.Display();

        }
    }
}
