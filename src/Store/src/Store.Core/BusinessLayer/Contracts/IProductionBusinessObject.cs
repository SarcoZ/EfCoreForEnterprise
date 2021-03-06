﻿using System;
using System.Threading.Tasks;
using Store.Core.BusinessLayer.Responses;
using Store.Core.EntityLayer.Production;

namespace Store.Core.BusinessLayer.Contracts
{
    public interface IProductionBusinessObject : IBusinessObject
    {
        Task<IListModelResponse<Product>> GetProductsAsync(Int32 pageSize = 0, Int32 pageNumber = 0);

        Task<IListModelResponse<Warehouse>> GetWarehousesAsync(Int32 pageSize = 0, Int32 pageNumber = 0);
    }
}
