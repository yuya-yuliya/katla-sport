namespace KatlaSport.Services.ProductManagement
{
    public class ProductStoreItemRequest
    {
        /// <summary>
        /// Gets or sets a product store item request ID.
        /// </summary>
        public int Id { get; set; }

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
