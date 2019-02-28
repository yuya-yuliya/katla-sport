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

        /// <summary>
        /// Gets a product store item with specified identifier.
        /// </summary>
        /// <param name="productStoreItemId">A product store identifier.</param>
        /// <returns>A <see cref="Task{ProductStoreItem}"/>.</returns>
        Task<ProductStoreItem> GetProductStoreItemAsync(int productStoreItemId);

        /// <summary>
        /// Gets a product store item with specified identifier of hive section and product.
        /// </summary>
        /// <param name="productId">A product identifier.</param>
        /// /// <param name="hiveSectionId">A phive section identifier.</param>
        /// <returns>A <see cref="Task{ProductStoreItem}"/>.</returns>
        Task<List<ProductStoreItem>> GetProductStoreItemsAsync(int productId, int hiveSectionId);

        /// <summary>
        /// Creates a new store product item.
        /// </summary>
        /// <param name="createRequest">A <see cref="UpdateProductStoreItemRequest"/>.</param>
        /// <returns>A <see cref="Task{ProductStoreItem}"/>.</returns>
        Task<ProductStoreItem> CreateProductStoreItemAsync(UpdateProductStoreItemRequest createRequest);

        /// <summary>
        /// Updates an existed product category.
        /// </summary>
        /// <param name="storeItemId">A product category identifier.</param>
        /// <param name="updateRequest">A <see cref="UpdateProductStoreItemRequest"/>.</param>
        /// <returns>A <see cref="Task{ProductStoreItem}"/>.</returns>
        Task<ProductStoreItem> UpdateProductStoreItemAsync(int storeItemId, UpdateProductStoreItemRequest updateRequest);
    }
}
