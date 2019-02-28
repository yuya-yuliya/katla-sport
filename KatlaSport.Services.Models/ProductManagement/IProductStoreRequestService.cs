using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatlaSport.Services.ProductManagement
{
    /// <summary>
    /// Represents a store product request service.
    /// </summary>
    public interface IProductStoreRequestService
    {
        /// <summary>
        /// Gets a list of product requests.
        /// </summary>
        /// <returns>A <see cref="Task{List{ProductStoreItemRequest}}"/>.</returns>
        Task<List<ProductStoreItemRequest>> GetRequestsAsync();

        /// <summary>
        /// Gets a product request with specified identifier.
        /// </summary>
        /// <param name="requestId">A product request identifier.</param>
        /// <returns>A <see cref="Task{ProductStoreItemRequest}"/>.</returns>
        Task<ProductStoreItemRequest> GetRequestAsync(int requestId);

        /// <summary>
        /// Creates a new products request.
        /// </summary>
        /// <param name="createRequest">A <see cref="UpdateRequestRequest"/>.</param>
        /// <returns>A <see cref="Task{ProductStoreItemRequest}"/>.</returns>
        Task<ProductStoreItemRequest> CreateRequestAsync(UpdateRequestRequest createRequest);

        /// <summary>
        /// Sets completed status for a request.
        /// </summary>
        /// <param name="requestId">A request identifier.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        Task SetRequestCompletedAsync(int requestId);
    }
}
