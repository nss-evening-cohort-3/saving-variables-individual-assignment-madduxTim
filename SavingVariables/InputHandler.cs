using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
                    RegexSwitch(input);
                    break;
            }
        }
        //this regex is looking for a Variable of 1 letter and a value 
        //of any numeric value between 1 and 3 digits
        string myRegex = @"^(?<Var>[a-z]{1})\s*\=\s*(?<Num>[0-9]{1,3})$";
        public void RegexSwitch(string input)
        {
            Regex regex = new Regex(myRegex);
            if (regex.IsMatch(input))
            {
                Console.WriteLine("Now we're cooking with grease!");
            } else
            {
                Console.WriteLine(" Your input is incorrect. \n Try setting a one-letter variable to one-to-three digit numeral. \n Ex: \"h = 753\"");
            }
        }
    }
}
