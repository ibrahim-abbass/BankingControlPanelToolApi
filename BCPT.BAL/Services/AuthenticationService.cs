using BCPT.ABSTACTION;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace BCPT.BAL
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthenticationService(
            IOptions<JwtOptions> jwtOptions,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this._jwtOptions = jwtOptions.Value ??
                throw new ArgumentNullException(nameof(jwtOptions), "jwt options cannot be null.");

            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        #region Public
        public async Task<RegisterResponse> Register(RegisterRequest registerRequest)
        {
            var userExistByEmail = await _userManager.FindByEmailAsync(registerRequest.Email);
            if (userExistByEmail != null)
                return new RegisterResponse()
                {
                    Code = HttpStatusCode.Forbidden,
                    Message = string.Format(ErrorMessage.UserExist, "Email"),
                    Status = Status.Error
                };

            var userExistByUserName = await _userManager.FindByNameAsync(registerRequest.Username);
            if (userExistByUserName != null)
                return new RegisterResponse()
                {
                    Code = HttpStatusCode.Forbidden,
                    Message = string.Format(ErrorMessage.UserExist, "Username"),
                    Status = Status.Error
                };

            if (await _roleManager.RoleExistsAsync(registerRequest.Role.ToString()))
            {
                IdentityUser user = new IdentityUser()
                {
                    Email = registerRequest.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerRequest.Username
                };

                var result = await _userManager.CreateAsync(user, registerRequest.Password);

                if (!result.Succeeded)
                    return new RegisterResponse()
                    {
                        Code = HttpStatusCode.InternalServerError,
                        Message = ErrorMessage.UserCreationFailed,
                        Status = Status.Error,
                    };

                result = await _userManager.AddToRoleAsync(user, registerRequest.Role.ToString());

                return new RegisterResponse()
                {
                    Code = HttpStatusCode.Created,
                    Message = string.Format(SuccessMessage.UserCreated, "User"),
                    Status = Status.Success,
                };
            }
            else
            {
                var message = string.Format(ErrorMessage.RoleNotExist, registerRequest.Role);
                return new RegisterResponse()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = message,
                    Status = Status.Error,
                };
            }
        }
        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var userRoles = await _userManager.GetRolesAsync(user);

                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtToken = GetToken(authClaims);

                return new LoginResponse()
                {
                    Code = HttpStatusCode.OK,
                    Status = Status.Success,
                    Message = SuccessMessage.Loginsuccessfully,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    Expiration = jwtToken.ValidTo
                };
            }

            return new LoginResponse()
            {
                Code = HttpStatusCode.Unauthorized,
                Message = ErrorMessage.Unauthorized,
                Status = Status.Error,
            };
        }
        #endregion

        #region Private
        private JwtSecurityToken GetToken(List<Claim> claims)
        {
            var authSingingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));

            return new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: DateTime.Now.AddHours(_jwtOptions.Expiration),
                claims: claims,
                signingCredentials: new SigningCredentials(authSingingKey, SecurityAlgorithms.HmacSha256));
        }
        #endregion
    }
}