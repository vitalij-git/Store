using API.Dto;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Interfaces;
using Core.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenSevice;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenSevice, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenSevice = tokenSevice;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipalAsync(User);

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenSevice.CreateToken(user),
                DisplayName = user.DisplayName,
            };
        }

        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("userinfo")]
        public async Task<ActionResult<UserInfoDto>> GetUserInfo()
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithUserInfoAsync(User);
            return _mapper.Map<UserInfo, UserInfoDto>(user.UserInfo);
        }

        [Authorize]
        [HttpPut("userinfo")]
        public async Task<ActionResult<UserInfoDto>> UpdateUserInfo(UserInfoDto userInfo)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithUserInfoAsync(User);
            user.UserInfo = _mapper.Map<UserInfoDto, UserInfo>(userInfo);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) 
            {
                return Ok(_mapper.Map<UserInfo, UserInfoDto>(user.UserInfo));
            }
            return BadRequest("Problem with updating the user");

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) 
            { 
                return Unauthorized(new ApiResponse(401));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenSevice.CreateToken(user),
                DisplayName = user.DisplayName,
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
           
            if (CheckEmailExistAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });
            }
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded) 
            {
                return BadRequest(new ApiResponse(400));
            }
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenSevice.CreateToken(user),
                Email = user.Email,
            };
        }


    }
}
