using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Entities;
using HocWeb.Infrastructure.Extensions;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;
using HocWeb.Service.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace HocWeb.Service.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(DataContext dataContext, Common.IServices.IUserService userService) : base(dataContext, userService) { }

        public async Task<ApiResult> Add(Product model)
        {
            var product = await _dataContext.Products
                .FirstOrDefaultAsync(x => x.Name == model.Name && x.CategoryId == model.CategoryId);
            if (product == null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    model.CreatedDate = _now;
                    _dataContext.Products.Add(model);
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

            return new ApiResult() { Message = "Sản phẩm này đã tồn tại!." };
        }

        public async Task<ApiResult> Add2(AddProductModel model)
        {
            var proDuct = await _dataContext.Products.FirstOrDefaultAsync(x => x.Name == model.Name);
            if (proDuct == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {

                    var newProduct = new Product()
                    {
                        Name = model.Name,
                        CategoryId = model.categoryId,
                        UnitBrief = model.unitBrief,
                        UnitPrice = model.unitPrice,
                        Image = model.Image,
                        Description = model.Description,
                        Quantity = model.quantity,
                        Discount = model.discount,
                        CreatedDate = _now,
                        ProductDate = _now,
                        UpdatedDate = null,
                        DeleteDate = null,
                        SupplierId = model.supplierId,
                        Available = true,
                        Special = true,
                        Latest = true,
                        Views = 1,
                    };
                    _dataContext.Products.Add(newProduct);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new(newProduct);
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message + "lỗi");
                }
            }

            return new()
            {
                Message = "Sản phẩm này đã tồn tại",
            };
        }

        public async Task<ApiResult> Delete(int id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    product.DeleteDate = _now;
                    _dataContext.Products.Remove(product);
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

            return new ApiResult() { Message = "Không tìm thấy sản phẩm này." };
        }

        public async Task<ApiResult> Delete_Unpermanently(int id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if(product != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try 
                {
                    product.DeleteDate = _now;
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new();
                }
                catch(Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }
            return new ApiResult() { Message = "Không tìm thấy sản phẩm này" };
        }
        public async Task<ApiResult> Cancel_Delete_Unpermanently(int id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    product.DeleteDate = null;
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
            return new ApiResult() { Message = "Không tìm thấy sản phẩm này" };
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.Products.Exist().ToListAsync();
            return new(result);

        }
        public async Task<ApiResult> GetAll_Insist_Deleted()
        {
            var result = await _dataContext.Products.ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.Products.Exist()
                .FirstOrDefaultAsync(x => x.Id == id);
            return new(result);

        }

        public async Task<ApiResult> Update(Product model)
        {
            var product = await _dataContext.Products.Exist()
                .FirstOrDefaultAsync(x => x.Id == model.Id);
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
                    product.UpdatedDate = _now;

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

            return new ApiResult() { Message = "Không tìm thấy sản phẩm này." };
        }

    }
}
