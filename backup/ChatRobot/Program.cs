using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                while (!Console.KeyAvailable)
                {
                    string input = Console.ReadLine();

                    Console.WriteLine(ChatBot.GetResponse(input));
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

           


                //SessionHandler.Display();
           
        }
    }
}
