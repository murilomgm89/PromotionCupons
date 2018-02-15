namespace ShiftInc.Raizen.ShellTanqueCheio.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminaccounttable9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Receipt", "isValidated", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Receipt", "isValidated", c => c.Boolean(nullable: false));
        }
    }
}
