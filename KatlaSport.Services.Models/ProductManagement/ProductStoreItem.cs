﻿namespace KatlaSport.Services.ProductManagement
{
    /// <summary>
    /// Represents a product store item.
    /// </summary>
    public class ProductStoreItem
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
