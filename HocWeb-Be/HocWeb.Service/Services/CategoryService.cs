using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Entities;
using HocWeb.Infrastructure.Extensions;
using HocWeb.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dataContext;

        public CategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Category?> Add(Category model)
        {
            var cateGory = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Name == model.Name);
            if (cateGory == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    model.CreatedDate = DateTime.Now;
                     _dataContext.Categories.Add(model);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return model;
                }
                catch(Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            throw new Exception("Category này đã tồn tại");
        }

        public async Task<bool> Delete(int id)
        {
             var cateGory = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (cateGory != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    //_dataContext.Remove(cateGory); bỏ cái này
                    cateGory.DeleteDate = DateTime.Now;
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return true;
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            throw new Exception("Không tìm thấy Category này");
        }

        public async Task<IList<Category>> GetAll()
        {
            return await _dataContext.Categories.Exist().ToListAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _dataContext.Categories.Exist().FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<bool> Update(Category model)
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
                    cateGory.UpdatedDate = DateTime.Now;
                    
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return true;
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            throw new Exception("Không tìm thấy Category này");
        }
    }
}
