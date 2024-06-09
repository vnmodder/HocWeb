using HocWeb.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Service.Interfaces
{
    public interface IOrderServiceDetail : IServiceBase<OrderDetail>
    {
        Task<IList<OrderDetail>> GetByOrderId(int orderId);

    }
}
