using HocWeb.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Infrastructure.Entities
{
    public class Product : EntityBase
    {
        public string? Name { get; set; }
        public string? UnitBrief { get; set; }
        public double UnitPrice { get; set; }
        public string? Image { get; set; }
        public System.DateTime ProductDate { get; set; }
        public bool Available { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? SupplierId { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public bool Special { get; set; }
        public bool Latest { get; set; }
        public int Views { get; set; }
    }
}
