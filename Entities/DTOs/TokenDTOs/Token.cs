using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs.TokenDTOs;

public class Token
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime AccessTokenExpire { get; set; }
}
