using Business.Abstract;
using Business.Validators.AuthValidators;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concret;
using Core.Utilities.Results.Concret.ErrorResults;
using Core.Utilities.Results.Concret.SuccessResults;
using Entities.Concrete;
using Entities.DTOs.AuthDTOs;
using Entities.DTOs.TokenDTOs;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using System.Net;

namespace Business.Concrete;
// SQL - Indexer
public class AuthManager : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly RoleManager<AppRole> _roleManager;

    public AuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _roleManager = roleManager;
    }

    public async Task<IResult> AssignRoleAsync(AssignRoleDTO entity)
    {
        var findUser = await _userManager.FindByIdAsync(entity.UserId);

        if (findUser == null)
            return new ErrorResult(HttpStatusCode.NotFound);

        var userRoles = await _userManager.GetRolesAsync(findUser);

        if (userRoles.Contains(entity.Role))
            return new ErrorResult(HttpStatusCode.Conflict);

        IdentityResult result = await _userManager.AddToRoleAsync(findUser, entity.Role);

        if (result.Succeeded)
            return new SuccessResult(HttpStatusCode.OK);
        else
        {
            string responseMessage = string.Empty;

            foreach (var error in result.Errors)
            {
                responseMessage += error.Description;
            }

            return new ErrorResult(HttpStatusCode.BadRequest, responseMessage);
        }
            
    }

    public async Task<IDataResult<Token>> LoginAsync(LoginDTO entity)
    {
        var validator = new LoginValidator();
        var validate = await validator.ValidateAsync(entity);

        if (!validate.IsValid)
        {
            string message = string.Empty;
            foreach (var error in validate.Errors)
            {
                message += error.ErrorMessage;
            }
            return new ErrorDataResult<Token>(HttpStatusCode.BadRequest, message.ToString());
        }

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

    // [ValidationAspect(typeof(RegisterValidator))]
    public async Task<IResult> RegisterAsync(RegisterDTO entity)
    {
        var validator = new RegisterValidator();
        var validate = await validator.ValidateAsync(entity);

        if (!validate.IsValid)
        {
            string message = string.Empty;
            foreach (var error in validate.Errors)
            {
                message += error.ErrorMessage;
            }
            return new ErrorResult(HttpStatusCode.BadRequest, message.ToString());
        }

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

    public async Task<IResult> RemoveRoleAsync(RemoveRoleDTO entity)
    {
        var findUser = await _userManager.FindByIdAsync(entity.UserId);

        if (findUser == null)
            return new ErrorResult(HttpStatusCode.NotFound);


        var findRole = await _roleManager.GetRoleNameAsync(new AppRole()
        {
            Name = entity.Role
        });

        if(findRole == null)
            return new ErrorResult(HttpStatusCode.NotFound);

        var result =await _userManager.RemoveFromRoleAsync(findUser, entity.Role);

        if (result.Succeeded)
        {
            return new SuccessResult(HttpStatusCode.OK);
        }
        else
        {
            string responseMessage = string.Empty;

            foreach (var error in result.Errors)
            {
                responseMessage += error.Description;
            }

            return new ErrorResult(HttpStatusCode.BadRequest, responseMessage);
        }
    }
}
