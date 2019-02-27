using System.Data.Entity.Migrations;

namespace KatlaSport.DataAccess.Migrations
{
    /// <summary>
    /// Add product store item request migration.
    /// </summary>
    public partial class AddProductStoreItemRequest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.product_store_items_request",
                c => new
                    {
                        product_store_items_request_id = c.Int(nullable: false, identity: true),
                        product_store_item_request_product_id = c.Int(nullable: false),
                        product_store_item_request_hive_section_id = c.Int(nullable: false),
                        product_store_items_request_quantity = c.Int(nullable: false),
                        product_store_items_request_completed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.product_store_items_request_id)
                .ForeignKey("dbo.product_hive_sections", t => t.product_store_item_request_hive_section_id, cascadeDelete: true)
                .ForeignKey("dbo.catalogue_products", t => t.product_store_item_request_product_id, cascadeDelete: true)
                .Index(t => t.product_store_item_request_product_id)
                .Index(t => t.product_store_item_request_hive_section_id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.product_store_items_request", "product_store_item_request_product_id", "dbo.catalogue_products");
            DropForeignKey("dbo.product_store_items_request", "product_store_item_request_hive_section_id", "dbo.product_hive_sections");
            DropIndex("dbo.product_store_items_request", new[] { "product_store_item_request_hive_section_id" });
            DropIndex("dbo.product_store_items_request", new[] { "product_store_item_request_product_id" });
            DropTable("dbo.product_store_items_request");
        }
    }
}
