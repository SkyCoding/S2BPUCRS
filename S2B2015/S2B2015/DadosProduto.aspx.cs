using S2B2015.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace S2B2015
{
    public partial class DadosProduto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!PermissaoPergunta())
            {
                btnSalvar.Enabled = false;
            }
            if(!PermissaoCancelar())
            {
                btnCancelar.Visible = false;
            }

            int id;
            string param =  Request.QueryString["ProdutoId"];

            if (!Int32.TryParse(param, out  id))
            {
                lblTitulo.Text = "Produto não encontrado!";
                btnComprar.Visible = false;
                btnVender.Visible = false;
            }
            else
            {
                S2BStoreEntities _db = new S2BStoreEntities();
                int userid = (from u in _db.Usuarios
                              where u.strEmail == Context.User.Identity.Name
                              select u.UsuarioId).FirstOrDefault();
                Produto query;
                if(Context.User.Identity.Name =="admin@s2b.edu.br")
                         query = (Produto)(from a in _db.Produtos
                                where a.ProdutoId == id
                                select a).FirstOrDefault();
                else
                         query = (Produto)(from a in _db.Produtos
                                where a.ProdutoId == id
                                && (userid == a.UsuarioId || userid == a.CompradorId || (a.nEstado < 2 && a.bAtivada == true))
                                select a).FirstOrDefault();
                if (query != null)
                {
                    if (query.CompradorId == userid && userid != 0)
                    {
                        btnCancelar.Visible = false;
                        btnComprar.Visible = false;
                        lblRating.Text = "Avalie o vendedor!";
                    }
                    else
                    {

                        AtualizaRatingVendedor();
                    }
                }


                if (query != null)
                {
                    if (Context.User.Identity.Name == "")
                    {
                        //btnComprar.Visible = false;
                        btnVender.Visible = false;
                    }
                    if (query.UsuarioId == userid)
                    {
                        btnComprar.Visible = false;
                        if (query.nEstado != 1)
                            btnVender.Visible = false;
                    }
                    else
                    {
                        if (Context.User.Identity.Name == "admin@s2b.edu.br")
                            btnComprar.Visible = false;
                            btnVender.Visible = false;
                        

                    }

                    ViewState["id"] = id;
                    lblTitulo.Text = query.strTitulo;
                    imgProduto.ImageUrl = query.strLink;
                    lblPreco.Text = "R$ " + query.Preco.ToString("0.00");

                    CarregaPerguntas();

                    String myEncodedString;
                    
                    myEncodedString = HttpUtility.HtmlDecode(query.strDescrição);
                    
                    HtmlGenericControl divControl = new HtmlGenericControl("div");


                    divControl.Attributes.Add("id", "divDescricao");
                    divControl.Attributes.Add("class", "col-md-9");

                    divControl.InnerHtml = myEncodedString;
                    divControl.Visible = true;
                    phDescricao.Controls.Add(divControl);                                        

                }
                else
                {
                    lblTitulo.Text = "Solicitação inválida";
                    btnComprar.Visible = false;
                    btnVender.Visible = false;
                }
            }       




        }

        public void AtualizaRatingVendedor()
        {
            S2BStoreEntities _db = new S2BStoreEntities();            

            int nProdId;
            int.TryParse(Request.QueryString["ProdutoId"], out nProdId);

            IQueryable<ProdutoViewModel> query = from a in _db.Produtos
                                                 where a.nEstado == 2 &&
                                                    a.UsuarioId == (from u in _db.Produtos
                                                                    where u.ProdutoId == nProdId
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



            var queryAvaliacao = from a in query
                                 where a.nAvaliacao > 0
                                 select new
                                 {
                                     a.nAvaliacao
                                 };

            if (queryAvaliacao.Count() > 0)
            {

                float Rating = queryAvaliacao.AsEnumerable().Sum(o => o.nAvaliacao) / queryAvaliacao.Count();

                RatingCompra.CurrentRating = (int)Rating;
            }


            
        }


        bool PermissaoCancelar ()
        {
            if (UsuarioVendedor() && !ProdutoCancelado())
                return true;
            return false;
        }
        bool ProdutoCancelado()
        {
            S2BStoreEntities _db = new S2BStoreEntities();
            int id ;
            if(int.TryParse(Request.QueryString["ProdutoId"],out id))
            {

                bool nstatus = (from p in _db.Produtos
                               where p.ProdutoId == id
                               select p.bAtivada).First();
                if (nstatus == false)
                    return true;
                else
                    return false;
            }
            return false;
        }
        bool PermissaoPergunta ()
        {
            if (!User.Identity.IsAuthenticated)
                return false;
            if(Context.User.Identity.Name=="admin@s2b.edu.br")
                return false;
            if(UsuarioVendedor())
                return false;
            return true;

        }
        bool UsuarioVendedor()
        {
            int prdid;
            if (int.TryParse(Request.QueryString["ProdutoId"], out prdid))
            {
                S2BStoreEntities _db = new S2BStoreEntities();
                int userid = (from u in _db.Usuarios
                              where u.strEmail == Context.User.Identity.Name
                              select u.UsuarioId).FirstOrDefault();
                var query = (from p in _db.Produtos
                             where prdid == p.ProdutoId
                             select p).FirstOrDefault();
                if (userid == null)
                    return false;
                else
                    if (query.UsuarioId == userid)
                        return true;
                    else
                        return false;
            }
            else
                return false;
        }


        protected void btnComprar_Click(object sender, EventArgs e)
        {
            if (Context.User.Identity.Name == "")
                Response.Redirect("~/Account/Register.aspx"); 
            else
            {

                int nProdId;
                if (int.TryParse(Request.QueryString["ProdutoId"], out nProdId))
                {
                    S2BStoreEntities _db = new S2BStoreEntities();

                    int userid = (from u in _db.Usuarios
                                  where u.strEmail == Context.User.Identity.Name
                                  select u.UsuarioId).FirstOrDefault();


                    var query = (from p in _db.Produtos
                                 where p.ProdutoId == nProdId
                                 select p).First();
                    query.nEstado = 1;
                    query.CompradorId = userid;
                    _db.SaveChanges();
                }
            }

        }

        protected void AvaliarCompra(object sender, AjaxControlToolkit.RatingEventArgs e)
        {

            int nProdId;
            if (int.TryParse(Request.QueryString["ProdutoId"], out nProdId))
            {
                S2BStoreEntities _db = new S2BStoreEntities();             

                int userid = (from u in _db.Usuarios
                              where u.strEmail == Context.User.Identity.Name

                              select u.UsuarioId).FirstOrDefault();

                Produto queryComprador;
                
                queryComprador = (Produto)(from a in _db.Produtos
                    where a.ProdutoId == nProdId && userid == a.CompradorId
                    select a).FirstOrDefault();

                if (queryComprador != null)
                {

                    var query = (from p in _db.Produtos
                                 where p.ProdutoId == nProdId
                                 select p).First();
                    query.nAvaliacao = RatingCompra.CurrentRating;
                    _db.SaveChanges();
                }
            }

        }

        protected void btnVender_Click(object sender, EventArgs e)
        {
            int nProdId;
            int.TryParse(Request.QueryString["ProdutoId"], out nProdId);
            S2BStoreEntities _db = new S2BStoreEntities();
            var query = (from p in _db.Produtos
                         where p.ProdutoId == nProdId
                         select p).First();
            query.nEstado = 2;
            _db.SaveChanges();
        }

        protected void btnListaUsuario_Click(object sender, EventArgs e)
        {
            int nProdId;
            int.TryParse(Request.QueryString["ProdutoId"], out nProdId);
            S2BStoreEntities _db = new S2BStoreEntities();

            var nIdVendedor = (from a in _db.Produtos
                         where a.ProdutoId == nProdId
                               select a.UsuarioId).First();
            
            Response.Redirect("~/BuscaProduto.aspx?Usuario=" + nIdVendedor.ToString());      

        }

        protected void btnListaCategoria_Click(object sender, EventArgs e)
        {
            int nProdId;
            int.TryParse(Request.QueryString["ProdutoId"], out nProdId);
            S2BStoreEntities _db = new S2BStoreEntities();

            var nIdCategoria = (from a in _db.Produtos
                               where a.ProdutoId == nProdId
                               select a.CategoriaId).First();

            Response.Redirect("~/BuscaProduto.aspx?Categoria=" + nIdCategoria.ToString());      

        }

        

        int get_userid()
        {
            S2BStoreEntities _db = new S2BStoreEntities();
            int id = (from u in _db.Usuarios
                     where u.strEmail== Context.User.Identity.Name
                     select u.UsuarioId).First();
            return id;
        }
        protected void  btnEnviarPergunta_Click(object sender, EventArgs e)
        {
            int nProdId;
            int.TryParse(Request.QueryString["ProdutoId"], out nProdId);
            S2BStoreEntities _db = new S2BStoreEntities();
            Pergunta pt = new Pergunta();
            pt.UsuarioId = get_userid();
            pt.dtPergunta = DateTime.Now;
            pt.strPergunta = txtPergunta.Text;
            pt.strRespostas = "";
            pt.dtResposta = DateTime.Now;
            pt.ProdutoId = nProdId;
            Usuario currentUser = (Usuario)(from a in _db.Usuarios
                                            where a.strEmail == User.Identity.Name
                                            select a).FirstOrDefault();
            pt.oUsuario = currentUser;
            _db.Perguntas.Add(pt);
            try
            {
                _db.SaveChanges();

               

            }
            catch (Exception e1)
            {

            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            int nProdId;
            int.TryParse(Request.QueryString["ProdutoId"], out nProdId);
            S2BStoreEntities _db = new S2BStoreEntities();
            var query = (from p in _db.Produtos
                         where p.ProdutoId == nProdId
                         select p).First();
            query.bAtivada = false;
            _db.SaveChanges();
        }

        protected void btncep_Click(object sender, EventArgs e)
        {
            int ncep;
            if (Int32.TryParse(txtCep.Text, out ncep) && txtCep.Text.Length == 8)
            {
                string strCepOrigem = "90619900";
                string strCepDest = txtCep.Text;
                string nCdEmpresa = string.Empty;
                string sDsSenha = string.Empty;
                string nCdServico = "40010";
                string nVlPeso = "1";
                int nCdFormato = 1;
                decimal nVlComprimento = 16;
                decimal nVlAltura = 2;
                decimal nVlLargura = 11;
                decimal nVlDiametro = 2;
                Correios.CalcPrecoPrazoWSSoapClient webServiceCorreios = new Correios.CalcPrecoPrazoWSSoapClient("CalcPrecoPrazoWSSoap");
                Correios.cResultado retornoCorreios = webServiceCorreios.CalcPrecoPrazo(nCdEmpresa, sDsSenha, nCdServico, strCepOrigem, strCepDest, nVlPeso, nCdFormato, nVlComprimento, nVlAltura, nVlLargura, nVlDiametro, "N", 0, "N");
                if (retornoCorreios.Servicos.Length > 0)
                {
                    // Se deu tudo certo, então retorna o valor
                    if (retornoCorreios.Servicos[0].Erro == "0")
                        lblFrete.Text = "Frete: R$" + retornoCorreios.Servicos[0].Valor.ToString();

                }
            }
            else
                lblerrofrete.Text = "*Por favor informe 8 dígitos ,somente números.";
            
        }















        public IQueryable<PerguntaViewModel> CarregaPerguntas()
        {

            int nProdId;
            int.TryParse(Request.QueryString["ProdutoId"], out nProdId);

            S2BStoreEntities _db = new S2BStoreEntities();

            IQueryable<PerguntaViewModel> query = (from a in _db.Perguntas
                                          where a.ProdutoId == nProdId
                                                   select new PerguntaViewModel
                                                 {
                                                     strPergunta = a.strPergunta,
                                                     dtPergunta = a.dtPergunta,
                                                     
                                                     strRespostas = a.strRespostas,
                                                     dtResposta = a.dtResposta
                                                     
                                                 });

            return query;
        }
    }
}