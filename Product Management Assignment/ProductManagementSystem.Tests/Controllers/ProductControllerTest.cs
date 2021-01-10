using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagementSystem;
using ProductManagementSystem.Controllers;
using ProductManagementSystem.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void Create()
        {
            // Arrange
            ProductController controller = new ProductController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
