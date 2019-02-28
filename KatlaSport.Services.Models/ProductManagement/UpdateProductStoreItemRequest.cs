using FluentValidation.Attributes;

namespace KatlaSport.Services.ProductManagement
{
    /// <summary>
    /// Represents a request for creating and updating a store product item.
    /// </summary>
    [Validator(typeof(UpdateProductStoreItemRequestValidator))]
    public class UpdateProductStoreItemRequest
    {
        /// <summary>
        /// Gets or sets a producr store item ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a product identifier.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets a hive section identifier.
        /// </summary>
        public int HiveSectionId { get; set; }

        /// <summary>
        /// Gets or sets a quantity of items of certain product in the location.
        /// </summary>
        public int Quantity { get; set; }
    }
}
