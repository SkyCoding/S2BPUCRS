namespace S2B2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        strTitulo = c.String(),
                        strDescrição = c.String(),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Perguntas",
                c => new
                    {
                        PerguntaId = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        strPergunta = c.String(),
                        dtPergunta = c.DateTime(nullable: false),
                        strRespostas = c.String(),
                        dtResposta = c.DateTime(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PerguntaId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .ForeignKey("dbo.Produtoes", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        strEmail = c.String(),
                        strSenha = c.String(),
                        strNome = c.String(),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Produtoes",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false, identity: true),
                        dtPublicação = c.DateTime(nullable: false),
                        strTitulo = c.String(),
                        strLink = c.String(),
                        strDescrição = c.String(),
                        nValidade = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        Preco = c.Single(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                        bAtivada = c.Boolean(nullable: false),
                        nEstado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProdutoId)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.CategoriaId)
                .Index(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produtoes", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Produtoes", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.Perguntas", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Perguntas", "ProdutoId", "dbo.Produtoes");
            DropIndex("dbo.Produtoes", new[] { "UsuarioId" });
            DropIndex("dbo.Produtoes", new[] { "CategoriaId" });
            DropIndex("dbo.Perguntas", new[] { "UsuarioId" });
            DropTable("dbo.Produtoes");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Perguntas");
            DropTable("dbo.Categorias");
        }
    }
}
