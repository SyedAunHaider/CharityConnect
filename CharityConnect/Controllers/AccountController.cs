using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CharityConnect.Backend.BusinessAccess;
using CharityConnect.Backend.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CharityConnect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private ILogger logger;
        private readonly UserManager<AppUser> _user;
        private readonly SignInManager<AppUser> _signInManager;
        private ApiResponse<IdentityResult> _apiResponse = new ApiResponse<IdentityResult>();

        public AccountController(ILogger<AccountController> logger, UserManager<AppUser> user, SignInManager<AppUser> signInManager)
        {
            this.logger = logger;
            _user = user;
            _signInManager = signInManager;
        }
        [HttpPost]
        public async Task<ApiResponse<IdentityResult>> Register(UserRegistration model)
        {

            if (ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    UserName = model.UserName,
                    CNIC = model.CNIC,
                    MobileNo = model.MobileNo,
                    CharityDistributionDate = model.CharityDistributionDate,
                    CreationDate = DateTime.Now,
                    PhoneNumber = model.MobileNo,
                    FamilyMembersCount = model.FamilyMembersCount,
                    PConstituencyId = model.PConstituencyId
                };
                var result = await _user.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(user.Id))
                    {
                        _apiResponse.token = createToken(user.Id.ToString());
                        _apiResponse.Data = result;
                    }
                }

                _apiResponse.token = null;
                _apiResponse.Data = result;

                return _apiResponse;
            }
            else
                return null;


        }

        private string createToken(string Id)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Id)
                }),
                Expires = DateTime.UtcNow.AddSeconds(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var returnToken = tokenHandler.WriteToken(token);
            return returnToken;
        }
    }
}