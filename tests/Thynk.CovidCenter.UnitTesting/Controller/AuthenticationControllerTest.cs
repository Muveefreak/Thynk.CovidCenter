using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System.Threading.Tasks;
using Thynk.CovidCenter.API.Controllers;
using Thynk.CovidCenter.Core.DTOs;
using Thynk.CovidCenter.Core.Interface;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Core.ResponseModel;
using Xunit;

namespace Thynk.CovidCenter.UnitTesting.Controller
{
    public class AuthenticationControllerTest
    {
        private AuthenticationController sut;
        private Mock<IAuthenticationService> authenticationMock = new Mock<IAuthenticationService>();

        public AuthenticationControllerTest()
        {
            sut = new AuthenticationController(authenticationMock.Object)
            {
            };
        }

        //[Fact]
        //public async Task AuthenticateUser_Should_Return_OkObjectResult_For_Valid_Input()
        //{
        //    //Arrange
        //    string userId = "111111";

        //    GenericResponse<UserDTO> response = new GenericResponse<UserDTO>()
        //    {
        //        Status = true
        //    };

        //    authenticationMock.Setup(o => o.Authenticate(It.IsAny<AuthenticationRequest>())).ReturnsAsync(() => (response));
        //    //Act
        //    var result = await sut.Authenticate(new AuthenticationRequest());

        //    Assert.IsType<OkObjectResult>(result);
        //    result.ShouldNotBeNull();

        //}

    }
}
