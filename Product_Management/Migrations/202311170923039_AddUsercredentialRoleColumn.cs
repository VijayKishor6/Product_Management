namespace Product_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsercredentialRoleColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserCredentials", "role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserCredentials", "role");
        }
    }
}
