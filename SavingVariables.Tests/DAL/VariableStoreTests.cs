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
        //set up Mock dbContext
        public Mock<SavingVariablesContext> mock_context = new Mock<SavingVariablesContext>();
        public Mock<DbSet<SavedVariable>> saved_variables = new Mock<DbSet<SavedVariable>>();
        VariableStore repo { get; set; }
        List<SavedVariable> var_list { get; set; } // Fake 
        // A couple fake variables to play with for testing.
        [TestInitialize] // gets run before each test method
        public void Initialize()
        {
            MocksSetup();
            //mock_context = new Mock<VariableStore>();
            //mock_variable_table = new Mock<DbSet<SavedVariable>>();
            //mock_variable_list = new List<SavedVariable>(); //Fake
            repo = new VariableStore();

        }
        List<SavedVariable> mock_variables_list = new List<SavedVariable>
        {
            new SavedVariable { ID = 1, Name = "a", Value = 1 },
            new SavedVariable { ID = 2, Name = "b", Value = 2 },
            new SavedVariable { ID = 3, Name = "c", Value = 3 }
        };
        public void MocksSetup()
        {
            // setting up mock DbSets
            // when implemented, methods on Mock DbSets, the application
            // should use the methods that are on the list_queryable IQueryable instead 
            var list_queryable = mock_variables_list.AsQueryable();
            saved_variables.As<IQueryable<SavedVariable>>().Setup(x => x.Provider).Returns(list_queryable.Provider);
            saved_variables.As<IQueryable<SavedVariable>>().Setup(x => x.Expression).Returns(list_queryable.Expression);
            saved_variables.As<IQueryable<SavedVariable>>().Setup(x => x.ElementType).Returns(list_queryable.ElementType);
            saved_variables.As<IQueryable<SavedVariable>>().Setup(x => x.Provider).Returns(list_queryable.Provider);
            mock_context.Setup(x => x.SavedVariables).Returns(saved_variables.Object);
            saved_variables.Setup(x => x.Add(It.IsAny<SavedVariable>())).Callback((SavedVariable saved_var) => mock_variables_list.Add(thing));
        }


        // Test below passes, but need to refactor point to Mock
        [TestMethod]
        public void CanCreateInstance()
        {
            VariableStore store = new VariableStore();
            Assert.IsNotNull(store);
        }        

        [TestMethod]
        public void CanCreateMockInstance()
        {
            VariableStore store = new VariableStore(mock_context.Object);
            Assert.IsNotNull(store);
        }
        [TestMethod]
        public void CanMockDbSet()
        {
            //
        }
        [TestMethod]
        public void CanGetAllSavedVars()
        {
            //Arrange
            // see above. maybe refactor later to get actual nums?
            //mock_context = new Mock<SavingVariablesContext>();
            //repo = new VariableStore(mock_context.Object);
            
            //Act
            List<SavedVariable> actualVariables = repo.GetAll();
            int expected = 0;
            int actualVariablesCount = actualVariables.Count;

            //Assert
            Assert.AreEqual(actualVariablesCount, expected);
        }
        [TestMethod]
        public void CanAddSavedVars()
        {
            //Arrange

            //Act
            SavedVariable testVar = new SavedVariable
            {
                ID = 1,
                Name= "a",
                Value = 1
            };

            //Assert
            repo.Add(testVar);
        }
    }
}

