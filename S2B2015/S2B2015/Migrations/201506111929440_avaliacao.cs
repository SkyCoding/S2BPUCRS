namespace S2B2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class avaliacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produtoes", "nAvaliacao", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produtoes", "nAvaliacao");
        }
    }
}
