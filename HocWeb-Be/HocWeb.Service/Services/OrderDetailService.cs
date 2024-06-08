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

        public Task<OrderDetail> Add(OrderDetail model)
        {
          throw new NotImplementedException();
        }


        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<OrderDetail>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetail?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(OrderDetail model)
        {
            throw new NotImplementedException();
        }

       
    }
}
