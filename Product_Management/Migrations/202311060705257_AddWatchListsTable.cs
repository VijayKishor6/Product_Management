namespace Product_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWatchListsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WatchLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        LastModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedBy = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WatchLists", "UserId", "dbo.Users");
            DropIndex("dbo.WatchLists", new[] { "UserId" });
            DropTable("dbo.WatchLists");
        }
    }
}
