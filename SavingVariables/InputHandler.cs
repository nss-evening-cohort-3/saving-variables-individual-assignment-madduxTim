using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SavingVariables
{
    class InputHandler
    {
        public bool @switch = true;
        public void InputSwitch(string input)
        {
            switch (input.Trim())
            {
                case "exit":
                case "quit":
                    Console.WriteLine("Peace.");
                    Thread.Sleep(800);
                    @switch = false;
                    break;
                case "lastq":
                    Console.WriteLine("method for repeating the last input");
                    break;
                case "show all":
                    Console.WriteLine("method for showing all variables/values");
                    break;
                case "clear all":
                case "remove all":
                case "delete all":
                    Console.WriteLine("method for deleting all vars");
                    break;
                default:
                    Console.WriteLine("Probably use this for further evaluating");
                    break;
            }
        }
    }
}
