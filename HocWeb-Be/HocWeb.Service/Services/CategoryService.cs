using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Entities;
using HocWeb.Infrastructure.Extensions;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace HocWeb.Service.Services
{
    public class CategoryService : BaseService, ICategoryService
    {

        public CategoryService(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<ApiResult> Add(Category model)
        {
            var cateGory = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Name == model.Name);
            if (cateGory == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    model.CreatedDate = _now;
                    _dataContext.Categories.Add(model);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new(model);
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            return new()
            {
                Message = "Category này đã tồn tại",
            };
        }

        public async Task<ApiResult> Delete(int id)
        {
            var cateGory = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (cateGory != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    cateGory.DeleteDate = _now;
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
            return new()
            {
                Message = "Không tìm thấy Category này"
            };
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.Categories.Exist().ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.Categories.Exist().FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public async Task<ApiResult> Update(Category model)
        {
            var cateGory = await _dataContext.Categories.Exist()
                             .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (cateGory != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    cateGory.Name = model.Name;
                    cateGory.Image = model.Image;
                    cateGory.Icon = model.Icon;
                    cateGory.NameVN = model.NameVN;
                    cateGory.UpdatedDate = _now;

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

            return new() { Message = "Không tìm thấy Category này" };
        }
    }
}