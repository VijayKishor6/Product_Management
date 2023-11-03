namespace Product_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProductWatchListShareLogs", new[] { "ProductWatchListId" });
            AlterColumn("dbo.ProductWatchListShareLogs", "ProductWatchListId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ProductWatchListShareLogs", "ProductWatchListId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductWatchListShareLogs", new[] { "ProductWatchListId" });
            AlterColumn("dbo.ProductWatchListShareLogs", "ProductWatchListId", c => c.Guid(nullable: false, identity: true));
            CreateIndex("dbo.ProductWatchListShareLogs", "ProductWatchListId");
        }
    }
}
