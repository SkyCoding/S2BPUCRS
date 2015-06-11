using S2B2015.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace S2B2015
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //txtPesquisa.Visible = true;
            //S2BStoreEntities db = new S2BStoreEntities();
            //verificar as paginas na qual vai estar visivel
            //var query = from c in db.Categorias
             //           select c.strTitulo;
            //foreach (var p in query)
            //    drplstCategoria.Items.Add(p);
            if (Context.User.Identity.Name != "" && Context.User.Identity.Name != "admin@s2b.edu.br")
            {
                btnMinhasVendas.Visible = true;
                btnAdmin.Visible = false;
                S2BStoreEntities _db = new S2BStoreEntities();
                var query = (from u in _db.Usuarios
                             where u.strEmail == Context.User.Identity.Name
                             select u.UsuarioId).First();
                btnMinhasVendas.HRef += query;
                int nSolicitacoesCompra = (from p in _db.Produtos
                                           where p.UsuarioId == query && p.nEstado == 1
                                           select p).Count();
                int nPerguntas = (from pt in _db.Perguntas
                                  where (from p in _db.Produtos
                                         where p.nEstado < 2 && p.UsuarioId == (from u in _db.Usuarios
                                                                                where u.strEmail == Context.User.Identity.Name
                                                                                select u.UsuarioId).FirstOrDefault()
                                         select p.ProdutoId).Contains(pt.ProdutoId) && pt.strRespostas == ""
                                  select pt).Count();
                notificacoes.InnerText = (nSolicitacoesCompra + nPerguntas).ToString();
                //btnMeusProdutos.s
            }
            if (Context.User.Identity.Name == "admin@s2b.edu.br")
                btnAdmin.Visible = true;

        }

       

        protected void Pesquisar(object sender, EventArgs e)
        {

            Response.Redirect("~/BuscaProduto?Filtro=" + txtPesquisa.Text);            
        }


        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut();
        }
    }

}