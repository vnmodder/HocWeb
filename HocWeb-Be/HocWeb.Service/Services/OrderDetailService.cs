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
    public class OrderDetailService : IOrderDetailService
    {
        private readonly DataContext datacontext;

        public OrderDetailService(DataContext context)
        {
            datacontext = context;
        }

        public async Task<OrderDetail> Add(OrderDetail model)
        {
            using var tran = datacontext.Database.BeginTransaction();
            try
            {
                model.CreatedDate = DateTime.Now;
                datacontext.OrderDetails.Add(model);
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


        public async Task<bool> Delete(int id)
        {
            var order = await datacontext.OrderDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (order != null)
            {
                using var tran = datacontext.Database.BeginTransaction();
                try
                {
                    order.DeleteDate = DateTime.Now;
                    datacontext.OrderDetails.Remove(order);
                    await datacontext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return true;
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }
            throw new Exception("Sản phẩm này không tồn tại");
        }

        public async Task<IList<OrderDetail>> GetAll()
        {
            return await datacontext.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail?> GetById(int id)
        {
            return await datacontext.OrderDetails.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<OrderDetail>> GetByOrderId(int orderId)
        {
            return await datacontext.OrderDetails.Where(x => x.OrderId == orderId).ToListAsync();
        }
          

        

        public async Task<bool> Update(OrderDetail model)
        {
            var order = await datacontext.OrderDetails.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (order != null)
            {
                using var tran = datacontext.Database.BeginTransaction();
                try
                {
                    order.ProductId = model.ProductId;
                    order.Quantity = model.Quantity;
                    order.Discount = model.Discount;
                    order.UnitPrice = model.UnitPrice;
                    order.UpdatedDate = DateTime.Now;
                    await datacontext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return true;
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }
            throw new Exception("Sản phẩm này không tồn tại");
        }

       
    }
}
