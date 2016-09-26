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
        //switch bool kills the program 
        public bool @switch = true;
        //stores the lastq 
        string lastq = "Nothing to see here, yet. Check back one more time to see your last request.";
        //This first switch statement is for initially sorting input options 
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
                    Console.WriteLine(lastq);
                    lastq = input;
                    break;
                case "show all":
                    Console.WriteLine("method for showing all variables/values");
                    lastq = input;
                    break;
                case "clear all":
                case "remove all":
                case "delete all":
                    Console.WriteLine("method for deleting all vars");
                    lastq = input;
                    break;
                default:
                    RegexSorter(input);
                    lastq = input;
                    break;
            }
        }
        //this regex is looking for a Variable of 1 letter and a value 
        //of any numeric value between 1 and 3 digits
        string myRegex = @"^(?<Var>[a-z]{1})\s*\=\s*(?<Num>[0-9]{1,3})$";
        //this second sorting method matches regex and begins saving process. 
        public void RegexSorter(string input)
        {
            Regex regex = new Regex(myRegex);
            if (regex.IsMatch(input))
            {
                Console.WriteLine("Now we're cooking with grease!");
            }
            else
            {
                Console.WriteLine(" Your input is incorrect. \n Try setting a one-letter variable to one-to-three digit numeral. \n Ex: \"h = 753\"");
            }
        }
    }
}
