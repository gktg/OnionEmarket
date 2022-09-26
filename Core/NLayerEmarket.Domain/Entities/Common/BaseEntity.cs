using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayerEmarket.Domain.Enums;

namespace NLayerEmarket.Domain.Entities.Common
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Status = DataStatus.Inserted;
            CreatedDate = DateTime.Now;
        }

        public Guid ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DataStatus Status { get; set; }

    }
}
