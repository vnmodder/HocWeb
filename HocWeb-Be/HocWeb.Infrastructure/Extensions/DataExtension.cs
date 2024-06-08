using HocWeb.Infrastructure.Base;
using HocWeb.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Infrastructure.Extensions
{
    public static class DataExtension
    {
        public static IQueryable<T> Exist<T>(this DbSet<T> data) where T : EntityBase
        {
            return data.Where(x => x.DeleteDate.HasValue);
        }

        public static IQueryable<User> Exist(this DbSet<User> data)
        {
            return data.Where(x => x.DeleteDate.HasValue);
        }
    }
}
