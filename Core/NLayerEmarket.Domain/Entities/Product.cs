using NLayerEmarket.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerEmarket.Domain.Entities
{
    public class Product: BaseEntity
    {
        public virtual Category Category { get; set; }

        public string Name { get; set; }

        public int Stock { get; set; }

        public float Price { get; set; }

        public string Media { get; set; }

    }
}
