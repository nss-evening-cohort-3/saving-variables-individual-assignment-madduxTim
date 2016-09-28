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
                    foreach (var savedVar in repo.GetVars())
                    {
                        Console.WriteLine($"{savedVar.Name} = {savedVar.Value}");
                    };
                    lastq = input;
                    break;
                case "clear all":
                case "remove all":
                case "delete all":
                    foreach (var savedVar in repo.GetVars())
                    {
                        repo.RemoveVar(savedVar.Name);
                    }
                    Console.WriteLine("All variables deleted. Blammo!");
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
            // if there's a match between input and the regex called regexAdd, create the new variable and value.
            if (regexAdd.IsMatch(input))
            {
                RegexHandlerAdd(input);
            }
            else if (regexDel.IsMatch(input))
            {
                RegexHandlerDelete(input);
            }
            else if (regexShow.IsMatch(input))
            {
                RegexHandlerShow(input);
            }
            else
            {
                Console.WriteLine(" Your input is incorrect. \n Try setting a one-letter variable to one-to-three digit numeral. \n Ex: \"h = 753\"");
            }
        }
        public void RegexHandlerAdd(string input)
        {
            Regex regexAdd = new Regex(addRegex);
            Match addVarCapture = regexAdd.Match(input);
            Match addValueCapture = regexAdd.Match(input);
            string varMatch = addVarCapture.Groups["Var"].Value;
            int valueMatch = Convert.ToInt32(addValueCapture.Groups["Num"].Value);
            bool isMatch = false;
            foreach (var savedVar in repo.GetVars())
            {
                if (savedVar.Name == varMatch)
                {
                    Console.WriteLine("Sorry, looks like that letter is already assigned a value.");
                    isMatch = true;
                }
            }
            if (!isMatch)
            {
                Console.WriteLine($"Awesome possum! {varMatch} now equals {valueMatch}");
                repo.AddVar(varMatch, valueMatch);
            }
        }
        public void RegexHandlerShow(string input)
        {
            Regex regexShow = new Regex(showRegex);
            Match showVarCapture = regexShow.Match(input);
            string varMatch = showVarCapture.Groups["Var"].Value;
            bool isMatch = false;
            foreach (var savedVar in repo.GetVars())
            {
                if (savedVar.Name == varMatch)
                {
                    Console.WriteLine($"{savedVar.Name} = {savedVar.Value}");
                    isMatch = true;
                }
            }
            if (!isMatch)
            {
                Console.WriteLine("Sorry, that character hasn't been assigned yet.");
            }
        }
        public void RegexHandlerDelete(string input)
        {
            Regex regexDel = new Regex(delRegex);
            Match deleteVarCapture = regexDel.Match(input);
            string varMatch = deleteVarCapture.Groups["Var"].Value;
            bool isMatch = false;
            foreach (var savedVar in repo.GetVars())
            {
                if (savedVar.Name == varMatch)
                {
                    Console.WriteLine($"{varMatch} has been cleared.");
                    isMatch = true;
                    repo.RemoveVar(varMatch);
                }
            }
            if (!isMatch)
            {
                Console.WriteLine("Sorry, that character hasn't been assigned yet.");
            }
        }
    }
}

    // ----------- * Program input reqs: * ----------- //

                          /*
                          
     "a = 4" --> adds a and value to table (create)
     "clear a" --> deletes value of a (delete) 
     "a" --> shows value of a (read)
     "clear all" || "remove all" || "delete all" --> removes all saved entries from database (delete)
     "lastq" --> prints the last entered command or expression even if it was unsuccessful (read) 
     "show all" --> reads all of the vars and their values (read) 
     "exit" || "quit" --> closes the program 
     
                          */
