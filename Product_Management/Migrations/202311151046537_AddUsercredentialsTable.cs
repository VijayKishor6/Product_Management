namespace Product_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsercredentialsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCredentials",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserCredentials");
        }
    }
}
