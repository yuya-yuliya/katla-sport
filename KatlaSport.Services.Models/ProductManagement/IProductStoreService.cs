using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatlaSport.Services.ProductManagement
{
    /// <summary>
    /// Represents a store product service.
    /// </summary>
    public interface IProductStoreService
    {
        /// <summary>
        /// Gets a list of store products in hive section.
        /// </summary>
        /// <param name="hiveSectionId">An hive section id.</param>
        /// <returns>A <see cref="Task{List{ProductCategoryListItem}}"/>.</returns>
        Task<List<ProductStoreItem>> GetProductStoreItemsAsync(int hiveSectionId);
    }
}
