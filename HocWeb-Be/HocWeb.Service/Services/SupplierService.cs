using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HocWeb.Service.Services
{
    public class SupplierService
    {
        private readonly DataContext _dataContext;

        public SupplierService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Supplier?> Add(Supplier model)
        {
            var existingSupplier = await _dataContext.Suppliers.FirstOrDefaultAsync(x => x.Name == model.Name);
            if (existingSupplier != null)
            {
                throw new Exception("Nhà cung cấp này đã tồn tại.");
            }

            using var tran = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                model.CreatedDate = DateTime.Now;
                await _dataContext.Suppliers.AddAsync(model);
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

        public async Task<bool> Delete(int id)
        {
            var supplier = await _dataContext.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            if (supplier == null)
            {
                throw new Exception("Không tìm thấy nhà cung cấp này.");
            }

            using var tran = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                supplier.DeleteDate = DateTime.Now;
                _dataContext.Suppliers.Remove(supplier);
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

        public async Task<IList<Supplier>> GetAll()
        {
            return await _dataContext.Suppliers.ToListAsync();
        }

        public async Task<Supplier?> GetById(int id)
        {
            return await _dataContext.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(Supplier model)
        {
            var supplier = await _dataContext.Suppliers.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (supplier == null)
            {
                throw new Exception("Không tìm thấy nhà cung cấp này.");
            }

            using var tran = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                supplier.Name = model.Name;
                supplier.Logo = model.Logo;
                supplier.Email = model.Email;
                supplier.Phone = model.Phone;
                supplier.UpdatedDate = DateTime.Now;

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
    }
}
