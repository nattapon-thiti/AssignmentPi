using AssignmentPiSecurities.Controllers.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pi.Interfaces.Repositories.Users;
using Pi.Interfaces.Services.Users;
using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Users;
using Pi.Models.ResponseModels.Users;
using Pi.Services.User;
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

            // mock db pi user
            List<PiUser> mock_users = new List<PiUser>
                              {
                                  //new PiUser { Id=1, GivenName = "mana", Email = "mana@gmail.com" },
                                  //new PiUser { Id=2, GivenName = "manb", Email = "manb@gmail.com" },
                                  new PiUser { Id=3, GivenName = "manc", Email = "manc@gmail.com" },
                                  //new PiUser { Id=4, GivenName = "mana2", Email = "mana2@gmail.com" },
                                  new PiUser { Id=5, GivenName = "aManc2", Email = "aManc2@gmail.com" },
                                  //new PiUser { Id=6, GivenName = "manb2", Email = "manb2@gmail.com" },
                              };
            string searchPattern = "manc";

            _mockServices.Setup(s => s.GetUsers(searchPattern)).ReturnsAsync(mock_users);

            #endregion

            #region actual

            // call controller
            var result = await _userController.GetUsers(searchPattern);

            #endregion

            #region assert

            OkObjectResult okResult = (OkObjectResult)result;
            GetUserResponse value = (GetUserResponse)okResult.Value;

            Assert.AreEqual(value.IsSuccess, true);
            Assert.AreEqual(value.Message, null);

            List<PiUser> userList = value.Data.ToList();
            PiUser userAtIndex = userList[0];

            Assert.IsNotNull(value);
            Console.WriteLine(userAtIndex);

            Assert.IsNotNull(userList);
            Assert.AreEqual(2, userList.Count);
            Assert.AreEqual("manc", userList[0].GivenName);
            Assert.AreEqual("manc@gmail.com", userList[0].Email);
            Assert.AreEqual("aManc2", userList[1].GivenName);
            Assert.AreEqual("aManc2@gmail.com", userList[1].Email);

            #endregion
        }

        [TestMethod]
        public async Task GetUsersList_ReturnOk_WhenItemNotExists()
        {
            #region arrange
            List<PiUser> mock_users = new List<PiUser>
            {
                //new PiUser { Id=1, GivenName = "mana", Email = "mana@gmail.com" },
                //new PiUser { Id=2, GivenName = "manb", Email = "manb@gmail.com" },
                //new PiUser { Id=3, GivenName = "manc", Email = "manc@gmail.com" },
                //new PiUser { Id=4, GivenName = "mana2", Email = "mana2@gmail.com" },
                //new PiUser { Id=5, GivenName = "aManc2", Email = "aManc2@gmail.com" },
                //new PiUser { Id=6, GivenName = "manb2", Email = "manb2@gmail.com" },
            };
            string searchPattern = "zzz";
            _mockServices.Setup(s => s.GetUsers(searchPattern)).ReturnsAsync(mock_users);
            #endregion

            #region actual
            // call controller
            var result = await _userController.GetUsers(searchPattern);
            #endregion

            #region assert
            OkObjectResult okResult = (OkObjectResult)result;
            GetUserResponse value = (GetUserResponse)okResult.Value;

            Assert.AreEqual(value.IsSuccess, true);
            Assert.AreEqual(value.Message, null);

            List<PiUser> userList = value.Data.ToList();

            Assert.IsNotNull(value);
            Assert.AreEqual(0, userList.Count);
            #endregion
        }

        [TestMethod]
        public async Task GetUsersList_ReturnOk_WithAllUser_WithOutSearchAnyThings()
        {
            #region arrange

            // mock db pi user
            List<PiUser> mock_users = new List<PiUser>
                              {
                                  new PiUser { Id=1, GivenName = "mana", Email = "mana@gmail.com" },
                                  new PiUser { Id=2, GivenName = "manb", Email = "manb@gmail.com" },
                                  new PiUser { Id=3, GivenName = "manc", Email = "manc@gmail.com" }
                              };

            _mockServices.Setup(s => s.GetUsers(null)).ReturnsAsync(mock_users);

            #endregion

            #region actual
            // call controller
            var result = await _userController.GetUsers(null);
            #endregion

            #region assert
            OkObjectResult okResult = (OkObjectResult)result;
            GetUserResponse value = (GetUserResponse)okResult.Value;

            List<PiUser> userList = value.Data.ToList();
            PiUser userAtIndex = userList[0];

            Assert.IsNotNull(value);
            Console.WriteLine(userAtIndex);

            Assert.IsNotNull(userList);
            Assert.AreEqual(3, userList.Count);
            Assert.AreEqual("mana", userList[0].GivenName);
            Assert.AreEqual("mana@gmail.com", userList[0].Email);
            Assert.AreEqual("manb", userList[1].GivenName);
            Assert.AreEqual("manb@gmail.com", userList[1].Email);
            Assert.AreEqual("manc", userList[2].GivenName);
            Assert.AreEqual("manc@gmail.com", userList[2].Email);
            #endregion
        }

        [TestMethod]
        public async Task CreateUser_ReturnOk_WithEffectedUser()
        {
            #region arrange

            // mock db pi user
            PiUser mock_users = new PiUser
            {
                Id = 15,
                GivenName = "mana",
                Email = "mana@gmail.com"
            };

            UserCreateOrUpdateRequest mock_request = new UserCreateOrUpdateRequest()
            {
                GivenName = "mana",
                Email = "mana@gmail.com"
            };
            CreateUserResponse mock_response = new CreateUserResponse(true, mock_users, "User id 15 created");

            _mockServices.Setup(s => s.CreateOrUpdateUser(mock_request)).ReturnsAsync(mock_response);

            #endregion

            #region actual
            // call controller
            var result = await _userController.CreateOrUpdateUser(mock_request);
            #endregion

            #region assert
            OkObjectResult okResult = (OkObjectResult)result;
            CreateUserResponse value = (CreateUserResponse)okResult.Value;

            PiUser user = value.Data;

            Assert.IsNotNull(value);

            Assert.IsNotNull(user);
            Assert.AreEqual("mana", user.GivenName);
            Assert.AreEqual("mana@gmail.com", user.Email);
            Assert.AreEqual(value.Message, "User id 15 created");
            #endregion
        }

        [TestMethod]
        public async Task UpdateUser_ReturnOk_WithEffectedUser()
        {
            #region arrange

            // mock db pi user
            PiUser mock_users = new PiUser
            {
                Id = 15,
                GivenName = "mana15",
                Email = "mana15@gmail.com"
            };

            UserCreateOrUpdateRequest mock_request = new UserCreateOrUpdateRequest()
            {
                Id = 15,
                GivenName = "mana15",
                Email = "mana15@gmail.com"
            };
            CreateUserResponse mock_response = new CreateUserResponse(true, mock_users, "User id 15 updated");

            _mockServices.Setup(s => s.CreateOrUpdateUser(mock_request)).ReturnsAsync(mock_response);

            #endregion

            #region actual
            // call controller
            var result = await _userController.CreateOrUpdateUser(mock_request);
            #endregion

            #region assert
            OkObjectResult okResult = (OkObjectResult)result;
            CreateUserResponse value = (CreateUserResponse)okResult.Value;

            PiUser user = value.Data;

            Assert.IsNotNull(value);

            Assert.IsNotNull(user);
            Assert.AreEqual("mana15", user.GivenName);
            Assert.AreEqual("mana15@gmail.com", user.Email);
            Assert.AreEqual(value.Message, "User id 15 updated");
            #endregion
        }
    }
}
