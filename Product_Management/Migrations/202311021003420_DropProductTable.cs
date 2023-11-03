namespace Product_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropProductTable : DbMigration
    {
        public override void Up()
        {
                    
           DropTable("dbo.ProductWatchListProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductWatchListProducts",
                c => new
                    {
                        ProductWatchListProductId = c.Guid(nullable: false, identity: true),
                        ProductId = c.Guid(nullable: false, identity: true),
                        ProductWatchListId = c.Guid(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ProductWatchListProductId);           
            
        }
    }
}
