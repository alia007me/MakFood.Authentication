
using MakFood.Authentication.Infraustraucture.Contract;
using MakFood.FBI.Middleware;
using MakFood.FBI.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MakFood.Authentication.UnitTest
{
    public class FBIPackageTest
    {
        [Fact]
        public async Task Authorization_Checks_Header_Token_Validation()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "Token";
            RequestDelegate next = (ctx) =>
            {
                return Task.CompletedTask;
            };
            var redisMock = new Mock<IRedisCache>();
            var options = Options.Create(new JwsLocalOptions("Key","Issuer","Audience"));
            var sut = new AuthorizationMiddleware(next, redisMock.Object, options);



            //Act
            await sut.InvokeAsync(httpContext);



            //assert
            Assert.Equal(StatusCodes.Status401Unauthorized, httpContext.Response.StatusCode);

        }
        [Fact]
        public async Task Authorization_Checks_Header_Token_Existance()
        {
            //arrange
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "";
            RequestDelegate next = (ctx) =>
            {
                return Task.CompletedTask;
            };
            var redisMock = new Mock<IRedisCache>();
            var options = Options.Create(new JwsLocalOptions("Key", "Issuer", "Audience"));
            var sut = new AuthorizationMiddleware(next, redisMock.Object, options);

            //act
            await sut.InvokeAsync(httpContext);


            //assert

            Assert.Equal(StatusCodes.Status403Forbidden, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Authorization_Checks_Header_Token_And_Let_The_Process_Going()
        {
            //Arrange
            var key = "ThisIsASuperStrongSecretKeyForJwt123456!";
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"ali"),
                new Claim(ClaimTypes.NameIdentifier, "1")

            };
            var fakeToken = new JwtSecurityToken(
                issuer: "Issuer",
                audience: "Audience",
                claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256));
            var fakeTokenGenerated = new JwtSecurityTokenHandler().WriteToken(fakeToken);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "Jws " + fakeTokenGenerated;
            RequestDelegate next = (ctx) =>
            {
                return Task.CompletedTask;
            };
            var options = Options.Create(new JwsLocalOptions("ThisIsASuperStrongSecretKeyForJwt123456!", "Issuer", "Audience"));
            var redisMock = new Mock<IRedisCache>();
            redisMock.Setup(x => x.CheckKeyExistanceInRedis(It.IsAny<string>())).ReturnsAsync(true);
            var sut = new AuthorizationMiddleware(next, redisMock.Object, options);



            //Act
            await sut.InvokeAsync(httpContext);


            //Assert
            Assert.True(httpContext.User.Identity.IsAuthenticated);


        }
    }
}
