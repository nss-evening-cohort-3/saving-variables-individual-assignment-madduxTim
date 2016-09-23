using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SavingVariables.DAL;
using System.Collections.Generic;
using SavingVariables.Models;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace SavingVariables.Tests.DAL
{

    [TestClass]
    public class VariableStoreTests
    {
        Mock<SavingVariablesContext> mock_context { get; set; }
        Mock<DbSet<SavedVariable>> mock_var_table { get; set; }
        List<SavedVariable> var_list { get; set; } // Fake 
        VariableRepository repo { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<SavingVariablesContext>();
            mock_var_table = new Mock<DbSet<SavedVariable>>();
            var_list = new List<SavedVariable>(); // Fake 
            repo = new VariableRepository(mock_context.Object);
            MocksSetup();
        }
        
        public void MocksSetup()
        {
            // setting up mock DbSets
            // when implemented, methods on Mock DbSets, the application
            // should use the methods that are on the list_queryable IQueryable instead 
            var list_queryable = var_list.AsQueryable();

            // This 'lies' to LINQ, making it think that our new list_queryable is a database table. 
            mock_var_table.As<IQueryable<SavedVariable>>().Setup(x => x.Provider).Returns(list_queryable.Provider);
            mock_var_table.As<IQueryable<SavedVariable>>().Setup(x => x.Expression).Returns(list_queryable.Expression);
            mock_var_table.As<IQueryable<SavedVariable>>().Setup(x => x.ElementType).Returns(list_queryable.ElementType);
            mock_var_table.As<IQueryable<SavedVariable>>().Setup(x => x.Provider).Returns(list_queryable.Provider);
            
            // SavedVariables property returns our list_queryable (aka fake database table) 
            mock_context.Setup(x => x.SavedVariables).Returns(mock_var_table.Object);
            mock_var_table.Setup(x => x.Add(It.IsAny<SavedVariable>())).Callback((SavedVariable saved_var) => var_list.Add(saved_var));
            // Remove
        }

        [TestCleanup] // need to read about this. "don't use code you don't understand."
        public void TearDown()
        {
            repo = null;
        }

        // Test below passes, but need to refactor point to Mock
        [TestMethod]
        public void CanCreateInstance()
        {
            VariableRepository repo = new VariableRepository();
            Assert.IsNotNull(repo);
        }        

        [TestMethod]
        public void CanCreateMockInstance()
        {
            VariableRepository repo = new VariableRepository();
            SavingVariablesContext actual_context = repo.Context;
            Assert.IsInstanceOfType(actual_context, typeof(SavingVariablesContext));
        }

        [TestMethod]
        public void CheckRepoEmpty()
        {
            //Arrange

            //Act
            List<SavedVariable> actual_vars = repo.GetAll();
            int expected = 0;
            int actual = actual_vars.Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanAddVars()
        {
            //Arrange
            SavedVariable testVar = new SavedVariable { ID = 1, Name = "a", Value = 1 };
            //Act
            repo.AddVar(testVar);
            int actual_vars_count = repo.GetAll().Count;
            int expected = 1;
            //Assert
            Assert.AreEqual(expected, actual_vars_count);
        }

        [TestMethod]
        public void CanRemoveVars()
        {

        }
    }
}