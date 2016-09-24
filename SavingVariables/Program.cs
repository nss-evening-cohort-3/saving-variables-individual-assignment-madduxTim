using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SavingVariables.DAL;

namespace SavingVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            InputHandler input_handler = new InputHandler();
            Console.WriteLine("Let's get to savin' some variables!");
            while (input_handler.@switch == true)
            {
                string linePrompt = ">> ";
                Console.Write(linePrompt);
                string input = Console.ReadLine().ToLower();
                input_handler.InputSwitch(input);                
            }
        }
    }
}
