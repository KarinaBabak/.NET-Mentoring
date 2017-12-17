namespace Task2_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version_13 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Region", newName: "Regions");
            AddColumn("dbo.Customers", "FoundationDate", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Regions", newName: "Region");
            DropColumn("dbo.Customers", "FoundationDate");
        }
    }
}
