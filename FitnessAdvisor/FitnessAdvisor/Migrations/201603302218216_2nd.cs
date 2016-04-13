namespace FitnessAdvisor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2nd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CurrentWeights", "today", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CurrentWeights", "today");
        }
    }
}
