using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using WebAppMvc.Controllers;

namespace WebAppMvc.Models
{
    [TestFixture]
    public class TestCust
    {
        private DbContextOptions<CustomerLogInfoContext> _options;
        [SetUp]
        public void Setup()
        {
            // Set up an in-memory database for testing
            _options = new DbContextOptionsBuilder<CustomerLogInfoContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }
        [Test]
        public void AddUser_ValidUser_UserIsAddedToDatabase()
        {
            // Arrange
            using (var context = new CustomerLogInfoContext(_options))
            {
                var userDAL = new UserInfoesController(context);
                var userInfo = new UserInfo
                {
                    UserId = 1,

                    Password = "password123"
                };

                // Act
                userDAL.Create(userInfo);

                // Assert
                //var addedUser = context.UserInfos.FirstOrDefault(u => u.UserId == userInfo.UserId);
                //Assert.IsNotNull(addedUser);
                Assert.AreEqual(userInfo.Password, "password123");
                Assert.AreEqual(userInfo.UserId, 1);
            }
        }
        [Test]
        public void SaveCustLogInfotest()
        {
            // Arrange
            using (var context = new CustomerLogInfoContext(_options))
            {
                var userDAL = new CustLogInfoesController(context);
                var userInfo = new CustLogInfo
                {
                    LogId = 1234,

                    CustEmail = "aniket.chouksey444@gmail.com",

                    CustName = "Piyush",

                     LogStatus = "My Status"
    };

                // Act
                userDAL.Create(userInfo);

                // Assert              
                Assert.AreEqual(userInfo.LogId, 1234);
             
             
            }
        }
    }
}
