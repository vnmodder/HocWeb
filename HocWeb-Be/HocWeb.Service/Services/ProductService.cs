using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HocWeb.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Product?> Add(Product model)
        {
            var product = await _dataContext.Products
                .FirstOrDefaultAsync(x => x.Name == model.Name && x.CategoryId == model.CategoryId);
            if (product == null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    model.CreatedDate = DateTime.Now;
                    _dataContext.Products.Add(model);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return model;
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            throw new Exception("Sản phẩm này đã tồn tại!.");
        }

        public async Task<bool> Delete(int id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    product.DeleteDate = DateTime.Now;
                    _dataContext.Products.Remove(product);
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

            throw new Exception("Không tìm thấy sản phẩm này.");
        }

        public async Task<IList<Product>> GetAll()
        {
            return await _dataContext.Products.ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(Product model)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (product != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    product.Name = model.Name;
                    product.UnitBrief = model.UnitBrief;
                    product.UnitPrice = model.UnitPrice;
                    product.Image = model.Image;
                    product.ProductDate = model.ProductDate;
                    product.Available = model.Available;
                    product.Description = model.Description;
                    product.CategoryId = model.CategoryId;
                    product.SupplierId = model.SupplierId;
                    product.Quantity = model.Quantity;
                    product.Discount = model.Discount;
                    product.Special = model.Special;
                    product.Latest = model.Latest;
                    product.Views = model.Views;
                    product.UpdatedDate = DateTime.Now;

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

            throw new Exception("Không tìm thấy sản phẩm này.");
        }
    }
}
