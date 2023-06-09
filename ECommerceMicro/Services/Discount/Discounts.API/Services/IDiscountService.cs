﻿using Discounts.API.Model;
using Shared.Dtos;
using System.Reflection;
using System.Threading.Tasks;

namespace Discounts.API.Services;
public interface IDiscountService
{
    Task<Response<List<Discount>>> GetAll();
    Task<Response<Discount>> GetById(int id);
    Task<Response<Discount>> GetByCodeAndUserId(string code, string userId);

    Task<Response<NoContent>> Create(Discount discount);
    Task<Response<NoContent>> Update(Discount discount);
    Task<Response<NoContent>> Delete(int id);

}
