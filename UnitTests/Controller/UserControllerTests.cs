using AssignmentPiSecurities.Controllers.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pi.Interfaces.Repositories.Users;
using Pi.Interfaces.Services.Users;
using Pi.Models.Entities.PI;
using Pi.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Controller
{
    [TestClass()]
    public class UserControllerTests
    {
        private readonly Mock<IUserServices> _mockServices;
        private readonly UsersController _userController;
        //[TestInitialize]
        public UserControllerTests()
        {
            _mockServices = new Mock<IUserServices>();
            _userController = new UsersController(_mockServices.Object);
        }

        [TestMethod]
        public async Task GetUsersList_ReturnOk_WhenItemExists()
        {
            #region arrange
            _mockServices.Setup(s => s.GetUsers()).ReturnsAsync(new List<PiUser>
                              {
                                  new PiUser { Id=1, GivenName = "mana", Email = "mana@gmail.com" },
                                  new PiUser { Id=1, GivenName = "manb", Email = "manb@gmail.com" }
                              });
            var result = await _userController.GetUsers();
            #endregion

            #region actual

            #endregion

            #region assert

            OkObjectResult okResult = (OkObjectResult)result;
            List<PiUser> value = (List<PiUser>)okResult.Value;
            Assert.IsNotNull(value);
            Assert.AreEqual(2, value.Count);
            Assert.AreEqual("mana", value[0].GivenName);
            Assert.AreEqual("mana@gmail.com", value[0].Email);
            Assert.AreEqual("manb", value[1].GivenName);
            Assert.AreEqual("manb@gmail.com", value[1].Email);

            #endregion
        }
    }
}
