using FluentValidation.Attributes;

namespace KatlaSport.Services.ProductManagement
{
    /// <summary>
    /// Represents a request for creating a store product items request.
    /// </summary>
    [Validator(typeof(UpdateRequestRequestValidator))]
    public class UpdateRequestRequest
    {
        /// <summary>
        /// Gets or sets a product ID.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets a location ID.
        /// </summary>
        public int HiveSectionId { get; set; }

        /// <summary>
        /// Gets or sets a quantity of items of product in request.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether certain request is completed.
        /// </summary>
        public bool Completed { get; set; }
    }
}
