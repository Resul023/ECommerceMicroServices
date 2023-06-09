﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Dtos.Product;
using Product.API.Services;
using Shared.ControllerBases;

namespace Product.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : CustomControllerBases
{
    private readonly ICategoryService _categoryService;
    public CategoriesController(ICategoryService categoryService)
    {
        this._categoryService = categoryService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();

        return CreateActionResultInstance(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        return CreateActionResultInstance(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto categoryDto)
    {
        var response = await _categoryService.CreateAsync(categoryDto);

        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
    {
        var response = await _categoryService.UpdateAsync(categoryUpdateDto);

        return CreateActionResultInstance(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _categoryService.DeleteAsync(id);

        return CreateActionResultInstance(response);
    }
}
