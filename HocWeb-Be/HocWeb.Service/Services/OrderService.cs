using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext datacontext;

        public OrderService(DataContext context)
        {
            datacontext = context;
        }


        public async Task<Order?> Add(Order model)
        {
          var order = await datacontext.Orders.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (order == null)
            {
                using var tran = datacontext.Database.BeginTransaction();
                try
                {
                    model.CreatedDate = DateTime.Now;   
                    datacontext.Orders.Add(model);
                    await datacontext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return model;
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            throw new Exception("Order này đã tồn tại");
        }

        public async Task<bool> Delete(int id)
        {
            var order = await datacontext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order != null)
            {
                using var tran = datacontext.Database.BeginTransaction();
                try
                {
                    order.DeleteDate = DateTime.Now;
                    datacontext.Orders.Remove(order);
                    await datacontext.SaveChangesAsync();
                    await tran.CommitAsync();
           
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            throw new Exception("Order này không tồn tại");
        }

        public async Task<IList<Order>> GetAll()
        {
            return await datacontext.Orders.Where(x => x.DeleteDate == null).ToListAsync();
        }

        public async Task<Order?> GetById(int id)
        {
            return await datacontext.Orders.Where(x=> x.DeleteDate == null).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(Order model)
        {
            var order = await datacontext.Orders.Where(x => x.DeleteDate == null).FirstOrDefaultAsync(x => x.Id == model.Id);
            if (order != null)
            {
                using var tran = datacontext.Database.BeginTransaction();
                try
                {
                    order.Id = model.Id;
                    order.CustomerId = model.CustomerId;
                    order.RequireDate = model.RequireDate;
                    order.Receiver = model.Receiver;
                    order.Address = model.Address;
                    order.Description = model.Description;
                    order.Amount = model.Amount;
                    await datacontext.SaveChangesAsync();
                    await tran.CommitAsync();
     
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            throw new Exception("Order này đã tồn tại");
        }
    }
   
}
