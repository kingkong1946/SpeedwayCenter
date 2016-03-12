using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpeedwayCenter.Controllers;
using SpeedwayCenter.Models.Entity_Framework;
using SpeedwayCenter.Models.Repository;

namespace SpeedwayCenter.Tests
{
    [TestClass]
    public class SpeedwayController_Tests
    {
        //[TestMethod]
        //public void IndexAction()
        //{
        //    //Arrange
        //    List<Rider> collection = CreateFakeBase();
        //    IQueryRepository<Rider> fakeRepository = CreateFakeRepository(collection);
        //    var target = new RiderController(fakeRepository);

        //    const int expectedCount = 4;

        //    //Act
        //    var result = (ViewResult)target.Index();

        //    //Assert
        //    var model = (IQueryable<Rider>)result.Model;
        //    Assert.AreEqual(expectedCount, model.Count());
        //}

        //[TestMethod]
        //public void AddAction()
        //{
        //    //Arrange
        //    List<Rider> collection = CreateFakeBase();
        //    IQueryRepository<Rider> fakeRepository = CreateFakeRepository(collection);
        //    var target = new RiderController(fakeRepository);
        //    var fakeHttpContextBase = new Mock<HttpContextBase>();
        //    fakeHttpContextBase.Setup(b => b.Server.MapPath(It.IsAny<string>())).Returns("\\");
        //    target.ControllerContext = new ControllerContext(fakeHttpContextBase.Object, new RouteData(), target);

        //    var newRecord = new Rider
        //    {
        //        Id = 5,
        //        FirstName = "First5",
        //        LastName = "Last5",
        //        Country = "Country5",
        //        BirthDate = new DateTime(2015, 01, 05)
        //    };
        //    string imagePath = $"~/Photos/{newRecord.GetHashCode()}.png";
        //    HttpPostedFileBase newImage = CreateFakeImage();
        //    const int expectedCount = 5;

        //    //Act
        //    var result = target.Add(newRecord, newImage);

        //    //Assert
        //    Rider actual = collection.Find(rider => rider.Id == 5);
        //    Assert.AreEqual(expectedCount, collection.Count, $"Count is {collection.Count}");
        //    Assert.AreEqual(newRecord.Id, actual.Id, "Wrong id");
        //    Assert.AreEqual(imagePath, actual.Image, $"Actual image path is: {actual.Image}, \nexpected: {imagePath}");
        //}

        //[ExpectedException(typeof(ArgumentNullException))]
        //[TestMethod]
        //public void AddAction_RiderIsNull()
        //{
        //    //Arrange
        //    List<Rider> collection = CreateFakeBase();
        //    IQueryRepository<Rider> fakeRepository = CreateFakeRepository(collection);
        //    var target = new RiderController(fakeRepository);
        //    var fakeHttpContextBase = new Mock<HttpContextBase>();
        //    fakeHttpContextBase.Setup(b => b.Server.MapPath(It.IsAny<string>())).Returns("\\");
        //    target.ControllerContext = new ControllerContext(fakeHttpContextBase.Object, new RouteData(), target);

        //    HttpPostedFileBase newImage = CreateFakeImage();

        //    //Act
        //    //Assert
        //    var result = target.Add(null, newImage);
        //}

        //[TestMethod]
        //public void AddAction_ImageIsNull()
        //{
        //    //Arrange
        //    List<Rider> collection = CreateFakeBase();
        //    IQueryRepository<Rider> fakeRepository = CreateFakeRepository(collection);
        //    var target = new RiderController(fakeRepository);
        //    var fakeHttpContextBase = new Mock<HttpContextBase>();
        //    fakeHttpContextBase.Setup(b => b.Server.MapPath(It.IsAny<string>())).Returns("\\");
        //    target.ControllerContext = new ControllerContext(fakeHttpContextBase.Object, new RouteData(), target);

        //    var newRecord = new Rider
        //    {
        //        Id = 5,
        //        FirstName = "First5",
        //        LastName = "Last5",
        //        Country = "Country5",
        //        BirthDate = new DateTime(2015, 01, 05)
        //    };
        //    const int expectedCount = 5;

        //    //Act
        //    var result = target.Add(newRecord, null);

        //    //Assert
        //    Rider actual = collection.Find(rider => rider.Id == 5);
        //    Assert.AreEqual(expectedCount, collection.Count, $"Count is {collection.Count}");
        //    Assert.AreEqual(newRecord.Id, actual.Id, "Wrong id");
        //    Assert.IsNull(actual.Image);
        //}

