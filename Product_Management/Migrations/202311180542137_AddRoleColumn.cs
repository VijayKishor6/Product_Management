namespace Product_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserCredentials", "role", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserCredentials", "role", c => c.String());
        }
    }
}
