namespace Product_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductWatchListProductTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductWatchListProducts",
                c => new
                    {
                        ProductWatchListProductId = c.Guid(nullable: false, identity: true),
                        ProductId = c.Guid(nullable: false),
                        ProductWatchListId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ProductWatchListProductId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductWatchLists", t => t.ProductWatchListId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ProductWatchListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductWatchListProducts", "ProductWatchListId", "dbo.ProductWatchLists");
            DropForeignKey("dbo.ProductWatchListProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductWatchListProducts", new[] { "ProductWatchListId" });
            DropIndex("dbo.ProductWatchListProducts", new[] { "ProductId" });
            DropTable("dbo.ProductWatchListProducts");
        }
    }
}
