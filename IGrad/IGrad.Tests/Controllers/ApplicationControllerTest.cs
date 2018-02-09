using IGrad.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Diagnostics;

namespace IGrad.Tests.Controllers
{
    [TestClass]
    public class ApplicationControllerTest
    {
        [TestMethod]
        public void GetNewApplicationExists()
        {
            // Arrange
            ApplicationController controller = new ApplicationController();

            // Act
            ViewResult result = controller.GetNewApplication() as ViewResult;

            // Assert
            Assert.IsNotNull(result);    
            
                 
        }

        [TestMethod]
        public void GetHomeTestLinkForExternalIGradSite()
        {
            //Execute Driver 
            ChromeDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://codedot.net/");
            IWebElement clickableLink = driver.FindElementById("igrad-external");
            string urlToNavTo = clickableLink.GetAttribute("href");
            string iGradTitle = driver.Title;

            //Assert the title of the driver and title of page are equal
            Assert.AreEqual(driver.Title, iGradTitle);
            string title = "iGrad / Home";
            driver.Navigate().GoToUrl(urlToNavTo);

            //Assert the title of new page and title of url are equal
            Assert.IsTrue(driver.Title == title);


        }


    }
}
