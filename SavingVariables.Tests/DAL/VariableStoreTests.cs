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
        [TestInitialize] // gets run before each test method
        public void TestSetUp()
        {
            var list_queryable = mock_variables_list.AsQueryable();
            saved_variables.As<IQueryable<SavedVariable>>().Setup(x => x.Provider).Returns(list_queryable.Provider);
            saved_variables.As<IQueryable<SavedVariable>>().Setup(x => x.Expression).Returns(list_queryable.Expression);
            saved_variables.As<IQueryable<SavedVariable>>().Setup(x => x.ElementType).Returns(list_queryable.ElementType);
            saved_variables.As<IQueryable<SavedVariable>>().Setup(x => x.Provider).Returns(list_queryable.Provider);
        }

        /* Test below passes, but need to refactor point to Mock
        [TestMethod]
        public void CanCreateInstance()
        {
            VariableStore store = new VariableStore();
            Assert.IsNotNull(store);
        */

        //set up your Mock dbContext
        private Mock<SavingVariablesContext> mock_context = new Mock<SavingVariablesContext>();
        private Mock<DbSet<SavedVariable>> saved_variables = new Mock<DbSet<SavedVariable>>();
        // A couple fake variables to play with for testing.
        List<SavedVariable> mock_variables_list = new List<SavedVariable>
        {
            new SavedVariable { ID = 1, Name = "a", Value = 1 },
            new SavedVariable { ID = 2, Name = "b", Value = 2 },
            new SavedVariable { ID = 3, Name = "c", Value = 3 }
        };


        [TestMethod]
        public void CanCreateInstance()
        {
            VariableStore store = new VariableStore(mock_context.Object);
            Assert.IsNotNull(store);
        }
        [TestMethod]
        public void CanMockDbSet()
        {

        }
    }
}

