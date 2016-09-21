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
        /* Test below passes, but need to refactor point to Mock
        [TestMethod]
        public void CanCreateInstance()
        {
            VariableStore store = new VariableStore();
            Assert.IsNotNull(store);
        */

        //set up your Mock dbContext
        private Mock<SavingVariablesContext> mock_context = new Mock<SavingVariablesContext>();
        [TestMethod]
        public void CanCreateInstance()
        {
            VariableStore store = new VariableStore(mock_context.Object);
            Assert.IsNotNull(store);
        }
    }
}

