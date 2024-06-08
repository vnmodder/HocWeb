using HocWeb.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Infrastructure.Entities
{
    public class Order : EntityBase
    {
        public  int CustomerId { get; set; }
        public DateTime? RequireDate { get; set; }
        public string? Receiver { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }

        /*public virtual Customer Customer { get; set; }*/
    /*    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }

        public static implicit operator Order(bool v)
        {
            throw new NotImplementedException();
        }*/
    }
}
