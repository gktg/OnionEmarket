using NLayerEmarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerEmarket.Domain.ViewModels
{
    public class CategoryVM
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public List<Product> Products { get; set; }


    }
}
