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
                        product_store_items_request_item_id = c.Int(nullable: false),
                        product_store_items_request_quantity = c.Int(nullable: false),
                        product_store_items_request_completed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.product_store_items_request_id)
                .ForeignKey("dbo.product_store_items", t => t.product_store_items_request_item_id, cascadeDelete: true)
                .Index(t => t.product_store_items_request_item_id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.product_store_items_request", "product_store_items_request_item_id", "dbo.product_store_items");
            DropIndex("dbo.product_store_items_request", new[] { "product_store_items_request_item_id" });
            DropTable("dbo.product_store_items_request");
        }
    }
}
