namespace S2B2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class produtoidonperguntas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Perguntas", "ProdutoId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Perguntas", "ProdutoId");
        }
    }
}
