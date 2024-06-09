using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;

namespace HocWeb.Service.Services
{
    public class OrderDetailService : BaseService,  IOrderDetailService
    {
        public OrderDetailService(DataContext context) : base(context) { }

        public Task<ApiResult> Add(OrderDetail model)
        {
          throw new NotImplementedException();
        }

        public Task<ApiResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> Update(OrderDetail model)
        {
            throw new NotImplementedException();
        }
    }
}
