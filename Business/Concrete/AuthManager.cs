using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concret.ErrorResults;
using Core.Utilities.Results.Concret.SuccessResults;
using Entities.Concrete;
using Entities.DTOs.AuthDTOs;
using Entities.DTOs.TokenDTOs;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Business.Concrete;

public class AuthManager : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    public AuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<IDataResult<Token>> LoginAsync(LoginDTO entity)
    {
        var findUser = await _userManager.FindByEmailAsync(entity.Email);

        if (findUser == null)
            return new ErrorDataResult<Token>(HttpStatusCode.NotFound);

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(findUser, entity.Password, true);

        if (result.Succeeded)
        {
            var userRoles = await _userManager.GetRolesAsync(findUser);
            Token token = _tokenService.CreateTokenAsync(findUser, userRoles.ToList());

            findUser.RefreshToken = token.RefreshToken;
            findUser.RefreshTokenExpire = DateTime.UtcNow.AddMonths(1);
            
            await _userManager.UpdateAsync(findUser);
            
            return new SuccessDataResult<Token>(HttpStatusCode.OK, token);
        }
        else
        {
            return new ErrorDataResult<Token>(HttpStatusCode.BadRequest, "Email or Password is not valid!");
        }
    }

    public async Task<IResult> LogoutAsync(string userId)
    {
        var findUser = await _userManager.FindByIdAsync(userId);
        if (findUser == null)
            return new ErrorResult(HttpStatusCode.NotFound);

        await _signInManager.SignOutAsync();

        findUser.RefreshToken = null;
        findUser.RefreshTokenExpire = null;
        await _userManager.UpdateAsync(findUser);

        return new SuccessResult(HttpStatusCode.OK);
    }

    public async Task<IDataResult<Token>> RefreshLoginAsync(string refreshToken)
    {
        var findUser = _userManager.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);
        if (findUser == null)
            return new ErrorDataResult<Token>(HttpStatusCode.NotFound);

        if(findUser.RefreshTokenExpire > DateTime.UtcNow)
        {
            return new ErrorDataResult<Token>(HttpStatusCode.BadRequest, "Expired RefreshToken Time!");
        }

        var userRoles = await _userManager.GetRolesAsync(findUser);

        Token token = _tokenService.CreateTokenAsync(findUser, userRoles.ToList());
        token.RefreshToken = refreshToken;

        return new SuccessDataResult<Token>(HttpStatusCode.OK, token);
    }

    public async Task<IResult> RegisterAsync(RegisterDTO entity)
    {
        AppUser appUser = new()
        {
            FirstName = entity.FirstName,
            LastName = entity.SurName,
            Email = entity.Email,
            UserName = entity.Email
        };

        IdentityResult result = await _userManager.CreateAsync(appUser, entity.Password);
        if (result.Succeeded)
        {
            return new SuccessResult(HttpStatusCode.Created);
        }
        else
        {
            string response = string.Empty;

            foreach (var error in result.Errors)
            {
                response += error.Description;
            }

            return new ErrorResult(HttpStatusCode.BadRequest, response);
        }
    }

}
