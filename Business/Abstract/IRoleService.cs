using Core.Utilities.Results.Abstract;
using Entities.DTOs.RoleDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract;

public interface IRoleService
{
    Task<IResult> CreateAsync(AddRoleDTO entity);
}
