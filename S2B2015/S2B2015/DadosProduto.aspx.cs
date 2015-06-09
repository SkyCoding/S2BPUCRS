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
            //StoreEntities _db = new StoreEntities();
            //Load_Data(_db);

            //gwDadosProd.DataSource = _db.Produtos.ToList();
            //gwDadosProd.DataBind();



            int id;
            string param =  Request.QueryString["ProdutoId"];

            if (!Int32.TryParse(param, out  id))
            {
                lblTitulo.Text = "Produto não encontrado!";
                btnComprar.Visible = false;
            }
            else
            {
                S2BStoreEntities _db = new S2BStoreEntities();
                Produto query = (Produto)(from a in _db.Produtos
                                where a.ProdutoId == id
                                select a).FirstOrDefault();

                string sFiltro = "Kit";

                var oProd = (from a in _db.Produtos
                             where a.strTitulo.Contains(@"/" + sFiltro + "/")
                                select a);//).FirstOrDefault();

                List<Produto> llll = oProd.ToList();


                if (query != null)
                {
                    if (query.bAtivada == false && query.nEstado == 3)
                    {
                        btnComprar.Visible = false;  
                    }
                    ViewState["id"] = id;
                    lblTitulo.Text = query.strTitulo;
                    imgProduto.ImageUrl = query.strLink;
                    lblCategoria.Text = query.oCategoria.strTitulo;
                    lblPreco.Text = query.Preco.ToString();
                    
                    HtmlString o = new HtmlString(query.strDescrição);

                    string s = o.ToHtmlString();
                    string a = o.ToString();


                    String myEncodedString;

                    //myEncodedString = HttpUtility.HtmlEncode(query.strDescrição);

                    myEncodedString = HttpUtility.HtmlDecode(query.strDescrição);

                    //System.IO.StringWriter myWriter = new System.IO.StringWriter();

                    //HttpUtility.HtmlDecode(myEncodedString, myWriter);
                    
                    HtmlGenericControl divControl = new HtmlGenericControl("div");


                    divControl.Attributes.Add("id", "divDescricao");
                    divControl.Attributes.Add("class", "col-md-9");
                    //divControl.Attributes.Add("innerHtml", "bob");
                    divControl.InnerHtml = myEncodedString;
                    divControl.Visible = true;
                    phDescricao.Controls.Add(divControl);


                    //txtDescricao.Text = query.strDescrição;
                    //txtDescricao.Text = Server.HtmlDecode(myEncodedString);


                }
                else
                {
                    lblTitulo.Text = "Solicitação inválida";
                    btnComprar.Visible = false;
                }
            }
        




        }

        void Load_Data(S2BStoreEntities db)
        {


            imgProduto.ImageUrl = "";

        }



    }
}