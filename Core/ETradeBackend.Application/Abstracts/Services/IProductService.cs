﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Application.Abstracts.Services
{
    public interface IProductService
    {
        Task<byte[]> QRCodeToProductAsync(string productId);
    }
}
