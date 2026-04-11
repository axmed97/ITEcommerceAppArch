using Core.Utilities.Results.Abstract;
using Entities.DTOs.AuthDTOs;
using Entities.DTOs.TokenDTOs;

namespace Business.Abstract;

public interface IAuthService
{
    Task<IResult> RegisterAsync(RegisterDTO entity);
    Task<IDataResult<Token>> LoginAsync(LoginDTO entity);
    Task<IDataResult<Token>> RefreshLoginAsync(string refreshToken);
    Task<IResult> LogoutAsync(string userId);
    Task<IResult> AssignRoleAsync(AssignRoleDTO entity);
    Task<IResult> RemoveRoleAsync(RemoveRoleDTO entity);
}
