using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Infrastructure.Base
{
    public abstract class EntityBase<TKey>
    {
        public virtual TKey? Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime UpdatedDate { get; set; }
        public virtual DateTime? DeleteDate { get; set; }
    }
}
