using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Models;
using HocWeb.Service.Models.Product;

namespace HocWeb.Service.Interfaces
{
    public interface IProductService : IServiceBase<Product>
    {
        //Task<ApiResult> Search_Detail(Product model);
        Task<ApiResult> Add(AddProductModel model);
    }
}
