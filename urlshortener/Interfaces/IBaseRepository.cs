using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace urlshortener.Interfaces;

public interface IBaseRepository
{
    Task<DbTransaction> CreateTransactionAsync();
}
