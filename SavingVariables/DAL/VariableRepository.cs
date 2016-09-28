using System.Collections.Generic;
using System.Linq;
using SavingVariables.Models;

namespace SavingVariables.DAL
{
    public class VariableRepository
    { 
        // Dependency injection -- In order for your repository to use your dbcontext, make the constructor of your repository class take a an instance of dbcontext class a parameter, and assign the dbcontext being in to a property on the repo class. 
        public SavingVariablesContext Context { get; set; }
        public VariableRepository()
        {
            Context = new SavingVariablesContext();
        }
        public VariableRepository(SavingVariablesContext _context)
        {
            Context = _context;
        }

        public List<SavedVariable> GetVars() // <------ AKA 'Read'
        {
            return Context.SavedVariables.ToList();
        }

        public void AddVar(SavedVariable @var) // <----- AKA 'Create'
        {
            //SavedVariable added_var = new SavedVariable();
            Context.SavedVariables.Add(@var);
            Context.SaveChanges();
        }
        public void AddVar(string var_name, int value)
        {
            SavedVariable savedvariable = new SavedVariable { Name = var_name, Value = value };
            Context.SavedVariables.Add(savedvariable);
            Context.SaveChanges();          
        }
        public SavedVariable TargetVar(string var_name)
        {
            //SavedVariable target = Context.SavedVariables.FirstOrDefault(a => Convert.ToInt32(a.ID) == Convert.ToInt32(id)); 
            // ^^^^ First attempt trying to search for ID.No worky. The param was an int called "id". ^^^^^
            SavedVariable target = Context.SavedVariables.FirstOrDefault(a => a.Name.ToLower() == var_name.ToLower());
            return target;
        }
        public SavedVariable RemoveVar(string var_name) // <----- AKA 'Delete' 
        {
            SavedVariable found_var = TargetVar(var_name);
            if (found_var != null)
            {
                Context.SavedVariables.Remove(found_var);
                Context.SaveChanges();  
            }
            return found_var;
        }

        // "a = 4" --> adds a and value to table (create)
        // "clear a" --> deletes value of a (delete) 
        // "a" --> shows value of a (read)
        // "clear all" || "remove all" || "delete all" --> removes all saved entries from database (delete)
        // "lastq" --> prints the last entered command or expression even if it was unsuccessful (read) 
        // "show all" --> reads all of the vars and their values (read) 
        // "exit" || "quit" --> closes the program 
    }
}
