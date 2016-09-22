using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SavingVariables.Models;

namespace SavingVariables.DAL
{
    public class VariableStore
    {
        /* Dependency injection -- In order for your repository to use 
        your dbcontext, make the constructor of your repository class
        take a an instance of dbcontext class a parameter, and assign 
        the dbcontext being in to a property on the repo class. 
        */
        public SavingVariablesContext Context { get; set; }
        public VariableStore()
        {
            Context = new SavingVariablesContext();
        }
        public VariableStore(SavingVariablesContext _context)
        {
            Context = _context;
        }

        public List<SavedVariable> GetAll()
        {
            return Context.SavedVariables.ToList();
        }


        // "a = 4" --> adds a and value to dictionary (create)
        // "clear a" --> deletes value of a (delete) 
        // "a" --> shows value of a (read)
        // "clear all" || "remove all" || "delete all" --> removes all saved entries from database (delete)
        // "lastq" --> prints the last entered command or expression even if it was unsuccessful (read) 
        // "show all" --> reads all of the vars and their values (read) 
        // "exit" || "quit" --> closes the program 
    }
}
