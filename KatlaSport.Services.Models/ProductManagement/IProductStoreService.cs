using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatlaSport.Services.ProductManagement
{
    public interface IProductStoreService
    {
        Task<List<ProductStoreItem>> GetProductStoreItemsAsync(int hiveSectionId);
    }
}
