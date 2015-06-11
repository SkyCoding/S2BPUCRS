namespace S2B2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class compradordoproduto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produtoes", "UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.Produtoes", new[] { "UsuarioId" });
            AddColumn("dbo.Produtoes", "CompradorId", c => c.Int(nullable: false));
            AddColumn("dbo.Produtoes", "oComprador_UsuarioId", c => c.Int());
            AddColumn("dbo.Produtoes", "ousuario_UsuarioId", c => c.Int());
            CreateIndex("dbo.Produtoes", "oComprador_UsuarioId");
            CreateIndex("dbo.Produtoes", "ousuario_UsuarioId");
            AddForeignKey("dbo.Produtoes", "oComprador_UsuarioId", "dbo.Usuarios", "UsuarioId");
            AddForeignKey("dbo.Produtoes", "ousuario_UsuarioId", "dbo.Usuarios", "UsuarioId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produtoes", "ousuario_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Produtoes", "oComprador_UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.Produtoes", new[] { "ousuario_UsuarioId" });
            DropIndex("dbo.Produtoes", new[] { "oComprador_UsuarioId" });
            DropColumn("dbo.Produtoes", "ousuario_UsuarioId");
            DropColumn("dbo.Produtoes", "oComprador_UsuarioId");
            DropColumn("dbo.Produtoes", "CompradorId");
            CreateIndex("dbo.Produtoes", "UsuarioId");
            AddForeignKey("dbo.Produtoes", "UsuarioId", "dbo.Usuarios", "UsuarioId", cascadeDelete: true);
        }
    }
}
