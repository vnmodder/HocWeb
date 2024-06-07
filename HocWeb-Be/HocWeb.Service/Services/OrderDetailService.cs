using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Service.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        public Task<OrderDetail?> Add(OrderDetail model)
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
