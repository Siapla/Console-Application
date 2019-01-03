namespace IndividualProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReadState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "ReadState", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "ReadState");
        }
    }
}
