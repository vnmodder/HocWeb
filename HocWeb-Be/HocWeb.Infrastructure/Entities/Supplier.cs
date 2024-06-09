using HocWeb.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Infrastructure.Entities
{
    public class Supplier : EntityBase
    {
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
