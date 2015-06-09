using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2B2015.Models
{
    public class Usuario
    {

        public int UsuarioId { get; set; }
        public string strEmail { get; set; }
        public string strSenha { get; set; }
        public string strNome { get; set; }


        //public int ApplicationUserManagerId { get; set; }
        //public virtual ApplicationUserManager oUserManager { get; set; }

    }
}
