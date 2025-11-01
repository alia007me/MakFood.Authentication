using MakFood.Authentication.Application.Contracts;
using MakFood.Authentication.Domain.Model.Contracts;
using MakFood.Authentication.Domain.Model.Entities;
using MakFood.Authentication.Infraustraucture.Substructure.Utils.JwsInformation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Service.JwsService
{
    public class JwsService : IJwsService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly JwsInformationOptions _jwsOptions;

        public JwsService(IUserRepository userRepository, IGroupRepository groupRepository, IPermissionRepository permissionRepository, IOptions<JwsInformationOptions> jwsOptions)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _permissionRepository = permissionRepository;
            _jwsOptions = jwsOptions.Value;
        }

        public async Task<string> CreateJWSToken(User user, CancellationToken cancellationToken)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwsOptions.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name , user.Username),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var userGroups = await _userRepository.GetAllUserGroupsAsync(user.Id,cancellationToken);
            var groupPermissions = await _groupRepository.GetAllGroupPermissionAsync(userGroups, cancellationToken);
            var permissions = await _permissionRepository.GetAllPermissionsInOneGroupAsync(groupPermissions, cancellationToken);

            foreach(var permission in permissions) 
                claims.Add(new Claim("permission",permission.Key));

            var token = new JwtSecurityToken(
                issuer: _jwsOptions.Issuer,
                audience: _jwsOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(int.Parse(_jwsOptions.Expires)),
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
