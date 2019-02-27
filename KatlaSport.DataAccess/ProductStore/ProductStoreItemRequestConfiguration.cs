using System.Data.Entity.ModelConfiguration;

namespace KatlaSport.DataAccess.ProductStore
{
    internal sealed class ProductStoreItemRequestConfiguration : EntityTypeConfiguration<ProductStoreItemRequest>
    {
        public ProductStoreItemRequestConfiguration()
        {
            ToTable("product_store_items_request");
            HasKey(r => r.Id);
            HasRequired(r => r.Product).WithMany(i => i.Requests).HasForeignKey(r => r.ProductId);
            HasRequired(r => r.HiveSection).WithMany(i => i.Requests).HasForeignKey(r => r.HiveSectionId);
            Property(r => r.Id).HasColumnName("product_store_items_request_id");
            Property(r => r.Quantity).HasColumnName("product_store_items_request_quantity");
            Property(r => r.Completed).HasColumnName("product_store_items_request_completed");
            Property(r => r.HiveSectionId).HasColumnName("product_store_item_request_hive_section_id");
            Property(r => r.ProductId).HasColumnName("product_store_item_request_product_id");
        }
    }
}
