using MakFood.Authentication.Application.Contracts;
using MakFood.Authentication.Domain.Model.Contracts;
using MakFood.Authentication.Domain.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MakFood.Authentication.Host.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwsService _jwsService;
        private readonly IUserRepository _userRepository;

        public AuthController(IJwsService jwsService, IUserRepository userRepository)
        {
            _jwsService = jwsService;
            _userRepository = userRepository;
        }

       
        /// <summary>
        /// لاگ‌اوت: حذف JWS (revocation)؛ باید هدر Authorization شامل Bearer token باشد.
        /// </summary>
        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return BadRequest(new { message = "No bearer token found in Authorization header." });

            var tokenString = authHeader.Substring("Bearer ".Length).Trim();
            if (string.IsNullOrEmpty(tokenString))
                return BadRequest(new { message = "Token is empty." });

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest(new { message = "User identifier not found in token." });

            var user = await _userRepository.GetUserByIdAsync(userId, cancellationToken);
            if (user == null)
                return BadRequest(new { message = "User not found." });

            await _jwsService.DeleteJwsToken(user, tokenString);

            return Ok(new { message = "Logged out successfully." });
        }
    }
}
