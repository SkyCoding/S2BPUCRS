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
                
        }


        public IQueryable<ProdutoViewModel> GetAlbuns()
        {
            
            String filtro = null;

            
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
                                                   CategoriaId = a.CategoriaId
                                               };

            if (filtro != null)
            {
                switch (oTipoPesquisa)
                {
                    case TiposPesquisas.Titulo:
                        query = query.Where(p => p.strTitulo.Contains(filtro));
                        break;
                    case TiposPesquisas.Categoria:
                        int nTempCategoria = 0;
                        int.TryParse(filtro, out nTempCategoria);
                        query = query.Where(p => p.CategoriaId == nTempCategoria);
                        break;

                }

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