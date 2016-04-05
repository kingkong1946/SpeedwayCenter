using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SpeedwayCenter.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpeedwayCenter.ORM;
using SpeedwayCenter.ORM.Models;
using SpeedwayCenter.ORM.Repository;

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
            var work = new UnitOfWork(new SpeedwayCenterContext(), new List<object>());
            var test1 = work.GetQueryRepository<Rider>() as QueryRepository<Rider>;
            var test2 = work.GetRepository<Rider>() as Repository<Rider>;
            var test3 = work.GetQueryRepository<Team>() as QueryRepository<Team>;
            var test4 = work.GetRepository<Team>() as Repository<Team>;

            Assert.AreSame(test1._context, test2._context);
            Assert.AreSame(test1._context, test3._context);
            Assert.AreSame(test1._context, test4._context);
            Assert.AreSame(test2._context, test3._context);
            Assert.AreSame(test2._context, test4._context);
            Assert.AreSame(test3._context, test4._context);

            //Act
            //Assert
        }
    }
}
