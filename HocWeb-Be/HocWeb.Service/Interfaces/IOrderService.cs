﻿using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Models;

namespace HocWeb.Service.Interfaces
{
    public interface IOrderService : IServiceBase<Order>
    {
        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        Task<ApiResult> CreateNew(OrderModel model);
    }
}
