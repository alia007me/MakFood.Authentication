using MakFood.Authentication.Application.Contracts;
using MakFood.Authentication.Domain.Model.Contracts;
using MakFood.Authentication.Domain.Model.Entities;
using MakFood.Authentication.Infraustraucture.Substructure.Utils.JwsInformation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Service.JwsService
{
    public class JwsService : IJwsService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly JwsInformationOptions _jwsOptions;
        private readonly IConnectionMultiplexer _redis;

        public JwsService(
            IUserRepository userRepository,
            IGroupRepository groupRepository,
            IPermissionRepository permissionRepository,
            IOptions<JwsInformationOptions> jwsOptions,
            IConnectionMultiplexer redis)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _permissionRepository = permissionRepository;
            _jwsOptions = jwsOptions.Value;
            _redis = redis;
        }

        public async Task<string> CreateJwsToken(User user, CancellationToken cancellationToken)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwsOptions.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jti = Guid.NewGuid().ToString();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var userGroups = await _userRepository.GetAllUserGroupsAsync(user.Id, cancellationToken);
            var groupPermissions = await _groupRepository.GetAllGroupPermissionAsync(userGroups, cancellationToken);
            var permissions = await _permissionRepository.GetAllPermissionsInOneGroupAsync(groupPermissions, cancellationToken);

            foreach (var permission in permissions)
                claims.Add(new Claim("permission", permission.Key));

            var expiresInHours = int.Parse(_jwsOptions.Expires);
            var expiresAt = DateTime.UtcNow.AddHours(expiresInHours);

            var token = new JwtSecurityToken(
                issuer: _jwsOptions.Issuer,
                audience: _jwsOptions.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: signingCredentials
            );

            var jwsToken = new JwtSecurityTokenHandler().WriteToken(token);
            var db = _redis.GetDatabase();
            var redisKey = $"Jws:{user.Id}:{jti}";
            await db.StringSetAsync(redisKey, "1", TimeSpan.FromHours(expiresInHours));

            return jwsToken;
        }

        public async Task DeleteJwsToken(User user, string jwsToken)
        {
            if (user == null || string.IsNullOrWhiteSpace(jwsToken)) return;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(jwsToken);
                var jti = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
                if (string.IsNullOrEmpty(jti)) return;

                var db = _redis.GetDatabase();
                var redisKey = $"Jws:{user.Id}:{jti}";
                await db.KeyDeleteAsync(redisKey);
            }
            catch
            {
            }
        }
    }
}
