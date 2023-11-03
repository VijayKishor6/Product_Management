namespace Product_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Guid(nullable: false, identity: true),
                        ProductName = c.String(),
                        ISIN = c.String(),
                        Ticker = c.String(),
                        FundPrimaryShareClass = c.Boolean(nullable: false),
                        FundType = c.String(),
                        ClassAssets = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalFundAssets = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductWatchListProducts",
                c => new
                    {
                        ProductWatchListProductId = c.Guid(nullable: false, identity: true),
                        ProductId = c.Guid(nullable: false, identity: true),
                        ProductWatchListId = c.Guid(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ProductWatchListProductId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductWatchLists", t => t.ProductWatchListId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ProductWatchListId);
            
            CreateTable(
                "dbo.ProductWatchLists",
                c => new
                    {
                        ProductWatchListId = c.Guid(nullable: false, identity: true),
                        ListName = c.String(nullable: false),
                        CreatedBy = c.Guid(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedBy = c.Guid(nullable: false, identity: true),
                        ModifiedAt = c.DateTime(nullable: false),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.ProductWatchListId);
            
            CreateTable(
                "dbo.ProductWatchListShareLogs",
                c => new
                    {
                        ProductWatchListShareLogId = c.Guid(nullable: false, identity: true),
                        ProductWatchListId = c.Guid(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        CreatedBy = c.Guid(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedBy = c.Guid(nullable: false, identity: true),
                        ModifiedAt = c.DateTime(nullable: false),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.ProductWatchListShareLogId)
                .ForeignKey("dbo.ProductWatchLists", t => t.ProductWatchListId, cascadeDelete: true)
                .Index(t => t.ProductWatchListId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        FullName = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 100),
                        ReportsTo = c.String(maxLength: 150),
                        Title = c.String(maxLength: 150),
                        IsActive = c.Boolean(nullable: false),
                        CreateDate = c.DateTimeOffset(nullable: false, precision: 7),
                        LastLoggedInDate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductWatchListShareLogs", "ProductWatchListId", "dbo.ProductWatchLists");
            DropForeignKey("dbo.ProductWatchListProducts", "ProductWatchListId", "dbo.ProductWatchLists");
            DropForeignKey("dbo.ProductWatchListProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductWatchListShareLogs", new[] { "ProductWatchListId" });
            DropIndex("dbo.ProductWatchListProducts", new[] { "ProductWatchListId" });
            DropIndex("dbo.ProductWatchListProducts", new[] { "ProductId" });
            DropTable("dbo.Users");
            DropTable("dbo.ProductWatchListShareLogs");
            DropTable("dbo.ProductWatchLists");
            DropTable("dbo.ProductWatchListProducts");
            DropTable("dbo.Products");
        }
    }
}
