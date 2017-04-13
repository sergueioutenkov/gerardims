using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RusoCars.Controllers;
using RusoCars.Models;


namespace RusoCars.Tests.Controllers
{
    /// <summary>
    /// Summary description for CategoryUnitTest
    /// </summary>
    [TestClass]
    public class CategoryUnitTest
    {
        [TestMethod]
        public void TestABMCategory()
        {
            var db = new CarsRusoEntities();
            var Categorycontroller = new CategoryController();

            var category = new Category
            {
                Name = "##Test##"
            };

            Categorycontroller.Create(category);

            Assert.AreEqual(Categorycontroller., category.Name);

        }
    }
}
