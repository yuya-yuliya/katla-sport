using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KatlaSport.DataAccess;
using KatlaSport.DataAccess.ProductStore;
using KatlaSport.Services.ProductManagement;
using DbProductStoreItem = KatlaSport.DataAccess.ProductStore.StoreItem;

namespace KatlaSport.Services.StoreManagment
{
    /// <summary>
    /// Represents a product store service.
    /// </summary>
    public class ProductStoreService : IProductStoreService
    {
        private readonly IProductStoreContext _context;
        private readonly IUserContext _userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductStoreService"/> class with specified <see cref="IProductCatalogueContext"/>.
        /// </summary>
        /// <param name="context">A <see cref="IProductStoreContext"/>.</param>
        /// <param name="userContext">A <see cref="IUserContext"/>.</param>
        public ProductStoreService(IProductStoreContext context, IUserContext userContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <inheritdoc/>
        public async Task<List<ProductStoreItem>> GetProductStoreItemsAsync(int productId, int hiveSectionId)
        {
            var dbStoreItems = await _context.Items.Where(si => si.HiveSectionId == hiveSectionId && si.ProductId == productId).ToArrayAsync();
            var storeItems = dbStoreItems.Select(si => Mapper.Map<ProductStoreItem>(si)).ToList();

            return storeItems;
        }

        /// <inheritdoc/>
        public async Task<ProductStoreItem> GetProductStoreItemAsync(int productStoreItemId)
        {
            var dbStoreItems = await _context.Items.Where(i => i.Id == productStoreItemId).ToArrayAsync();
            if (dbStoreItems.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            return Mapper.Map<DbProductStoreItem, ProductStoreItem>(dbStoreItems[0]);
        }

        /// <inheritdoc/>
        public async Task<List<ProductStoreItem>> GetProductStoreItemsAsync(int hiveSectionId)
        {
            var dbStoreItems = await _context.Items.Where(si => si.HiveSectionId == hiveSectionId).ToArrayAsync();
            var storeItems = dbStoreItems.Select(si => Mapper.Map<ProductStoreItem>(si)).ToList();

            return storeItems;
        }

        /// <inheritdoc/>
        public async Task<ProductStoreItem> CreateProductStoreItemAsync(UpdateProductStoreItemRequest createRequest)
        {
            var dbItems = await _context.Items.Where(i => i.ProductId == createRequest.ProductId && i.HiveSectionId == createRequest.HiveSectionId).ToArrayAsync();
            if (dbItems.Length > 0)
            {
                throw new RequestedResourceHasConflictException("Product Id and/or Hive section Id");
            }

            var dbItem = Mapper.Map<UpdateProductStoreItemRequest, DbProductStoreItem>(createRequest);

            _context.Items.Add(dbItem);

            await _context.SaveChangesAsync();

            return Mapper.Map<ProductStoreItem>(dbItem);
        }

        /// <inheritdoc/>
        public async Task<ProductStoreItem> UpdateProductStoreItemAsync(int storeItemId, UpdateProductStoreItemRequest updateRequest)
        {
            var dbItems = await _context.Items.Where(i => i.HiveSectionId == updateRequest.HiveSectionId && i.ProductId == updateRequest.ProductId && i.Id != storeItemId).ToArrayAsync();
            if (dbItems.Length > 0)
            {
                throw new RequestedResourceHasConflictException("Hive section Id and/or Product Id");
            }

            dbItems = await _context.Items.Where(i => i.Id == storeItemId).ToArrayAsync();
            var dbItem = dbItems.FirstOrDefault();
            if (dbItem == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            Mapper.Map(updateRequest, dbItem);

            await _context.SaveChangesAsync();

            dbItems = await _context.Items.Where(i => i.Id == storeItemId).ToArrayAsync();
            return dbItems.Select(i => Mapper.Map<ProductStoreItem>(i)).FirstOrDefault();
        }
    }
}
