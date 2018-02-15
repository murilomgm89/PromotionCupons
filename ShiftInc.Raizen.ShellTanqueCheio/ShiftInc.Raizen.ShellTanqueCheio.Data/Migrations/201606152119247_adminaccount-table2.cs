namespace ShiftInc.Raizen.ShellTanqueCheio.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminaccounttable2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Receipt", "isValidated", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Receipt", "isValidated");
        }
    }
}
