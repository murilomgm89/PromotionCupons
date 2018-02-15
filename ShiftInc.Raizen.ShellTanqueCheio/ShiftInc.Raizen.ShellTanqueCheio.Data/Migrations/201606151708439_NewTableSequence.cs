namespace ShiftInc.Raizen.ShellTanqueCheio.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTableSequence : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SequenceSeries", "value", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SequenceSeries", "value");
        }
    }
}
