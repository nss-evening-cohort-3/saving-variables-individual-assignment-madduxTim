using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SavingVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's get to savin' some variables!");
            bool @switch = true;
            //int counter = 0;
            while (@switch == true)
            {
                string linePrompt = ">> ";
                Console.Write(linePrompt);
                string input = Console.ReadLine().ToLower();
                //counter += 1;
                if (input == "exit" | input == "quit")
                {
                    Console.WriteLine("Ok. See you later.");
                    Thread.Sleep(1500);
                    @switch = false;

                }
                else
                {
                    Console.WriteLine("ok, repeat.");
                }
            }
        }
    }
}
