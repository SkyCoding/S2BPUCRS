using S2B2015.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace S2B2015
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //S2BStoreEntities _db = new S2BStoreEntities();
            
            //GridView1.DataSource = _db.Categorias.ToList();
            //GridView1.DataBind();

        }

        public IQueryable<Categoria> GetCategories()
        {
            S2BStoreEntities _db = new S2BStoreEntities();
            IQueryable<Categoria> query = _db.Categorias;
            return query;
            
        }

    }
}