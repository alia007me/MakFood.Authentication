using MakFood.Authentication.Application.Contracts;
using MakFood.Authentication.Domain.Model.Entities;
using MakFood.Authentication.Domain.Model.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.UnitTest
{
    public class TestJwsService
    {
        [Fact]
        public async Task Get_Jws_Token()
        {
            //Arrange
            var sut = new Mock<IJwsService>();
            var user = new User("ali", "bbbb4444", "ali@gmail.com", "09123456789", Role.Customer);
            sut.Setup(x => x.CreateJwsToken(user, It.IsAny<CancellationToken>()))
                .ReturnsAsync($"Mocked Token For {user.Username}");


            //Act
            var result = await sut.Object.CreateJwsToken(user, CancellationToken.None);


            //Assert
            Assert.Contains(user.Username,result);
        }
    }
}
