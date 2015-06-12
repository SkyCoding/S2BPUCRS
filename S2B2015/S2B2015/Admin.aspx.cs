using S2B2015.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace S2B2015
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User.Identity.Name != "admin@s2b.edu.br")
                Response.Redirect("~/default.aspx");

            S2BStoreEntities _db = new S2BStoreEntities();

            if (!IsPostBack)
            {
                var query = from c in _db.Categorias
                            select c.strTitulo;
                foreach (var i in query)
                {
                    ListaCategorias.Items.Add(i);
                }

                var userQuery = from user in _db.Usuarios
                                where user.strEmail != "admin@s2b.edu.br"
                                select user.strEmail;

                drpListaUsuarios.Items.Clear();
                foreach (var u in userQuery)
                {
                    drpListaUsuarios.Items.Add(u);
                }
                drpListaUsuarios.DataBind();
                lblRelatorioItem.Text = "";
            }
            int userid;
            string filtro = Request.QueryString["Relatório"];
            if (filtro== "itens")
                this.relatorio_itens();
            if (int.TryParse(filtro, out userid))
            {
                this.relatorio_usuario(userid);
            }

            if(Request.QueryString["Relatório"] != null)
            {
                //collapseOne.Attributes["class"] = "panel-collapse collapse";
                //collapseTwo.Attributes["class"] = "panel-collapse collapse";
                //collapseThree.Attributes["class"] = "panel-collapse collapse in";

            }
            
        }


        protected void relatorio_usuario(int userid)
        {
            S2BStoreEntities _db = new S2BStoreEntities();
            var queryOfertas = (from p in _db.Produtos
                                where p.UsuarioId == userid /*(from u in _db.Usuarios
                                                      where u.strEmail == drpListaUsuarios.SelectedValue
                                                      select u.UsuarioId).FirstOrDefault()*/
                                select p);

            var queryCompras = (from p in _db.Produtos
                                where p.CompradorId == userid 
                                select p);

            var queryPerguntas = (from p in _db.Perguntas
                                  where p.UsuarioId == userid /*(from u in _db.Usuarios
                                                        where u.strEmail == drpListaUsuarios.SelectedValue
                                                        select u.UsuarioId).FirstOrDefault()*/
                                  select p);
            var queryRespostas = (from p in _db.Perguntas
                                  where (from pr in queryOfertas
                                         select pr.ProdutoId).Contains(p.ProdutoId)
                                             && p.strRespostas != ""
                                  select p);
            int nOfertas = queryOfertas.Count();
            int nVendidos = queryOfertas.Where(p => p.nEstado == 2).Count();
            int nComprados = queryCompras.Where(p => p.CompradorId == userid).Count();
            int nPerguntas = queryPerguntas.Count();
            int nRespostas = queryRespostas.Count();
            string strNomeUsuario = (from u in _db.Usuarios
                                    where u.UsuarioId == userid
                                    select u.strEmail).First();
            float nSomaOfertas=0;
            foreach (var p in queryOfertas)
                nSomaOfertas = nSomaOfertas + p.Preco;
            float nSomaVendas = 0;
            foreach (var p in queryOfertas.Where(p => p.nEstado == 2))
                nSomaVendas = nSomaVendas + p.Preco;
            double nTaxaVenda = ((double)nVendidos / (double)nOfertas) * 100;
            lblResultadoUsuarios.Text = "Relatório do Usuário: " + strNomeUsuario + ". Ofertou " + nOfertas + " itens, Vendeu "
                + nVendidos + " itens, Realizou " + nPerguntas + " perguntas e respondeu a " + nRespostas + " perguntas." +
                "Taxa de produtos vendidos = " + String.Format("{0:0.00}",nTaxaVenda)+ "%."+
                "Valor total total de todas suas ofertas: R$" + nSomaOfertas +". Valor total de suas vendas:R$"+nSomaVendas+".";
                

            lnkItensOfertados.Text = "Vizualizar os " + nOfertas + " produtos oferecidos.";
            lnkVendidos.Text = "Vizualizar os " + nVendidos + " produtos vendidos.";
            lnkComprados.Text = "Vizualizar os " + nComprados + " produtos comprados.";

            prodIframe.Attributes.Add("src", "~/adminFrame?Usuario=" + userid.ToString());
            prodIframe.Visible = true;

        }
        protected void relatorio_itens ()
        {
            S2BStoreEntities _db = new S2BStoreEntities();
            var query = from p in _db.Produtos
                        select p;
            int nItens = query.Count();
            //int nItensblock = query.Where(p => p.bAtivada == false).Count();
            int nBlockValidade = query.ToList().Where(p => p.dtPublicação.AddDays(p.nValidade) < DateTime.Now).Count();
            int nblockVendedor = query.Where(p => p.bAtivada == false && p.nEstado == 0).Count();
            int nVendidos = query.Where(p =>  p.nEstado == 2).Count();
            int nItensblock = nVendidos + nBlockValidade + nblockVendedor;
            lblRelatorioItem.Text= "Numero de itens anúnciados :" + nItens + ", numero de itens bloqueados:"+ nItensblock;
            lblRelatorioItem.Text +=", itens bloqueados por validade:" + nBlockValidade + ", itens bloqueados pelo vendedor:" + nblockVendedor + ", itens vendidos" + nVendidos;
        }
        protected void btnRelatorioItem_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin.aspx/?Relatório=itens");            
        }

        protected void btnCriar_Click(object sender, EventArgs e)
        {
            if (txtNomeCategoria.Text == "")
                lblerrorname.Text = "*Digite o nome da categoria";
            if (txtDescricaoCategoria.Text == "")
                lblerroDescr.Text = "*Digite a descrição da categoria";
            if( txtNomeCategoria.Text!= "" && txtDescricaoCategoria.Text !="")
            {
                S2BStoreEntities _db = new S2BStoreEntities();
                if (ListaCategorias.SelectedValue == "Nova Categoria")
                {
                    Categoria nova_categoria = new Categoria();
                    nova_categoria.strTitulo = txtNomeCategoria.Text;
                    nova_categoria.strDescrição = txtDescricaoCategoria.Text;
                    _db.Categorias.Add(nova_categoria);
                    _db.SaveChanges();
                }
                else
                {
                    var cat = (from c in _db.Categorias
                              where c.strTitulo == ListaCategorias.SelectedValue
                              select c).First();
                    cat.strTitulo = txtNomeCategoria.Text;
                    cat.strDescrição = txtDescricaoCategoria.Text;
                    _db.SaveChanges();
                }
                Response.Redirect("~/Admin.aspx");
            }

        }

        protected void btnRelatorioUsuarios_Click(object sender, EventArgs e)
        {
            S2BStoreEntities _db = new S2BStoreEntities();
            int userid;
            userid = (from u in _db.Usuarios
                     where u.strEmail == drpListaUsuarios.SelectedValue
                     select u.UsuarioId).First();
            Response.Redirect("~/Admin.aspx/?Relatório="+userid);
            
        }

        protected void drpListaUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblResultadoUsuarios.Text = "Relatório do Usuario:" + drpListaUsuarios.SelectedItem;
        }

        protected void ListaCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkItensOfertados_Click(object sender, EventArgs e)
        {
            int userid;
            if (int.TryParse(Request.QueryString["Relatório"], out userid))
            {
                this.relatorio_usuario(userid);
            }
            Response.Redirect("~/BuscaProduto?Usuario="+userid);
        }

        protected void lnkVendidos_Click(object sender, EventArgs e)
        {
            int userid;
            if (int.TryParse(Request.QueryString["Relatório"], out userid))
            {
                this.relatorio_usuario(userid);
            }
            Response.Redirect("~/BuscaProduto?Usuario=" + userid+ "&Vendidos=true");
        }

        protected void lnkComprados_Click(object sender, EventArgs e)
        {
            int userid;
            if (int.TryParse(Request.QueryString["Relatório"], out userid))
            {
                this.relatorio_usuario(userid);
            }
            Response.Redirect("~/BuscaProduto?Comprador=" + userid + "&Comprados=true");
        }



        public IQueryable<PerguntaViewModel> GetPerguntasRespondidas()
        {
            S2BStoreEntities _db = new S2BStoreEntities();


            IQueryable<PerguntaViewModel> query = (from perg in _db.Perguntas
                                                   join prod in _db.Produtos on perg.ProdutoId equals prod.ProdutoId
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
       

    }
}