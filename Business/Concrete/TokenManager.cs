using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs.TokenDTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Business.Concrete;

public class TokenManager : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Token CreateTokenAsync(AppUser appUser, List<string> roles)
    {
        Token token = new();

        List<Claim> claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, appUser.Id),
            new Claim(ClaimTypes.NameIdentifier, appUser.Id)
        };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecretKey"]!));

        token.AccessTokenExpire = DateTime.UtcNow.AddMinutes(2);

        JwtSecurityToken jwtSecurityToken = new(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: token.AccessTokenExpire,
                signingCredentials: new(securityKey, SecurityAlgorithms.HmacSha256)
            );

        JwtSecurityTokenHandler tokenHandler = new();

        token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);
        token.RefreshToken = CreateRefreshToken();

        return token;
    }

    private string CreateRefreshToken()
    {
        byte[] number = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);
    }
}
