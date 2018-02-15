namespace ShiftInc.Raizen.ShellTanqueCheio.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTableSequence2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Series", "qtdSort", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Series", "qtdSort");
        }
    }
}
