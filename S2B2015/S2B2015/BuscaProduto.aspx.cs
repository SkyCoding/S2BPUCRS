using S2B2015.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace S2B2015
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        enum TiposPesquisas
        {
            Titulo,
            Preco, 
            Categoria, 
            Vendedor, 
            Validade,
            Vendidos
        }

        TiposPesquisas oTipoPesquisa = TiposPesquisas.Titulo;

        protected void Page_Load(object sender, EventArgs e)
        {
                S2BStoreEntities _db = new S2BStoreEntities();
                if(!IsPostBack)
                { 
                    var query = from c in _db.Categorias
                                select c.strTitulo;
                    foreach (var i in query)
                     ListaCategorias.Items.Add(i);
                    var usuarios = from u in _db.Usuarios
                                   select u.strEmail;
                    foreach (var u in usuarios)
                        ListaUsuarios.Items.Add(u);
                 }
        
        }

        int userid;
        public IQueryable<ProdutoViewModel> GetProdutos()
        {
            
            String filtro = null;
            /*
            if (Request.QueryString["Usuario"] != null)
            {
                filtro = Request.QueryString["Usuario"];
                oTipoPesquisa = TiposPesquisas.Vendedor;
            }
            if (Context.User.Identity.Name == "admin@s2b.edu.br")
            {
                if (Request.QueryString["Vendidos"] != null)
                {
                    filtro = Request.QueryString["Vendidos"];
                    oTipoPesquisa = TiposPesquisas.Vendidos;
                }
            }*/
            S2BStoreEntities _db = new S2BStoreEntities();
            if(User.Identity.IsAuthenticated)
                 userid = (from u in _db.Usuarios
                            where u.strEmail == Context.User.Identity.Name
                            select u.UsuarioId).First();
            IQueryable<ProdutoViewModel> query = from a in _db.Produtos
                                                 select new ProdutoViewModel
                                               {
                                                   strLink = a.strLink,
                                                   dtPublicação = a.dtPublicação,
                                                   ProdutoId = a.ProdutoId,
                                                   strTitulo = a.strTitulo,
                                                   strDescrição = a.strDescrição,
                                                   Preco = a.Preco,
                                                   oCategoria = a.oCategoria,//a.oCategoria.strTitulo,
                                                   nEstado = a.nEstado,
                                                   CategoriaId = a.CategoriaId,
                                                   UsuarioId = a.UsuarioId,
                                                   bAtivada=a.bAtivada,
                                                   CompradorID = a.CompradorId
                                               };

            if (Request.QueryString["Categoria"] != null)
            {
                filtro = Request.QueryString["Categoria"];
                int nTempCategoria = 0;
                int.TryParse(filtro, out nTempCategoria);
                query = query.Where(p => p.CategoriaId == nTempCategoria);

                var oCategoria = (from p in query
                                  select p.oCategoria.strTitulo).FirstOrDefault();
                if (oCategoria != null)
                    lblTitulo.Text = "Resultados da pesquia pela categoria: " + oCategoria.ToString();

            }
            if (Request.QueryString["Filtro"] != null)
            {
                filtro = Request.QueryString["Filtro"];
                query = query.Where(p => p.strTitulo.Contains(filtro));


                if (query != null)
                    lblTitulo.Text = "Resultados da pesquia por: " + filtro;

            }

            

            if (Request.QueryString["Descrição"] != null)
            {
                filtro = Request.QueryString["Descrição"];
                query = query.Where(p => p.strDescrição.Contains(filtro));

            }

            if (Request.QueryString["Usuario"] != null)
            {
                filtro = Request.QueryString["Usuario"];
                int nUsuario = 0;
                int.TryParse(filtro, out nUsuario);
                query = query.Where(p =>p.UsuarioId == nUsuario);

                var strVendedor = (from a in _db.Usuarios
                         where a.UsuarioId == nUsuario
                               select a.strNome).FirstOrDefault();


                if (strVendedor != null)
                    lblTitulo.Text = "Resultados da pesquia pelo vendedor: " + strVendedor.ToString();


            }
            if (Request.QueryString["Min"] != null)
            {
                int nprecomin;
               if(int.TryParse(Request.QueryString["Min"],out nprecomin))
               {
                   query = query.Where(p => p.Preco >= nprecomin);
               }

            }

            if (Request.QueryString["Max"] != null)
            {
                int nprecomax;
                if (int.TryParse(Request.QueryString["Max"], out nprecomax))
                {
                    query = query.Where(p => p.Preco <= nprecomax);
                }

            }
            if (User.Identity.IsAuthenticated)
            {
                if (Request.QueryString["Vendidos"] != null)
                {
                    filtro = Request.QueryString["Vendidos"];
                    bool blnVendido;
                    bool.TryParse(filtro, out blnVendido);
                    if (blnVendido == true)
                        query = query.Where(p => p.nEstado == 2);
                    if (User.Identity.Name != "admi@s2b.edu.br")
                        query = query.Where(p => p.UsuarioId == userid);
                }

                if (Request.QueryString["Comprados"] != null)
                {
                    filtro = Request.QueryString["Comprados"];
                    bool blnComprados;
                    bool.TryParse(filtro, out blnComprados);
                    if (blnComprados == true)
                    {
                        filtro = Request.QueryString["Comprador"];
                        int nUsuario = 0;
                        int.TryParse(filtro, out nUsuario);
                        query = query.Where(p => p.CompradorID == nUsuario);
                    }
                }
                if (User.Identity.Name != "admi@s2b.edu.br")
                {
                    query = query.Where(p => p.bAtivada == true);
                    
                    query = new EnumerableQuery<ProdutoViewModel>(query.ToList().Where(p => p.dtPublicação.AddDays(p.nValidade) < DateTime.Now));
                }
            }
            else         
                query = query.Where(p => p.bAtivada == true && p.nEstado < 2);        
            
            if (filtro != null)
            {
                var min = (from p in query
                           orderby p.Preco ascending
                           select p.Preco).FirstOrDefault();

                var max = (from p in query
                           orderby p.Preco descending
                           select p.Preco).FirstOrDefault();


                lblNumeroResultados.Text = "Foram encontrados: " + query.Count().ToString() + " resultados";
                lblValores.Text = "O valores variam entre R$" + min.ToString() + " e R$" + max.ToString() +".";
            }

            return query;
        }

        protected void btnPesquisar_Click (object sender, EventArgs e)
        {
            S2BStoreEntities _db = new S2BStoreEntities();

            string url = "~/BuscaProduto?";
            if (txtNomeCategoria.Text != "")
                url += "&Filtro=" + txtNomeCategoria.Text;
            if (txtDescricaoCategoria.Text != "")
                url += "&Descrição=" + txtDescricaoCategoria.Text;
            if (ListaUsuarios.SelectedValue != "")
            {
                string strusuario = (from u in _db.Usuarios
                                     where u.strEmail == ListaUsuarios.SelectedValue
                                    select u.UsuarioId).FirstOrDefault().ToString();
                url += "&Usuario=" + strusuario;
            }
           
            
            int cat = (from c in _db.Categorias
                      where c.strTitulo == ListaCategorias.SelectedValue
                      select c.CategoriaId).FirstOrDefault();
            if(cat!=0)
                url += "&Categoria=" + cat;

            if (txtMin.Text!= "")
            {
                int npreçomin;
                if(int.TryParse(txtMin.Text, out npreçomin))
                {
                    url += "&Min=" + npreçomin;
                }
            }
            if (txtMax.Text != "")
            {
                int npreçomax;
                if (int.TryParse(txtMax.Text, out npreçomax))
                {
                    url += "&Max=" + npreçomax;
                }
            }
           // url += Categoria
            Response.Redirect(url);
           
        }
    }
}