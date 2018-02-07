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
            // Arrange
            ApplicationController controller = new ApplicationController();

            // Act
            ViewResult result = controller.GetNewApplication() as ViewResult;


            //Execute Driver 
            ChromeDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://codedot.net/");
            IWebElement clickableLink = driver.FindElementById("igrad-external");
            IEnumerable<string> windowHandles = driver.WindowHandles;
            string urlToNavTo = clickableLink.GetAttribute("href");
            driver.Navigate().GoToUrl(urlToNavTo);
            string title = "iGrad / Home";
            Debug.WriteLine(driver.Title);

            //Assert
            Assert.IsTrue(driver.Title == title);


        }
    }
}
