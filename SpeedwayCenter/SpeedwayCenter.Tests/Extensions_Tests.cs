using System;
using System.Web.Mvc;
using SpeedwayCenter.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpeedwayCenter.Tests
{
    [TestClass]
    public class Extensions_Tests
    {
        [TestMethod]
        public void BootstrapCheckbox()
        {
            //Arrange
            HtmlHelper target = null;
            var name = "Test";
            var text = "Testing text";

            var expected = @"<label>"
                + @"<input id=""Test"" name=""Test"" type=""checkbox"" />"
                + @"<input name=""Test"" type=""hidden"" value=""False"" />"
                + @"Testing text</label>";

            //Act
            var result = target.BootstrapCheckBox(name, text);
            
            //Assert
            Assert.AreEqual(expected, result.ToString());
        }

        [TestMethod]
        public void DateConverter()
        {
            //Arrange


            //Act
            //Assert
        }
    }
}
