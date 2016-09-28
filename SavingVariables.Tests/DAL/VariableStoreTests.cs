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
            mock_var_table.As<IQueryable<SavedVariable>>().Setup(x => x.GetEnumerator()).Returns(() => list_queryable.GetEnumerator());
            // SavedVariables property returns our list_queryable (aka fake database table) 
            mock_context.Setup(x => x.SavedVariables).Returns(mock_var_table.Object);
            mock_var_table.Setup(x => x.Add(It.IsAny<SavedVariable>())).Callback((SavedVariable a) => var_list.Add(a));
            mock_var_table.Setup(x => x.Remove(It.IsAny<SavedVariable>())).Callback((SavedVariable b) => var_list.Remove(b));
            // something for replace?
        }

        //[TestCleanup] // need to read about this. "don't use code you don't understand."
        //public void TearDown()
        //{
        //    repo = null;
        //}

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
            List<SavedVariable> actual_vars = repo.GetVars();             
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
            int actual_vars_count = repo.GetVars().Count;
            int expected = 1;
            //Assert
            Assert.AreEqual(expected, actual_vars_count);
        }

        [TestMethod]
        public void CanAddVarsWithArgs()
        {
            //Arrange
            //Act
            repo.AddVar("f", 15);
            List<SavedVariable> list = repo.GetVars();
            string expected = "f";
            string actual = list.First().Name;
            //Assert
            Assert.IsTrue(expected == actual);
        }

        [TestMethod]
        public void CanTargetVar()
        {
            //Arrange
            var_list.Add(new SavedVariable { ID = 1, Name = "a", Value = 1 });
            var_list.Add(new SavedVariable { ID = 2, Name = "b", Value = 2 });
            var_list.Add(new SavedVariable { ID = 3, Name = "c", Value = 3 });
            //Act
            string var_name = "c";
            SavedVariable actual_var = repo.TargetVar(var_name);
            //Assert
            int expected = 3;
            int actual = actual_var.ID;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanRemoveVars()
        {
            //Arrange
            var_list.Add(new SavedVariable { ID = 1, Name = "a", Value = 1 });
            var_list.Add(new SavedVariable { ID = 2, Name = "b", Value = 2 });
            var_list.Add(new SavedVariable { ID = 3, Name = "c", Value = 3 });
            //Act
            string var_name = "c";
            SavedVariable removed_var = repo.RemoveVar(var_name);
            int expected = 2;
            int actual = repo.GetVars().Count;
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}