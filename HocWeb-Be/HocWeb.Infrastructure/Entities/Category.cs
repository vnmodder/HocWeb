using HocWeb.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Infrastructure.Entities
{
    public class Category : EntityBase
    {
        public string? NameVN { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Icon { get; set; }
    }
}
