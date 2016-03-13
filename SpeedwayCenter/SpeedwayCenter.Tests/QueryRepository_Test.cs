using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpeedwayCenter.Models.Entity_Framework;
using SpeedwayCenter.Models.Repository;
using SpeedwayCenter.Tests.Fakes;

namespace SpeedwayCenter.Tests
{
    [TestClass]
    public class QueryRepository_Test
    {
        [TestMethod]
        public void GetAll()
        {
            //Arrange
            List<FakeModel> fakeBase = CreateFakeBase();
            DbSet<FakeModel> fakeDbSet = CreateDbSetMock(fakeBase);
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            QueryRepository<FakeDbContext, FakeModel> target = new QueryRepository<FakeDbContext, FakeModel>(fakeContext);

            const int countExpected = 4;

            //Act
            var result = target.GetAll();

            //Assert
            Assert.AreEqual(countExpected, result.Count());
        }

        [TestMethod]
        public void GetAll_Null()
        {
            //Arrange
            DbSet<FakeModel> fakeDbSet = CreateDbSetMock(new List<FakeModel>());
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            QueryRepository<FakeDbContext, FakeModel> target = new QueryRepository<FakeDbContext, FakeModel>(fakeContext);
            const int expectedCount = 0;

            //Act
            var result = target.GetAll();

            //Assert
            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void FindBy()
        {
            //Arrange
            List<FakeModel> fakeBase = CreateFakeBase();
            DbSet<FakeModel> fakeDbSet = CreateDbSetMock(fakeBase);
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            QueryRepository<FakeDbContext, FakeModel> target = new QueryRepository<FakeDbContext, FakeModel>(fakeContext);
            var entityExpected = fakeBase.ToList()[0];

            //Act
            var resultCollection = target.FindMany(rider => rider.Desc == "Desc1");
            var result = resultCollection.ToList()[0];

            //Assert
            Assert.AreEqual(entityExpected.Id, result.Id, "Wrong Id");
            Assert.AreEqual(entityExpected.Desc, result.Desc, "Wrong Description");
        }

        [TestMethod]
        public void FindBy_Many()
        {
            //Arrange
            List<FakeModel> fakeBase = CreateFakeBase();
            DbSet<FakeModel> fakeDbSet = CreateDbSetMock(fakeBase);
            fakeDbSet.Add(fakeBase.ToList()[0]);
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            QueryRepository<FakeDbContext, FakeModel> target = new QueryRepository<FakeDbContext, FakeModel>(fakeContext);
            var entityExpected = fakeBase.Where(rider => rider.Desc == "Desc1").Select(rider => rider);

            //Act
            var result = target.FindMany(rider => rider.Desc == "Desc1");

            //Assert
            Assert.AreEqual(entityExpected.Count(), result.Count());
        }

        [TestMethod]
        public void FindBy_Null()
        {
            //Arrange
            DbSet<FakeModel> fakeDbSet = CreateDbSetMock(new List<FakeModel>());
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            QueryRepository<FakeDbContext, FakeModel> target = new QueryRepository<FakeDbContext, FakeModel>(fakeContext);
            const int expectedCount = 0;

            //Act
            var result = target.FindMany(rider => rider.Desc == "Desc1");

            //Assert
            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void FindFirst()
        {
            //Arrange
            List<FakeModel> fakeBase = CreateFakeBase();
            DbSet<FakeModel> fakeDbSet = CreateDbSetMock(fakeBase);
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            QueryRepository<FakeDbContext, FakeModel> target = new QueryRepository<FakeDbContext, FakeModel>(fakeContext);
            var expected = fakeBase[0];

            //Act
            var result = target.FindBy(model => model.Desc == "Desc1");

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FindFirst_Null()
        {
            //Arrange
            List<FakeModel> fakeBase = new List<FakeModel>();
            DbSet<FakeModel> fakeDbSet = CreateDbSetMock(fakeBase);
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            QueryRepository<FakeDbContext, FakeModel> target = new QueryRepository<FakeDbContext, FakeModel>(fakeContext);

            //Act
            var result = target.FindBy(model => model.Desc == "Desc1");

            //Assert
            Assert.IsNull(result);
        }

        //[TestMethod]
        //public void Add()
        //{
        //    //Arrange
        //    List<Rider> fakeBase = CreateFakeBase();
        //    DbSet<Rider> fakeDbSet = CreateDbSetMock(fakeBase);
        //    FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
        //    Repository<FakeDbContext> target = new Repository<FakeDbContext>(fakeContext);
        //    var newRider = new Rider
        //    {
        //        Id = 5,
        //        FirstName = "First5",
        //        LastName = "Last5",
        //        Country = "Country5",
        //        BirthDate = new DateTime(2015, 01, 05),
        //        Image = "Image5.png"
        //    };
        //    const int countExpected = 5;

        //    //Act
        //    target.Add(newRider);

        //    //Assert
        //    var result = fakeContext.Models.FirstOrDefault(rider => rider.Country == "Country5");
        //    Assert.IsNotNull(result, "Actual not exist");
        //    Assert.AreEqual(newRider.Id, result.Id, "Wrong Id");
        //    Assert.AreEqual(countExpected, fakeContext.Models.Count(), "Wrong Count");
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void Add_Null_Passed()
        //{
        //    //Arrange
        //    List<Rider> fakeBase = CreateFakeBase();
        //    DbSet<Rider> fakeDbSet = CreateDbSetMock(fakeBase);
        //    FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
        //    Repository<FakeDbContext> target = new Repository<FakeDbContext>(fakeContext);

        //    //Act
        //    //Assert
        //    target.Add(null);
        //}

        //[TestMethod]
        //public void Delete()
        //{
        //    //Arrange
        //    List<Rider> fakeBase = CreateFakeBase();
        //    DbSet<Rider> fakeDbSet = CreateDbSetMock(fakeBase);
        //    FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
        //    Repository<FakeDbContext> target = new Repository<FakeDbContext>(fakeContext);
        //    var riderToRemove = fakeContext.Models.FirstOrDefault(rider => rider.Country == "Country1");
        //    const int countExpected = 3;

        //    //Act
        //    target.Delete(riderToRemove);

        //    //Assert
        //    Assert.AreEqual(countExpected, fakeContext.Models.Count());
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void Delete_NullPassed()
        //{
        //    //Arrange
        //    List<Rider> fakeBase = CreateFakeBase();
        //    DbSet<Rider> fakeDbSet = CreateDbSetMock(fakeBase);
        //    FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
        //    Repository<FakeDbContext> target = new Repository<FakeDbContext>(fakeContext);

        //    //Act
        //    //Assert
        //    target.Delete(null);
        //}

        //[TestMethod]
        //public void Edit()
        //{
        //    //Arrange
        //    List<Rider> fakeBase = CreateFakeBase();
        //    DbSet<Rider> fakeDbSet = CreateDbSetMock(fakeBase);
        //    FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
        //    Repository<FakeDbContext> target = new Repository<FakeDbContext>(fakeContext);
        //    var riderToEdit = fakeContext.Models.FirstOrDefault(rider => rider.Country == "Country2");

        //    //Act
        //    riderToEdit.Country = "Finland";
        //    target.Edit(riderToEdit);

        //    //Assert
        //    var resultCollection = target.FindMany(rider => rider.Country == "Finland");
        //    var result = resultCollection.ToList()[0];
        //    Assert.AreEqual(riderToEdit.Country, result.Country);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void Edit_NullPassed()
        //{
        //    //Arrange
        //    List<Rider> fakeBase = CreateFakeBase();
        //    DbSet<Rider> fakeDbSet = CreateDbSetMock(fakeBase);
        //    FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
        //    Repository<FakeDbContext> target = new Repository<FakeDbContext>(fakeContext);

        //    //Act
        //    //Assert
        //    target.Edit(null);
        //}

        private static FakeDbContext CreateContextMock(DbSet<FakeModel> dbSetMock)
        {
            var contextMock = new Mock<FakeDbContext>();
            contextMock.Setup(context => context.Set<FakeModel>()).Returns(dbSetMock);
            contextMock.Setup(context => context.Models).Returns(dbSetMock);
            return contextMock.Object;
        }

        private static DbSet<FakeModel> CreateDbSetMock(List<FakeModel> ridersList)
        {
            var riders = ridersList.AsQueryable();
            var dbSetMock = new Mock<DbSet<FakeModel>>();
            dbSetMock.As<IQueryable<FakeModel>>().Setup(set => set.ElementType).Returns(riders.ElementType);
            dbSetMock.As<IQueryable<FakeModel>>().Setup(set => set.Expression).Returns(riders.Expression);
            dbSetMock.As<IQueryable<FakeModel>>().Setup(set => set.Provider).Returns(riders.Provider);
            dbSetMock.As<IQueryable<FakeModel>>().Setup(set => set.GetEnumerator()).Returns(riders.GetEnumerator());
            dbSetMock.Setup(set => set.Add(It.IsAny<FakeModel>())).Callback<FakeModel>(rider => ridersList.Add(rider));
            dbSetMock.Setup(set => set.Remove(It.IsAny<FakeModel>())).Callback<FakeModel>(rider => ridersList.Remove(rider));
            return dbSetMock.Object;
        }

        private static List<FakeModel> CreateFakeBase()
        {
            return new List<FakeModel>
            {
                new FakeModel { Id = 1, Desc = "Desc1" },
                new FakeModel { Id = 2, Desc = "Desc2" },
                new FakeModel { Id = 3, Desc = "Desc3" },
                new FakeModel { Id = 4, Desc = "Desc4" },
            };
        }
    }
}
