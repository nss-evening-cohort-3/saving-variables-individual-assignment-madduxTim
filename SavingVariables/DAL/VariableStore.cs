using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables.DAL
{
    public class VariableStore
    {
        // "a = 4" --> adds a and value to dictionary (create)
        // "clear a" --> deletes value of a (delete) 
        // "a" --> shows value of a (read)
        // "clear all" || "remove all" || "delete all" --> removes all saved entries from database (delete)
        // "lastq" --> prints the last entered command or expression even if it was unsuccessful (read) 
        // "show all" --> reads all of the vars and their values (read) 
        // "exit" || "quit" --> closes the program 
    }
}
