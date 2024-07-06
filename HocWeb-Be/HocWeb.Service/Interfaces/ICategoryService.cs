using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Models;

namespace HocWeb.Service.Interfaces
{
    public interface ICategoryService : IServiceBase<Category>
    {
        Task<ApiResult> Add(AddCategoryModel model);
    }
}
