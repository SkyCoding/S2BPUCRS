using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using S2B2015.Models;
namespace S2B2015
{
    public partial class Vendas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            S2BStoreEntities _db = new S2BStoreEntities();
            string userEmail = Context.User.Identity.Name;
            if(!User.Identity.IsAuthenticated )
                Response.Redirect("~/Default.aspx");
            if (userEmail != "" && userEmail!="admin@s2b.eud.br")
            {
                /*int userid = (from u in _db.Usuarios
                              where u.strEmail == userEmail
                              select u.UsuarioId).First();
                var queryProd = from p in _db.Produtos
                                where p.UsuarioId == userid
                                select p;*/
                if (GetVendas().Count() > 0)
                    lblitenSolicitados.Text = "Foram solicitadas a venda dos seguintes itens:";
                if (GetPerguntas().Count() > 0)
                    lblPerguntas.Text = "Foram realizadas perguntas aos seguintes itens";
            }

        }
        public IQueryable<ProdutoViewModel> GetVendas()
        {
            S2BStoreEntities _db = new S2BStoreEntities();

            IQueryable<ProdutoViewModel> query = from a in _db.Produtos
                                                 where a.nEstado == 1 &&
                                                                         a.UsuarioId == (from u in _db.Usuarios
                                                                         where u.strEmail== Context.User.Identity.Name
                                                                         select u.UsuarioId).FirstOrDefault()
                                                 select new ProdutoViewModel
                                                 {
                                                     strLink = a.strLink,
                                                     ProdutoId = a.ProdutoId,
                                                     strTitulo = a.strTitulo,
                                                     Preco = a.Preco,
                                                     oCategoria = a.oCategoria,//a.oCategoria.strTitulo,
                                                     nEstado = a.nEstado,
                                                     CategoriaId = a.CategoriaId,
                                                     UsuarioId = a.UsuarioId,
                                                     bAtivada = a.bAtivada,
                                                 };
            
            return query;
            
        }
        public IQueryable<ProdutoViewModel> GetPerguntas()
        {
            S2BStoreEntities _db = new S2BStoreEntities();

            IQueryable<ProdutoViewModel> query = from a in _db.Produtos
                                                 where a.nEstado <2 && a.UsuarioId == (from u in _db.Usuarios
                                                        where u.strEmail == Context.User.Identity.Name
                                                        select u.UsuarioId).FirstOrDefault()
                                                 select new ProdutoViewModel
                                                 {
                                                     strLink = a.strLink,
                                                     ProdutoId = a.ProdutoId,
                                                     strTitulo = a.strTitulo,
                                                     Preco = a.Preco,
                                                     oCategoria = a.oCategoria,//a.oCategoria.strTitulo,
                                                     nEstado = a.nEstado,
                                                     CategoriaId = a.CategoriaId,
                                                     UsuarioId = a.UsuarioId,
                                                     bAtivada = a.bAtivada,
                                                     strPergunta="",
                                                     nPtId = 0,
                                                 };
            var perguntas = from pt in _db.Perguntas
                            where  (from p in _db.Produtos
                                                   where p.nEstado <2 && p.UsuarioId == (from u in _db.Usuarios
                                                                                        where u.strEmail == Context.User.Identity.Name
                                                                                           select u.UsuarioId).FirstOrDefault()
                                    select p.ProdutoId).Contains(pt.ProdutoId) && pt.strRespostas ==""
                            select pt;
            
            var querys = from a in query
                    join pt in perguntas
                    on  a.ProdutoId equals pt.ProdutoId
                    select new ProdutoViewModel
                    {
                        strLink = a.strLink,
                        ProdutoId = a.ProdutoId,
                        strTitulo = a.strTitulo,
                        Preco = a.Preco,
                        oCategoria = a.oCategoria,//a.oCategoria.strTitulo,
                        nEstado = a.nEstado,
                        CategoriaId = a.CategoriaId,
                        UsuarioId = a.UsuarioId,
                        bAtivada = a.bAtivada,
                        strPergunta = pt.strPergunta,
                        nPtId = pt.PerguntaId
                    };
            return querys;
        }


        protected void produtList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
    }

}