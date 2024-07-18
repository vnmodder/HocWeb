using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Service.Models.Product
{
    public class AddProductModel
    {
        public string? Name { get; set; }
        public int? categoryId { get; set; }

        public string? Image { get; set; } //hmm, can xem xet lai
        public string? Description { get; set; }

        public string? unitBrief { get; set; }

        public double? unitPrice { get; set; }

        public int? quantity { get; set; }

        public int? discount { get; set; }

        public string? supplierId { get; set;}
    }
}
