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

namespace S2B2015
{
    public partial class adminFrame : System.Web.UI.Page
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
                
        }


        public IQueryable<ProdutoViewModel> GetAlbuns()
        {
                      String filtro = null;

            /*
            if (Request.QueryString["Filtro"] != null)
            {
                filtro = Request.QueryString["Filtro"];
                oTipoPesquisa = TiposPesquisas.Titulo;
            }

            if (Request.QueryString["Categoria"] != null)
            {
                filtro = Request.QueryString["Categoria"];
                oTipoPesquisa = TiposPesquisas.Categoria;
            }

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

            IQueryable<ProdutoViewModel> query = from a in _db.Produtos
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
                                                   bAtivada=a.bAtivada,
                                               };
            if (Context.User.Identity.Name != "admin@s2b.edu.br")
                query = query.Where(p => p.bAtivada == true);

            if (Request.QueryString["Filtro"] != null)
            {
                filtro = Request.QueryString["Filtro"];
                query = query.Where(p => p.strTitulo.Contains(filtro));
                //oTipoPesquisa = TiposPesquisas.Titulo;
            }

            if (Request.QueryString["Categoria"] != null)
            {
                filtro = Request.QueryString["Categoria"];
                int nTempCategoria = 0;
                int.TryParse(filtro, out nTempCategoria);
                query = query.Where(p => p.CategoriaId == nTempCategoria);
                //oTipoPesquisa = TiposPesquisas.Categoria;
            }

            if (Request.QueryString["Usuario"] != null)
            {
                filtro = Request.QueryString["Usuario"];
                int nUsuario = 0;
                int.TryParse(filtro, out nUsuario);
                query = query.Where(p =>p.UsuarioId == nUsuario);
                //oTipoPesquisa = TiposPesquisas.Vendedor;
            }
            if (Context.User.Identity.Name == "admin@s2b.edu.br")
            {
                if (Request.QueryString["Vendidos"] != null)
                {
                    filtro = Request.QueryString["Vendidos"];
                    bool blnVendido;
                    bool.TryParse(filtro, out blnVendido);
                    if (blnVendido == true)
                        query = query.Where(p => p.nEstado == 3);
                    //oTipoPesquisa = TiposPesquisas.Vendidos;
                }
            }
            
            if (filtro != null)
            {
                /*switch (oTipoPesquisa)
                {
                    case TiposPesquisas.Titulo:
                        query = query.Where(p => p.strTitulo.Contains(filtro));
                        break;
                    case TiposPesquisas.Categoria:
                        int nTempCategoria = 0;
                        int.TryParse(filtro, out nTempCategoria);
                        query = query.Where(p => p.CategoriaId == nTempCategoria);
                        break;
                    case TiposPesquisas.Vendedor:
                        int nUsuario = 0;
                        int.TryParse(filtro, out nUsuario);
                        query = query.Where(p =>p.UsuarioId == nUsuario);
                        break;
                    case TiposPesquisas.Vendidos:
                        bool blnVendido;
                        bool.TryParse(filtro, out blnVendido);
                        if (blnVendido == true)
                            query = query.Where(p => p.nEstado == 3);
                        break;

                }*/

                var min = (from p in query
                           orderby p.Preco ascending
                           select p.Preco).FirstOrDefault();

                var max = (from p in query
                           orderby p.Preco descending
                           select p.Preco).FirstOrDefault();


                lblTitulo.Text = "Resultado da pesquia por: " + Request.QueryString["Categoria"];
                lblNumeroResultados.Text = "Foram encontrados " + query.Count().ToString() + " resultados";
                lblValores.Text = "O valores variam entre " + min.ToString() + " reais e " + max.ToString() + " reais.";
            }

            return query;
        }
    }
}