        //[ExpectedException(typeof(ArgumentNullException))]
        //[TestMethod]
        //public void AddAction_BothNull()
        //{
        //    //Arrange
        //    List<Rider> collection = CreateFakeBase();
        //    IQueryRepository<Rider> fakeRepository = CreateFakeRepository(collection);
        //    var target = new RiderController(fakeRepository);
        //    var fakeHttpContextBase = new Mock<HttpContextBase>();
        //    fakeHttpContextBase.Setup(b => b.Server.MapPath(It.IsAny<string>())).Returns("\\");
        //    target.ControllerContext = new ControllerContext(fakeHttpContextBase.Object, new RouteData(), target);

        //    //Act
        //    //Assert
        //    var result = target.Add(null, null);
        //}

        //[TestMethod]
        //public void DeleteAction()
        //{
        //    //Arrange
        //    List<Rider> collection = CreateFakeBase();
        //    IQueryRepository<Rider> fakeRepository = CreateFakeRepository(collection);
        //    var target = new RiderController(fakeRepository);
        //    const int riderIdToRemove = 1;
        //    const int expectedCount = 3;

        //    //Act
        //    var result = target.Delete(riderIdToRemove);

        //    //Assert
        //    Rider record = collection.Find(rider => rider.Id == riderIdToRemove);
        //    Assert.AreEqual(expectedCount, collection.Count);
        //    Assert.IsNull(record);
        //}

        //[TestMethod]
        //public void EditAction()
        //{
        //    //Arrange
        //    List<Rider> collection = CreateFakeBase();
        //    IQueryRepository<Rider> fakeRepository = CreateFakeRepository(collection);
        //    var target = new RiderController(fakeRepository);
        //    var fakeHttpContextBase = new Mock<HttpContextBase>();
        //    fakeHttpContextBase.Setup(b => b.Server.MapPath(It.IsAny<string>())).Returns("\\");
        //    target.ControllerContext = new ControllerContext(fakeHttpContextBase.Object, new RouteData(), target);

        //    Rider riderToEdit = collection[0];
        //    var newCountry = "AnotherCountry";
        //    var editedRider = new Rider
        //    {
        //        Id = riderToEdit.Id,
        //        FirstName = riderToEdit.FirstName,
        //        LastName = riderToEdit.LastName,
        //        BirthDate = riderToEdit.BirthDate,
        //        Country = newCountry
        //    };
        //    string imagePath = $"~/Photos/{editedRider.GetHashCode()}.png";
        //    HttpPostedFileBase newImage = CreateFakeImage();

        //    //Act
        //    RedirectToRouteResult result = target.Edit(editedRider, newImage, false);

        //    //Assert
        //    var updatedRider = collection.Find(rider => rider.Id == riderToEdit.Id);
        //    Assert.AreEqual(newCountry, updatedRider.Country);
        //    Assert.AreEqual(imagePath, updatedRider.Image);
        //}

        //[TestMethod]
        //public void EditAction_RemovePhoto()
        //{
        //    //Arrange
        //    List<Rider> collection = CreateFakeBase();
        //    IQueryRepository<Rider> fakeRepository = CreateFakeRepository(collection);
        //    var target = new RiderController(fakeRepository);

        //    Rider riderToEdit = collection[0];
        //    var editedRider = new Rider
        //    {
        //        Id = riderToEdit.Id,
        //        FirstName = riderToEdit.FirstName,
        //        LastName = riderToEdit.LastName,
        //        BirthDate = riderToEdit.BirthDate,
        //        Country = riderToEdit.Country
        //    };

        //    //Act
        //    RedirectToRouteResult result = target.Edit(editedRider, null, true);

        //    //Assert
        //    var updatedRider = collection.Find(rider => rider.Id == riderToEdit.Id);
        //    Assert.AreEqual(string.Empty, updatedRider.Image);
        //}

        //[TestMethod]
        //public void EditAction_KeepPhoto()
        //{
        //    //Arrange
        //    List<Rider> collection = CreateFakeBase();
        //    IQueryRepository<Rider> fakeRepository = CreateFakeRepository(collection);
        //    var target = new RiderController(fakeRepository);

        //    Rider riderToEdit = collection[0];

        //    //Act
        //    RedirectToRouteResult result = target.Edit(riderToEdit, null, false);

        //    //Assert
        //    var updatedRider = collection.Find(rider => rider.Id == riderToEdit.Id);
        //    Assert.AreEqual(riderToEdit.Image, updatedRider.Image);
        //}

