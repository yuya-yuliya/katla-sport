using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KatlaSport.DataAccess;
using KatlaSport.DataAccess.ProductCatalogue;
using KatlaSport.DataAccess.ProductStore;
using KatlaSport.DataAccess.ProductStoreHive;
using KatlaSport.Services.HiveManagement;
using KatlaSport.Services.ProductManagement;
using DbProductStoreItemRequest = KatlaSport.DataAccess.ProductStore.ProductStoreItemRequest;
using ProductStoreItemRequest = KatlaSport.Services.ProductManagement.ProductStoreItemRequest;

namespace KatlaSport.Services.StoreManagment
{
    /// <summary>
    /// Represents a store product items request service.
    /// </summary>
    public class ProductStoreRequestService : IProductStoreRequestService
    {
        private readonly IProductStoreContext _context;
        private readonly IHiveSectionService _hiveSectionService;
        private readonly IProductCatalogueService _productService;
        private readonly IUserContext _userContext;
        private readonly IProductStoreService _productStoreService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductStoreRequestService"/> class with specified <see cref="IProductCatalogueContext"/>.
        /// </summary>
        /// <param name="context">A <see cref="IProductStoreContext"/>.</param>
        /// <param name="hiveSectionService">A <see cref="IHiveSectionService"/>.</param>
        /// <param name="productService">A <see cref="IProductCatalogueService"/>.</param>
        /// <param name="productStoreService">A <see cref="IProductStoreService"/>.</param>
        /// <param name="userContext">A <see cref="IUserContext"/>.</param>
        public ProductStoreRequestService(IProductStoreContext context, IHiveSectionService hiveSectionService, IProductCatalogueService productService, IProductStoreService productStoreService, IUserContext userContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _hiveSectionService = hiveSectionService ?? throw new ArgumentNullException(nameof(hiveSectionService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _productStoreService = productStoreService ?? throw new ArgumentNullException(nameof(productStoreService));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <inheritdoc/>
        public async Task<List<ProductStoreItemRequest>> GetRequestsAsync()
        {
            var dbRequests = await _context.Requests.OrderBy(r => r.Id).ToArrayAsync();
            var requests = dbRequests.Select(r => Mapper.Map<ProductStoreItemRequest>(r)).ToList();
            return requests;
        }

        /// <inheritdoc/>
        public async Task<ProductStoreItemRequest> CreateRequestAsync(UpdateRequestRequest createRequest)
        {
            var dbProduct = await _productService.GetProductAsync(createRequest.ProductId);
            var dbHiveSection = await _hiveSectionService.GetHiveSectionAsync(createRequest.HiveSectionId);

            var dbRequest = Mapper.Map<UpdateRequestRequest, DbProductStoreItemRequest>(createRequest);
            _context.Requests.Add(dbRequest);

            await _context.SaveChangesAsync();

            return Mapper.Map<ProductStoreItemRequest>(dbRequest);
        }

        /// <inheritdoc/>
        public async Task SetRequestCompletedAsync(int requestId)
        {
            var dbRequests = await _context.Requests.Where(r => r.Id == requestId).ToArrayAsync();
            if (dbRequests.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbRequest = dbRequests[0];
            if (dbRequest.Completed)
            {
                throw new RequestedResourceHasConflictException();
            }

            dbRequest.Completed = true;

            var storeItems = await _productStoreService.GetProductStoreItemsAsync(dbRequest.ProductId, dbRequest.HiveSectionId);
            if (storeItems.Count == 0)
            {
                await _productStoreService.CreateProductStoreItemAsync(Mapper.Map<UpdateProductStoreItemRequest>(dbRequest));
            }
            else
            {
                var updateRequest = Mapper.Map<UpdateProductStoreItemRequest>(storeItems[0]);
                updateRequest.Quantity += dbRequest.Quantity;
                await _productStoreService.UpdateProductStoreItemAsync(storeItems[0].Id, updateRequest);
            }

            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<ProductStoreItemRequest> GetRequestAsync(int requestId)
        {
            var dbRequests = await _context.Requests.Where(r => r.Id == requestId).ToArrayAsync();

            if (dbRequests.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            return Mapper.Map<DbProductStoreItemRequest, ProductStoreItemRequest>(dbRequests[0]);
        }
    }
}
