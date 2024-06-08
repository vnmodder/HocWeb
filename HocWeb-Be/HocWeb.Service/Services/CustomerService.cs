using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Interfaces;
using HocWeb.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HocWeb.Service.Services
{
    internal class CustomerService : ICustomerService
    {

        private readonly DataContext _dataContext;
        public CustomerService(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }
        public async Task<Customer?> Add(Customer model)
        {
            var cusTomer = await _dataContext.Customers.FirstOrDefaultAsync(x => x.Email == model.Email);
            if(cusTomer == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();

                try
                {
                    //model.CreatedDate = DateTime.Now; hoi thay cach link voi thang entitybase
                    _dataContext.Customers.Add(model); // chu y thay cusTomer = model
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return model;
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw new Exception(ex.Message);
                }

            }
            throw new Exception("Khách hàng này đã tồn tại.");
        }

        public async Task<bool> Delete(int id)
        {
            var cusTomer = await _dataContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (cusTomer != null)   
            {
                using var tran = _dataContext.Database.BeginTransaction();

                try
                {
                    _dataContext.Customers.Remove(cusTomer);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw new Exception(ex.Message);
                    return false;
                }

            }
            throw new Exception("Khách hàng này không tồn tại.");
            return false;
        }

        public async Task<IList<Customer>> GetAll()
        {
            return await _dataContext.Customers.ToListAsync();
        }

        public async Task<Customer?> GetById(int id)
        {
            return await _dataContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(Customer model)
        {
            var cusTomer = await _dataContext.Customers.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (cusTomer != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();

                try
                {
                    cusTomer.Activated = model.Activated;
                    cusTomer.Photo = model.Photo;
                    cusTomer.Email = model.Email;
                    cusTomer.Fullname = model.Fullname;
                    cusTomer.Password = model.Password;
                    cusTomer.Id = model.Id;


                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw new Exception(ex.Message);
                    return false;
                }

            }
            throw new Exception("Khách hàng này không tồn tại.");
            return false;
        }
    }
}
