using System;
using System.Threading;
using System.Text.RegularExpressions;
using SavingVariables.DAL;

namespace SavingVariables
{
    public class InputHandler
    {
        //switch bool kills the program 
        public bool @switch = true;
        //stores the lastq 
        private string lastq = "Nothing to see here, yet. Check back one more time to see your last request.";
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
                    RegexSorter(input.Trim());
                    lastq = input;
                    break;
            }
        }
        //this regex is looking for a Variable of 1 letter and a value 
        //of any numeric value between 1 and 3 digits
        public string addRegex = @"^(?<Var>[a-z]{1})\s*\=\s*(?<Num>[0-9]{1,3})$";
        public string delRegex = @"^clear\s*(?<Var>[a-z]{1})$";
        public string showRegex = @"^(?<Var>[a-z]{1})$";
        VariableRepository repo = new VariableRepository();
        //this second sorting method matches regex and begins saving process. 
        public void RegexSorter(string input)
        {
            Regex regexAdd = new Regex(addRegex);
            Regex regexDel = new Regex(delRegex);
            Regex regexShow = new Regex(showRegex);
            Match showVarCapture = regexShow.Match(input);
            Match addVarCapture = regexAdd.Match(input);
            Match addValueCapture = regexAdd.Match(input);
            if (regexAdd.IsMatch(input))
            {
                string varMatch = addVarCapture.Groups["Var"].Value;
                int valueMatch = Convert.ToInt32(addValueCapture.Groups["Num"].Value);
                repo.AddVar(varMatch, valueMatch);
                //Console.WriteLine($" Nice work! {varMatch} now equals {valueMatch}");
            }
            else if (regexDel.IsMatch(input))
            {
                Console.WriteLine("Delete!");
            }
            else if (regexShow.IsMatch(input))
            {
                string varMatch = showVarCapture.Groups["Var"].Value;
                var allVars = repo.GetVars();
                foreach (var savedVar in allVars)
                {
                    Console.WriteLine($"{savedVar.Name} = {savedVar.Value}");
                }
                Console.WriteLine(repo.GetVars());
                //Console.WriteLine(allVars);
                //Console.WriteLine($"Show! = {varMatch}");
            }
            else
            {
                Console.WriteLine(" Your input is incorrect. \n Try setting a one-letter variable to one-to-three digit numeral. \n Ex: \"h = 753\"");
            }
        }
    }
}
