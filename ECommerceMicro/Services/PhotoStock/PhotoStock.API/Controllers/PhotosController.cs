﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoStock.API.Dtos;
using Shared.ControllerBases;
using Shared.Dtos;

namespace PhotoStock.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhotosController : CustomControllerBases
{
    [HttpPost]
    public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
    {
        var fileName = Guid.NewGuid().ToString().Substring(1, 8) + photo.FileName;
        if (photo != null && photo.Length > 0)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await photo.CopyToAsync(stream, cancellationToken);

            var returnPath =  photo.FileName;

            PhotoDto photoDto = new() { Url = fileName };

            return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
        }

        return CreateActionResultInstance(Response<PhotoDto>.Fail("photo is empty", 400));
    }
    [HttpDelete]
    public IActionResult PhotoDelete(string photoUrl)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
        if (!System.IO.File.Exists(path))
        {
            return CreateActionResultInstance(Response<NoContent>.Fail("photo not found", 404));
        }

        System.IO.File.Delete(path);

        return CreateActionResultInstance(Response<NoContent>.Success(204));
    }
}
