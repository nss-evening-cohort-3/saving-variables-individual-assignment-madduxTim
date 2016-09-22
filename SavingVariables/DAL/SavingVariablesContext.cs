using SavingVariables.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables.DAL
{
    public class SavingVariablesContext : DbContext
    {
		public virtual DbSet<SavedVariable> SavedVariables {get;set;}
    }
}
