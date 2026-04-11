using Core.Utilities.Results.Abstract;
using Entities.DTOs.ColorDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract;

public interface IColorService
{
    Task<IResult> CreateAsync(AddColorDTO entity);
}
