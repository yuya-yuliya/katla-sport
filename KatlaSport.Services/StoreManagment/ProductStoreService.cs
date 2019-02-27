using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KatlaSport.DataAccess;
using KatlaSport.DataAccess.ProductStore;
using KatlaSport.Services.ProductManagement;

namespace KatlaSport.Services.StoreManagment
{
    public class ProductStoreService : IProductStoreService
    {
        private readonly IProductStoreContext _context;
        private readonly IUserContext _userContext;

        public ProductStoreService(IProductStoreContext context, IUserContext userContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <inheritdoc/>
        public async Task<List<ProductStoreItem>> GetProductStoreItemsAsync(int hiveSectionId)
        {
            var dbStoreItems = await _context.Items.Where(si => si.HiveSectionId == hiveSectionId).ToArrayAsync();
            var storeItems = dbStoreItems.Select(si => Mapper.Map<ProductStoreItem>(si)).ToList();

            return storeItems;
        }
    }
}
