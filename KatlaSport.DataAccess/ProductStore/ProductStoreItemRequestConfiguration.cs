using System.Data.Entity.ModelConfiguration;

namespace KatlaSport.DataAccess.ProductStore
{
    internal sealed class ProductStoreItemRequestConfiguration : EntityTypeConfiguration<ProductStoreItemRequest>
    {
        public ProductStoreItemRequestConfiguration()
        {
            ToTable("product_store_items_request");
            HasKey(r => r.Id);
            HasRequired(r => r.Item).WithMany(i => i.Requests).HasForeignKey(r => r.StoreItemId);
            Property(r => r.Id).HasColumnName("product_store_items_request_id").IsRequired();
            Property(r => r.StoreItemId).HasColumnName("product_store_items_request_item_id").IsRequired();
            Property(r => r.Quantity).HasColumnName("product_store_items_request_quantity").IsRequired();
            Property(r => r.Completed).HasColumnName("product_store_items_request_completed").IsRequired();
        }
    }
}
