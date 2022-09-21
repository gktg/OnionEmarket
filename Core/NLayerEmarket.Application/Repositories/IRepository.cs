﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLayerEmarket.Domain.Entities.Common;

namespace NLayerEmarket.Application.Repositories
{
    public interface IRepository<T> where T: BaseEntity
    {
        DbSet<T> Table { get; }

    }
}
