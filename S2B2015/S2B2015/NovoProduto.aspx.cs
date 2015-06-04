using S2B2015.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace S2B2015
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ShowData();
            
        }

        void ShowData()
        {
            S2BStoreEntities _db = new S2BStoreEntities();
            FillData(_db);
        }

        private void FillData(S2BStoreEntities db)
        {
            var Query = from c in db.Categorias
                        select new
                        {
                            GenreID = c.CategoriaId,
                            GenreName = c.strTitulo
                        };

            cboCategorias.DataSource = Query.ToList();
            cboCategorias.DataValueField = "GenreID";
            cboCategorias.DataTextField = "GenreName";
            cboCategorias.DataBind();
        }

        protected void txtPreco_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnImage_Click(object sender, EventArgs e)
        {
            imgProduto.ImageUrl = txtImage.Text;
        }

        protected void txtImage_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            S2BStoreEntities _db = new S2BStoreEntities();

            Produto oProduto = new Produto();

            int nVal = 0;

            int.TryParse(txtValidade.Text, out nVal);


            int CategoriaId = 0;

            int.TryParse(cboCategorias.SelectedItem.Value, out CategoriaId);

            float nPreco = 0;

            float.TryParse(txtPreco.Text.Replace('.', ','), out nPreco);

            
            Usuario currentUser = (Usuario)(from a in _db.Usuarios
            where a.strEmail == User.Identity.Name
            select a).FirstOrDefault();


            oProduto.strLink = txtImage.Text;
            oProduto.strDescrição = txtDescricao.Text;
            oProduto.strTitulo = txtTitulo.Text;
            oProduto.UsuarioId = currentUser.UsuarioId;

            oProduto.CategoriaId = CategoriaId;
            oProduto.nValidade = nVal;
            oProduto.Preco = nPreco;

            _db.Produtos.Add(oProduto);

            try
            {
                _db.SaveChanges();

                string ss = "DadosProduto?ProdutoId=" + oProduto.ProdutoId;

                Response.Redirect("DadosProduto?ProdutoId=" + oProduto.ProdutoId);

            }catch(Exception e1)
            {

            }

            
        }

    }
}