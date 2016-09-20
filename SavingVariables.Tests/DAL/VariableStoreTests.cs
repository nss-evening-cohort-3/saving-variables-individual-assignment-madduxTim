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
        Mock<SavedVariables> mock_context { get; set; }
        VariableStore store { get; set; }

        /* Test below passes, but need to refactor point to Mock
        [TestMethod]
        public void CanCreateInstance()
        {
            VariableStore store = new VariableStore();
            Assert.IsNotNull(store);
        */
        //[TestMethod]
        //public void 
    }
}

