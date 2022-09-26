using NLayerEmarket.Domain.Entities.Common;
using NLayerEmarket.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerEmarket.Domain.Entities
{
    public class User:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

    }
}
