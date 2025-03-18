using GestionDeTareas.Core.Application.DTos.Account.Authenticate;
using GestionDeTareas.Core.Application.DTos.Account.JWT;
using GestionDeTareas.Core.Application.DTos.Account.Register;
using GestionDeTareas.Core.Application.Interfaces.Service;
using GestionDeTareas.Core.Domain.Settings;
using GestionDeTareas.Core.Domain.Utils;
using GestionDeTareas.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GestionDeTareas.Core.Application.DTos.Account;

namespace GestionDeTareas.Infrastructure.Identity.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JWTSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        private JWTSettings _jwtSettings { get; }


        public async Task<ApiResponse<RegisterResponse>> RegisterAccountAsync(RegisterRequest request, string roles)
        {
            var userWitUsername = await _userManager.FindByNameAsync(request.Username);
            if (userWitUsername != null)
                return ApiResponse<RegisterResponse>.ErrorResponse($"this user {userWitUsername} is already taken");

            var userWithEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithEmail != null)
                return ApiResponse<RegisterResponse>.ErrorResponse($"this email {userWithEmail} is already taken");

            User user = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user,request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user,roles);
                RegisterResponse response = new
                (
                    UserId: user.Id,
                    Username: user.UserName,
                    Email: user.Email
                );

                return ApiResponse<RegisterResponse>.SuccessResponse(response);
            }
            else
            {
                return ApiResponse<RegisterResponse>.ErrorResponse("An error occured trying to registed the user");
            }

        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request)
        {
            AuthenticateResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.StatusCodes = 404;
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName,request.Password,false,lockoutOnFailure: false);
            
            if (!result.Succeeded)
            {
                response.StatusCodes = 401;
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.StatusCodes = 400;
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            response.Id = user.Id;
            response.Username = user.UserName;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.Email = user.Email;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.PhoneNumber = user.PhoneNumber;
            response.JWTToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        public async Task<IEnumerable<AccountDTos>> GetAllAsync()
        {
            var users = _userManager.Users.ToList();
            IEnumerable<AccountDTos> usersList = users.Select(x => new AccountDTos
            (
                UserId: x.Id,
                FirstName: x.FirstName,
                LastName: x.LastName,
                Email: x.Email,
                Username: x.UserName,
                PhoneNumber: x.PhoneNumber
            ));
            
            return usersList;
        }

        #region Private Methods
        private async Task<JwtSecurityToken> GenerateToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            List<Claim> rolesClaims = new List<Claim>();

            foreach (var role in roles)
            {
                rolesClaims.Add(new Claim("roles", role));
            }

            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id",user.Id)
            }
            .Union(userClaims)
            .Union(rolesClaims);

            //Gemerate symmetric security key
            var symmectricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredetials = new SigningCredentials(symmectricSecurity,SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken
            (
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claim,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredetials
            );

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expired = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = RandomNumberGenerator.Create();
            var randomByte = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomByte);

            return BitConverter.ToString(randomByte).Replace("-", "");
        }

        #endregion

    }
}
