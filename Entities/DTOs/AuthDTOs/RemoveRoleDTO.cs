using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs.AuthDTOs;

public record RemoveRoleDTO(string UserId, string Role);
