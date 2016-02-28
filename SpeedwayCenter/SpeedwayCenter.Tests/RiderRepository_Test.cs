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
    public class RiderRepository_Test
    {
        [TestMethod]
        public void GetAll()
        {
            //Arrange
            List<Rider> fakeBase = CreateFakeBase();
            DbSet<Rider> fakeDbSet = CreateDbSetMock(fakeBase);
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            RiderRepository<FakeDbContext> target = new RiderRepository<FakeDbContext>(fakeContext);
            
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
            DbSet<Rider> fakeDbSet = CreateDbSetMock(new List<Rider>());
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            RiderRepository<FakeDbContext> target = new RiderRepository<FakeDbContext>(fakeContext);
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
            List<Rider> fakeBase = CreateFakeBase();
            DbSet<Rider> fakeDbSet = CreateDbSetMock(fakeBase);
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            RiderRepository<FakeDbContext> target = new RiderRepository<FakeDbContext>(fakeContext);
            var entityExpected = fakeBase.ToList()[0];

            //Act
            var resultCollection = target.FindBy(rider => rider.Country == "Country1");
            var result = resultCollection.ToList()[0];

            //Assert
            Assert.AreEqual(entityExpected.Id, result.Id, "Wrong Id");
            Assert.AreEqual(entityExpected.FirstName, result.FirstName, "Wrong FirstName");
            Assert.AreEqual(entityExpected.LastName, result.LastName, "Wrong LastName");
            Assert.AreEqual(entityExpected.BirthDate, result.BirthDate, "Wrong BirthDate");
            Assert.AreEqual(entityExpected.Country, result.Country, "Wrong Country");
            Assert.AreEqual(entityExpected.Image, result.Image, "Wrong Image");
        }

        [TestMethod]
        public void FindBy_Many()
        {
            //Arrange
            List<Rider> fakeBase = CreateFakeBase();
            DbSet<Rider> fakeDbSet = CreateDbSetMock(fakeBase);
            fakeDbSet.Add(fakeBase.ToList()[0]);
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            RiderRepository<FakeDbContext> target = new RiderRepository<FakeDbContext>(fakeContext);
            var entityExpected = fakeBase.Where(rider => rider.Country == "Country1").Select(rider => rider);

            //Act
            var result = target.FindBy(rider => rider.Country == "Country1");

            //Assert
            Assert.AreEqual(entityExpected.Count(), result.Count());
        }

        [TestMethod]
        public void FindBy_Null()
        {
            //Arrange
            DbSet<Rider> fakeDbSet = CreateDbSetMock(new List<Rider>());
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            RiderRepository<FakeDbContext> target = new RiderRepository<FakeDbContext>(fakeContext);
            const int expectedCount = 0;

            //Act
            var result = target.FindBy(rider => rider.Country == "Country1");

            //Assert
            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void Add()
        {
            //Arrange
            List<Rider> fakeBase = CreateFakeBase();
            DbSet<Rider> fakeDbSet = CreateDbSetMock(fakeBase);
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            RiderRepository<FakeDbContext> target = new RiderRepository<FakeDbContext>(fakeContext);
            var newRider = new Rider
            {
                Id = 5,
                FirstName = "First5",
                LastName = "Last5",
                Country = "Country5",
                BirthDate = new DateTime(2015, 01, 05),
                Image = "Image5.png"
            };
            const int countExpected = 5;

            //Act
            target.Add(newRider);

            //Assert
            var result = fakeContext.Riders.FirstOrDefault(rider => rider.Country == "Country5");
            Assert.IsNotNull(result, "Actual not exist");
            Assert.AreEqual(newRider.Id, result.Id, "Wrong Id");
            Assert.AreEqual(countExpected, fakeContext.Riders.Count(), "Wrong Count");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_Null_Passed()
        {
            //Arrange
            List<Rider> fakeBase = CreateFakeBase();
            DbSet<Rider> fakeDbSet = CreateDbSetMock(fakeBase);
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            RiderRepository<FakeDbContext> target = new RiderRepository<FakeDbContext>(fakeContext);

            //Act
            //Assert
            target.Add(null);
        }

        [TestMethod]
        public void Delete()
        {
            //Arrange
            List<Rider> fakeBase = CreateFakeBase();
            DbSet<Rider> fakeDbSet = CreateDbSetMock(fakeBase);
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            RiderRepository<FakeDbContext> target = new RiderRepository<FakeDbContext>(fakeContext);
            var riderToRemove = fakeContext.Riders.FirstOrDefault(rider => rider.Country == "Country1");
            const int countExpected = 3;

            //Act
            target.Delete(riderToRemove);

            //Assert
            Assert.AreEqual(countExpected, fakeContext.Riders.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullPassed()
        {
            //Arrange
            List<Rider> fakeBase = CreateFakeBase();
            DbSet<Rider> fakeDbSet = CreateDbSetMock(fakeBase);
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            RiderRepository<FakeDbContext> target = new RiderRepository<FakeDbContext>(fakeContext);

            //Act
            //Assert
            target.Delete(null);
        }

        [TestMethod]
        public void Edit()
        {
            //Arrange
            List<Rider> fakeBase = CreateFakeBase();
            DbSet<Rider> fakeDbSet = CreateDbSetMock(fakeBase);
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            RiderRepository<FakeDbContext> target = new RiderRepository<FakeDbContext>(fakeContext);
            var riderToEdit = fakeContext.Riders.FirstOrDefault(rider => rider.Country == "Country2");

            //Act
            riderToEdit.Country = "Finland";
            target.Edit(riderToEdit);

            //Assert
            var resultCollection = target.FindBy(rider => rider.Country == "Finland");
            var result = resultCollection.ToList()[0];
            Assert.AreEqual(riderToEdit.Country, result.Country);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Edit_NullPassed()
        {
            //Arrange
            List<Rider> fakeBase = CreateFakeBase();
            DbSet<Rider> fakeDbSet = CreateDbSetMock(fakeBase);
            FakeDbContext fakeContext = CreateContextMock(fakeDbSet);
            RiderRepository<FakeDbContext> target = new RiderRepository<FakeDbContext>(fakeContext);

            //Act
            //Assert
            target.Edit(null);
        }

        private static FakeDbContext CreateContextMock(DbSet<Rider> dbSetMock)
        {
            var contextMock = new Mock<FakeDbContext>();
            contextMock.Setup(context => context.Set<Rider>()).Returns(dbSetMock);
            contextMock.Setup(context => context.Riders).Returns(dbSetMock);
            return contextMock.Object;
        }

        private static DbSet<Rider> CreateDbSetMock(List<Rider> ridersList)
        {
            var riders = ridersList.AsQueryable();
            var dbSetMock = new Mock<DbSet<Rider>>();
            dbSetMock.As<IQueryable<Rider>>().Setup(set => set.ElementType).Returns(riders.ElementType);
            dbSetMock.As<IQueryable<Rider>>().Setup(set => set.Expression).Returns(riders.Expression);
            dbSetMock.As<IQueryable<Rider>>().Setup(set => set.Provider).Returns(riders.Provider);
            dbSetMock.As<IQueryable<Rider>>().Setup(set => set.GetEnumerator()).Returns(riders.GetEnumerator());
            dbSetMock.Setup(set => set.Add(It.IsAny<Rider>())).Callback<Rider>(rider => ridersList.Add(rider));
            dbSetMock.Setup(set => set.Remove(It.IsAny<Rider>())).Callback<Rider>(rider => ridersList.Remove(rider));
            return dbSetMock.Object;
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
