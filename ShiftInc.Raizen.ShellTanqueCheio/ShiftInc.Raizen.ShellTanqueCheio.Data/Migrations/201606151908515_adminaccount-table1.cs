namespace ShiftInc.Raizen.ShellTanqueCheio.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminaccounttable1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsSending",
                c => new
                    {
                        idNewsSending = c.Int(nullable: false, identity: true),
                        Destination = c.String(maxLength: 80),
                        Subject = c.String(maxLength: 100),
                        MessageBody = c.String(),
                        Status = c.Short(nullable: false),
                        dtSending = c.DateTime(),
                    })
                .PrimaryKey(t => t.idNewsSending);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NewsSending");
        }
    }
}
