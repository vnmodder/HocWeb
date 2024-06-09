using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Entities;
using HocWeb.Infrastructure.Extensions;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace HocWeb.Service.Services
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(DataContext context) : base(context) { }

        public async Task<ApiResult> Add(Order model)
        {
          var order = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (order == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    model.CreatedDate = _now;   
                    _dataContext.Orders.Add(model);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new( model);
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }
            return new ApiResult() { Message = "Order này đã tồn tại" };
        }


        public async Task<ApiResult> Delete(int id)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    order.DeleteDate = _now;
                   // _dataContext.Orders.Remove(order);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new();
           
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
       
                }
            }

            return new ApiResult() { Message = "Order này không tồn tại" };
        }

        public async Task<ApiResult> GetAll()
        {
            var result =  await _dataContext.Orders.Exist().ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result =  await _dataContext.Orders.Exist()
                .FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public async Task<ApiResult> Update(Order model)
        {
            var order = await _dataContext.Orders.Exist().FirstOrDefaultAsync(x => x.Id == model.Id);
            if (order != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    
                    order.CustomerId = model.CustomerId;
                    order.RequireDate = model.RequireDate;
                    order.Receiver = model.Receiver;
                    order.Address = model.Address;
                    order.Description = model.Description;
                    order.Amount = model.Amount;
                    order.UpdatedDate = _now;
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new();
     
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }
            return new ApiResult() { Message = "Order này không tồn tại" };
        }
    }
   
}
