namespace IndividualProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        MessageData = c.String(maxLength: 250),
                        Receiver_UserName = c.String(maxLength: 128),
                        Sender_UserName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Receiver_UserName)
                .ForeignKey("dbo.Users", t => t.Sender_UserName)
                .Index(t => t.Receiver_UserName)
                .Index(t => t.Sender_UserName);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Sender_UserName", "dbo.Users");
            DropForeignKey("dbo.Messages", "Receiver_UserName", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "Sender_UserName" });
            DropIndex("dbo.Messages", new[] { "Receiver_UserName" });
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
        }
    }
}
