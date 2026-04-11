using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concret.SuccessResults;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.ColorDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete;

public class ColorManager : IColorService
{
    private readonly IColorDAL _colorDAL;

    public ColorManager(IColorDAL colorDAL)
    {
        _colorDAL = colorDAL;
    }

    public async Task<IResult> CreateAsync(AddColorDTO entity)
    {
        await _colorDAL.AddAsync(new()
        {
            Name = entity.Name,
            Code = entity.Code
        });

        return new SuccessResult(System.Net.HttpStatusCode.Created);
    }
}
