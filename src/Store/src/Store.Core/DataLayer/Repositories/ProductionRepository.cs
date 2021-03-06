using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.DataLayer.Contracts;
using Store.Core.EntityLayer.Production;

namespace Store.Core.DataLayer.Repositories
{
    public class ProductionRepository : Repository, IProductionRepository
    {
        public ProductionRepository(IUserInfo userInfo, StoreDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public IQueryable<Product> GetProducts(Int32 pageSize = 10, Int32 pageNumber = 1, Int32? productCategoryID = null)
        {
            var query = DbContext.Set<Product>().AsQueryable();

            if (productCategoryID.HasValue)
            {
                query = query.Where(item => item.ProductCategoryID == productCategoryID);
            }

            return query.Paging<Product>(pageSize, pageNumber);
        }

        public Task<Product> GetProductAsync(Product entity)
        {
            return DbContext
                .Set<Product>()
                .FirstOrDefaultAsync(item => item.ProductID == entity.ProductID);
        }

        public Product GetProductByName(String productName)
        {
            return DbContext
                .Set<Product>()
                .FirstOrDefault(item => item.ProductName == productName);
        }

        public void AddProduct(Product entity)
        {
            Add(entity);

            CommitChanges();
        }

        public async Task<Int32> UpdateProductAsync(Product changes)
        {
            var entity = await GetProductAsync(changes);

            if (entity != null)
            {
                entity.ProductName = changes.ProductName;
                entity.ProductCategoryID = changes.ProductCategoryID;
                entity.UnitPrice = changes.UnitPrice;
                entity.Discontinued = changes.Discontinued;
                entity.Description = changes.Description;
            }

            return await CommitChangesAsync();
        }

        public void DeleteProduct(Product entity)
        {
            Remove(entity);

            CommitChanges();
        }

        public IQueryable<ProductCategory> GetProductCategories()
        {
            return DbContext.Set<ProductCategory>();
        }

        public ProductCategory GetProductCategory(ProductCategory entity)
        {
            return DbContext
                .Set<ProductCategory>()
                .FirstOrDefault(item => item.ProductCategoryID == entity.ProductCategoryID);
        }

        public void AddProductCategory(ProductCategory entity)
        {
            Add(entity);

            CommitChanges();
        }

        public void UpdateProductCategory(ProductCategory changes)
        {
            var entity = GetProductCategory(changes);

            if (entity != null)
            {
                entity.ProductCategoryName = changes.ProductCategoryName;

                Update(entity);

                CommitChanges();
            }
        }

        public void DeleteProductCategory(ProductCategory entity)
        {
            Remove(entity);

            CommitChanges();
        }

        public IQueryable<ProductInventory> GetProductInventories()
        {
            return DbContext.Set<ProductInventory>();
        }

        public ProductInventory GetProductInventory(ProductInventory entity)
        {
            return DbContext
                .Set<ProductInventory>()
                .FirstOrDefault(item => item.ProductInventoryID == entity.ProductInventoryID);
        }

        public Task<Int32> AddProductInventoryAsync(ProductInventory entity)
        {
            Add(entity);

            return CommitChangesAsync();
        }

        public async Task<Int32> UpdateProductInventoryAsync(ProductInventory changes)
        {
            var entity = GetProductInventory(changes);

            if (entity != null)
            {
                entity.ProductID = changes.ProductID;
                entity.Quantity = changes.Quantity;
                entity.Stocks = changes.Stocks;

                Update(entity);
            }

            return await CommitChangesAsync();
        }

        public void DeleteProductInventory(ProductInventory entity)
        {
            Remove(entity);

            CommitChanges();
        }

        public IQueryable<Warehouse> GetWarehouses(Int32 pageSize = 10, Int32 pageNumber = 0)
        {
            return DbContext.Paging<Warehouse>(pageSize, pageNumber);
        }

        public async Task<Warehouse> GetWarehouseAsync(Warehouse entity)
            => await DbContext.Set<Warehouse>().FirstOrDefaultAsync(item => item.WarehouseID == entity.WarehouseID);

        public async Task<Int32> AddWarehouseAsync(Warehouse entity)
        {
            Add(entity);

            return await CommitChangesAsync();
        }

        public async Task<Int32> UpdateWarehouseAsync(Warehouse changes)
        {
            Update(changes);

            return await CommitChangesAsync();
        }

        public async Task<Int32> RemoveWarehouseAsync(Warehouse entity)
        {
            Remove(entity);

            return await CommitChangesAsync();
        }
    }
}
