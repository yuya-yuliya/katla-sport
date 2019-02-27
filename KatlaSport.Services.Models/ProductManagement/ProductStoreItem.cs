namespace KatlaSport.Services.ProductManagement
{
    public class ProductStoreItem
    {
        /// <summary>
        /// Gets or sets a producr store item ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a product.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets a quantity of items of certain product in the location.
        /// </summary>
        public int Quantity { get; set; }
    }
}
