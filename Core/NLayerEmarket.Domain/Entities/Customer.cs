using NLayerEmarket.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerEmarket.Domain.Entities
{
    public class Customer:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