        //[ExpectedException(typeof(NullReferenceException))]
        //[TestMethod]
        //public void EditAction_RiderIsNull()
        //{
        //    //Arrange
        //    List<Rider> collection = CreateFakeBase();
        //    IQueryRepository<Rider> fakeRepository = CreateFakeRepository(collection);
        //    var target = new RiderController(fakeRepository);
        //    var fakeHttpContextBase = new Mock<HttpContextBase>();
        //    fakeHttpContextBase.Setup(b => b.Server.MapPath(It.IsAny<string>())).Returns("\\");
        //    target.ControllerContext = new ControllerContext(fakeHttpContextBase.Object, new RouteData(), target);

        //    Rider riderToEdit = collection[0];
        //    string imagePath = $"~/Photos/{riderToEdit.GetHashCode()}.png";
        //    HttpPostedFileBase newImage = CreateFakeImage();

        //    //Act
        //    //Assert
        //    RedirectToRouteResult result = target.Edit(null, newImage, false);
        //}
        
        //[TestMethod]
        //public void EditAction_ImageIsNull()
        //{
        //    //Arrange
        //    List<Rider> collection = CreateFakeBase();
        //    IQueryRepository<Rider> fakeRepository = CreateFakeRepository(collection);
        //    var target = new RiderController(fakeRepository);

        //    Rider riderToEdit = collection[0];
        //    var newCountry = "AnotherCountry";
        //    var editedRider = new Rider
        //    {
        //        Id = riderToEdit.Id,
        //        FirstName = riderToEdit.FirstName,
        //        LastName = riderToEdit.LastName,
        //        BirthDate = riderToEdit.BirthDate,
        //        Country = newCountry,
        //        Image = riderToEdit.Image
        //    };

        //    //Act
        //    RedirectToRouteResult result = target.Edit(editedRider, null, false);

        //    //Assert
        //    var updatedRider = collection.Find(rider => rider.Id == riderToEdit.Id);
        //    Assert.AreEqual(newCountry, updatedRider.Country);
        //}

        //[ExpectedException(typeof(NullReferenceException))]
        //[TestMethod]
        //public void EditAction_BothNull()
        //{
        //    //Arrange
        //    List<Rider> collection = CreateFakeBase();
        //    IQueryRepository<Rider> fakeRepository = CreateFakeRepository(collection);
        //    var target = new RiderController(fakeRepository);

        //    //Act
        //    //Assert
        //    RedirectToRouteResult result = target.Edit(null, null, false);
        //}

        //private static HttpPostedFileBase CreateFakeImage()
        //{
        //    var mock = new Mock<HttpPostedFileBase>();
        //    mock.Setup(file => file.SaveAs(It.IsAny<string>())).Verifiable();
        //    return mock.Object;
        //}

        private static IQueryRepository<Rider> CreateFakeRepository(List<Rider> collection)
        {
            var mock = new Mock<IQueryRepository<Rider>>();
            mock.Setup(rep => rep.FindBy(It.IsAny<Expression<Func<Rider, bool>>>()))
                .Returns((Expression<Func<Rider, bool>> func) => collection.AsQueryable().Where(func).Select(rider => rider));
            mock.Setup(rep => rep.GetAll()).Returns(collection.AsQueryable());
            //mock.Setup(rep => rep.Add(It.IsAny<Rider>())).Callback<Rider>(collection.Add);
            //mock.Setup(rep => rep.Delete(It.IsAny<Rider>())).Callback<Rider>(rider => collection.Remove(rider));
            //mock.Setup(rep => rep.Edit(It.IsAny<Rider>())).Callback<Rider>(rider =>
            //{
            //    collection.RemoveAll(r => r.Id == rider.Id);
            //    collection.Add(rider);
            //});
            return mock.Object;
        }

        private static List<Rider> CreateFakeBase()
        {
            return new List<Rider>
            {
                new Rider
                {
                    Id = 1,
                    FirstName = "First1",
                    LastName = "Last1",
                    Country = "Country1",
                    BirthDate = new DateTime(2015, 01, 01),
                    Image = "Image1.png"
                },
                new Rider
                {
                    Id = 2,
                    FirstName = "First2",
                    LastName = "Last2",
                    Country = "Country2",
                    BirthDate = new DateTime(2015, 01, 02),
                    Image = "Image2.png"
                },
                new Rider
                {
                    Id = 3,
                    FirstName = "First3",
                    LastName = "Last3",
                    Country = "Country3",
                    BirthDate = new DateTime(2015, 01, 03),
                    Image = "Image3.png"
                },
                new Rider
                {
                    Id = 4,
                    FirstName = "First4",
                    LastName = "Last4",
                    Country = "Country4",
                    BirthDate = new DateTime(2015, 01, 04),
                    Image = "Image4.png"
                }
            };
        }
    }
}
