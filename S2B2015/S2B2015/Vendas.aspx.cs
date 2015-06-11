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
                //if (GetVendas().Count() > 0)
                //    lblitenSolicitados.Text = "Foram solicitadas a venda dos seguintes itens:";
                //if (GetPerguntas().Count() > 0)
                //    lblPerguntas.Text = "Foram realizadas perguntas aos seguintes itens";
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
                                                     nAvaliacao = a.nAvaliacao
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
                                                     nAvaliacao = a.nAvaliacao
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
                        nPtId = pt.PerguntaId,
                        nAvaliacao = a.nAvaliacao
                    };
            return querys;
        }

        public IQueryable<PerguntaViewModel> GetPerguntasRespondidas()
        {
            S2BStoreEntities _db = new S2BStoreEntities();


            IQueryable<PerguntaViewModel> query = (from perg in _db.Perguntas
                                                   join prod in _db.Produtos on perg.ProdutoId equals prod.ProdutoId
                                                   where prod.UsuarioId == (from u in _db.Usuarios
                                                                            where u.strEmail == Context.User.Identity.Name
                                                                            select u.UsuarioId).FirstOrDefault()
                                                   select new PerguntaViewModel
                                                   {
                                                       quemRespondeu = prod.UsuarioId,
                                                       ProdutoId = prod.ProdutoId,
                                                       StrTituloProduto = prod.strTitulo,
                                                       StrLinkProduto = prod.strLink,
                                                       strPergunta = perg.strPergunta,
                                                       strRespostas = perg.strRespostas,
                                                       dtPergunta = perg.dtPergunta,
                                                       dtResposta = perg.dtResposta,
                                                       quemPerguntou = perg.UsuarioId
                                                   });


            return query;
        }

        public IQueryable<PerguntaViewModel> GetPerguntasFeitas()
        {
            S2BStoreEntities _db = new S2BStoreEntities();


            IQueryable<PerguntaViewModel> query = (from perg in _db.Perguntas
                                                   join prod in _db.Produtos on perg.ProdutoId equals prod.ProdutoId
                                                   where perg.UsuarioId == (from u in _db.Usuarios
                                                                            where u.strEmail == Context.User.Identity.Name
                                                                            select u.UsuarioId).FirstOrDefault()
                                                   select new PerguntaViewModel
                                                   {
                                                       quemRespondeu = prod.UsuarioId,
                                                       ProdutoId = prod.ProdutoId,
                                                       StrTituloProduto = prod.strTitulo,
                                                       StrLinkProduto = prod.strLink,
                                                       strPergunta = perg.strPergunta,
                                                       strRespostas = perg.strRespostas,
                                                       dtPergunta = perg.dtPergunta,
                                                       dtResposta = perg.dtResposta,
                                                       quemPerguntou = perg.UsuarioId
                                                   });


            return query;
        }


        public IQueryable<ProdutoViewModel> GetProdutosComprados()
        {
            S2BStoreEntities _db = new S2BStoreEntities();

            IQueryable<ProdutoViewModel> query = from a in _db.Produtos
                                                 where a.CompradorId == (from u in _db.Usuarios
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
                                                     nAvaliacao = a.nAvaliacao
                                                 };

            string strTotal = query.AsEnumerable().Sum(o => o.Preco).ToString();
            string strNumeroItens = query.Count().ToString();
            lblProdutosComprados.Text = "Suas compras: Você comprou: " + strNumeroItens + " Itens que somam um total de  R$" + strTotal;

            return query;
        }


        public IQueryable<ProdutoViewModel> GetProdutosVendidos()
        {
            S2BStoreEntities _db = new S2BStoreEntities();

            IQueryable<ProdutoViewModel> query = from a in _db.Produtos
                                                 where a.nEstado == 2 &&
                                                    a.UsuarioId == (from u in _db.Usuarios
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
                                                     nAvaliacao = a.nAvaliacao
                                                 };

            string strTotal = query.AsEnumerable().Sum(o => o.Preco).ToString();
            string strNumeroItens = query.Count().ToString();
            lblProdutosVendidos.Text = "Suas vendas: Você vendeu: " + strNumeroItens + " Itens que somam um total de  R$" + strTotal;

            var queryAvaliacao = from a in query
                                 where a.nAvaliacao > 0
                                 select new
                                 {
                                     a.nAvaliacao
                                 };

            if (queryAvaliacao.Count() > 0)
            {

                float Rating = queryAvaliacao.AsEnumerable().Sum(o => o.nAvaliacao) / queryAvaliacao.Count();

                RatingUsuario.CurrentRating = (int)Rating;
            }
            return query;
        }

        public IQueryable<ProdutoViewModel> GetProdutosValidos()
        {
            S2BStoreEntities _db = new S2BStoreEntities();

            IQueryable<ProdutoViewModel> query = from a in _db.Produtos
                                                 where a.nEstado == 0 &&
                                                    a.UsuarioId == (from u in _db.Usuarios
                                                                    where u.strEmail == Context.User.Identity.Name
                                                                    select u.UsuarioId).FirstOrDefault() &&
                                                                    a.bAtivada == true

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
                                                     nAvaliacao = a.nAvaliacao
                                                 };

            string strTotal = query.AsEnumerable().Sum(o => o.Preco).ToString();
            string strNumeroItens = query.Count().ToString();
            lblAnunciosValidos.Text = "Seus Anuncios: Você posui: " + strNumeroItens + " Anuncios validos que somam um total de  R$" + strTotal;


            return query;
        }

        public IQueryable<ProdutoViewModel> GetProdutosCancelados()
        {
            S2BStoreEntities _db = new S2BStoreEntities();

            IQueryable<ProdutoViewModel> query = from a in _db.Produtos
                                                 where a.UsuarioId == (from u in _db.Usuarios
                                                                    where u.strEmail == Context.User.Identity.Name
                                                                    select u.UsuarioId).FirstOrDefault() &&
                                                                    a.bAtivada == false

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
                                                     nAvaliacao = a.nAvaliacao
                                                 };

            return query;
        }


        

        protected void produtList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void AvaliarCompra(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            string str = ListViewProdutosComprados.SelectedIndex.ToString();
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/NovoProduto.aspx");
        }

        protected void Rating1_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            int value = int.Parse(e.Value);
            string result = string.Empty;
            switch (value)
            {
                case 0:
                    result = "Not yet rated";
                    break;
                case 1:
                    result = "bad!";
                    break;
                case 2:
                    result = "Not very good";
                    break;
                case 3:
                    result = "average";
                    break;
                case 4:
                    result = "Good";
                    break;
                case 5:
                    result = "very good";
                    break;

            }
        }
        
    }

}