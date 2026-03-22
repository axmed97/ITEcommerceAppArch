using Entities.Concrete;
using Entities.DTOs.TokenDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract;

public interface ITokenService
{
    Token CreateTokenAsync(AppUser appUser, List<string> roles);
}
