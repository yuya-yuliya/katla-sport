using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KatlaSport.DataAccess;
using KatlaSport.DataAccess.ProductCatalogue;
using KatlaSport.DataAccess.ProductStore;
using KatlaSport.DataAccess.ProductStoreHive;
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
        private readonly IProductStoreHiveContext _hiveContext;
        private readonly IProductCatalogueContext _productContext;
        private readonly IUserContext _userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductStoreRequestService"/> class with specified <see cref="IProductCatalogueContext"/>.
        /// </summary>
        /// <param name="context">A <see cref="IProductStoreContext"/>.</param>
        /// <param name="hiveContext">A <see cref="IProductStoreHiveContext"/>.</param>
        /// <param name="productContext">A <see cref="IProductCatalogueContext"/>.</param>
        /// <param name="userContext">A <see cref="IUserContext"/>.</param>
        public ProductStoreRequestService(IProductStoreContext context, IProductStoreHiveContext hiveContext, IProductCatalogueContext productContext, IUserContext userContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _hiveContext = hiveContext ?? throw new ArgumentNullException(nameof(hiveContext));
            _productContext = productContext ?? throw new ArgumentNullException(nameof(productContext));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <inheritdoc/>
        public async Task<List<ProductStoreItemRequest>> GetActiveRequestsAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<ProductStoreItemRequest> CreateRequestAsync(UpdateRequestRequest createRequest)
        {
            var dbProduct = await _productContext.Products.Where(p => p.Id == createRequest.ProductId).ToArrayAsync();
            if (dbProduct.Length == 0)
            {
                throw new RequestedResourceNotFoundException("Product Id");
            }

            var dbHiveSection = await _hiveContext.Sections.Where(s => s.Id == createRequest.HiveSectionId).ToArrayAsync();
            if (dbHiveSection.Length == 0)
            {
                throw new RequestedResourceNotFoundException("Hive section Id");
            }

            var dbRequest = Mapper.Map<UpdateRequestRequest, DbProductStoreItemRequest>(createRequest);
            _context.Requests.Add(dbRequest);

            await _context.SaveChangesAsync();

            return Mapper.Map<ProductStoreItemRequest>(dbRequest);
        }

        /// <inheritdoc/>
        public async Task SetRequestCompletedAsync(int requestId)
        {
            throw new NotImplementedException();
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
