using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using S2B2015.Models;
namespace S2B2015
{
    public partial class Responder : System.Web.UI.Page
    {
        int ptid;
        protected void Page_Load(object sender, EventArgs e)
        {
            S2BStoreEntities _db = new S2BStoreEntities();
            if((int.TryParse(Request.QueryString["PerguntaId"], out ptid)))
            {
                if (!PermissaoResponder())
                    Response.Redirect("~/Default.aspx");
                else
                {
                    string strAskingUser =( from u in _db.Usuarios
                                           where u.UsuarioId == (int) (from pt in _db.Perguntas
                                                                 where pt.PerguntaId == ptid
                                                                 select pt.UsuarioId).FirstOrDefault()
                                           select u.strEmail).First().ToString();

                    lblHeader.Text = "Usuario " + strAskingUser +" perguntou:";
                    lblPergunta.Text = (from pt in _db.Perguntas
                                       where pt.PerguntaId == ptid
                                       select pt.strPergunta).First().ToString();
                }
            }
            else
                Response.Redirect("~/Default.aspx");

            
        }


        protected bool PermissaoResponder()
        {
            int prdid;
            S2BStoreEntities _db = new S2BStoreEntities();
            if (!User.Identity.IsAuthenticated)
                return false;
            if(int.TryParse(Request.QueryString["PerguntaId"], out prdid))
            {
                ;
            }
            //verificar se; item é do usuario
            return true;
        }
        protected void btnResponder_Click(object sender, EventArgs e)
        {
            if (txtResposta.Text == "")
                lblErroResposta.Text = "* Resposta inválida";
            else
            {
                S2BStoreEntities _db = new S2BStoreEntities();
                Pergunta pergunta = new Pergunta();
                pergunta= (from pt in _db.Perguntas
                           where ptid == pt.PerguntaId
                           select pt).First();
                pergunta.strRespostas = txtResposta.Text;
                pergunta.dtResposta = DateTime.Now;
                _db.SaveChanges();
            }
        }
    }
}