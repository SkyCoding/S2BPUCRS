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
            Validade
        }

        TiposPesquisas oTipoPesquisa = TiposPesquisas.Titulo;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sFilter = Request.QueryString["Filtro"];

            oTipoPesquisa = TiposPesquisas.Titulo;              

            sFilter = Request.QueryString["Categoria"];
                       
            oTipoPesquisa = TiposPesquisas.Categoria;

            Pesquisa(sFilter);
                
        }

        void Pesquisa(string sFiltro)
        {

            
            switch(oTipoPesquisa)
            {
                case TiposPesquisas.Titulo:
                    PesquisaTitulo();
                    break;
                case TiposPesquisas.Categoria:
                    PesquisaCategoria();
                    break;

            }

            
                        

            

        }

        void PesquisaTitulo()
        {

            S2BStoreEntities _db = new S2BStoreEntities();

            var oProd = (from a in _db.Produtos
                         where a.strTitulo.Contains(Request.QueryString["Filtro"])
                         select new
                         {
                             a.strLink,
                             a.strTitulo,
                             a.Preco,
                             a.CategoriaId
                         });//).FirstOrDefault();

            //quando for datas;
            //.OrderByDescending(x => x.Delivery.SubmissionDate);
            var min = (from p in oProd
                       orderby p.Preco ascending
                       select p.Preco).FirstOrDefault();

            var max = (from p in oProd
                       orderby p.Preco descending
                       select p.Preco).FirstOrDefault();


            lblTitulo.Text = "Resultado da pesquia por: " + Request.QueryString["Filtro"];
            lblNumeroResultados.Text = "Foram encontrados " + oProd.Count().ToString() + " resultados";
            lblValores.Text = "O valores variam entre " + min.ToString() + " reais e " + max.ToString() + " reais.";



            grdProdutos.DataSource = oProd.ToList();


            grdProdutos.DataBind();

        }

        void PesquisaCategoria()
        {

            S2BStoreEntities _db = new S2BStoreEntities();

            int nCategoria = 0;

            int.TryParse(Request.QueryString["Categoria"], out nCategoria);

            var oProd = (from a in _db.Produtos
                         where a.CategoriaId == nCategoria
                         select new
                         {
                             a.strLink,
                             a.strTitulo,
                             a.Preco,
                             a.CategoriaId
                         });//).FirstOrDefault();

            //quando for datas;
            //.OrderByDescending(x => x.Delivery.SubmissionDate);
            var min = (from p in oProd
                       orderby p.Preco ascending
                       select p.Preco).FirstOrDefault();

            var max = (from p in oProd
                       orderby p.Preco descending
                       select p.Preco).FirstOrDefault();


            lblTitulo.Text = "Resultado da pesquia por: " + Request.QueryString["Categoria"];
            lblNumeroResultados.Text = "Foram encontrados " + oProd.Count().ToString() + " resultados";
            lblValores.Text = "O valores variam entre " + min.ToString() + " reais e " + max.ToString() + " reais.";



            grdProdutos.DataSource = oProd.ToList();


            grdProdutos.DataBind();

        }

        protected void grdProdutos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            int idx = (int)e.Row.RowIndex;
            
            //string value = grdProdutos.Rows[1].Cells["uri"].Value.ToString();
            //ThreadPool.QueueUserWorkItem(delegate
            //{
            //        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("value");
            //        myRequest.Method = "GET";
            //        HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            //        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(myResponse.GetResponseStream());
            //        myResponse.Close();
            //        grdProdutos.Rows[idx].Cells["Img"].Value = bmp;
            //});
            
        }
    }
}