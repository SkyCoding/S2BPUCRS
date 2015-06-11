using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S2B2015.Models
{
    public class PerguntaViewModel
    {
        public int PerguntaId { get; set; }
        public int UsuarioId { get; set; }

        public virtual Usuario oUsuario { get; set; }

        public string strPergunta { get; set; }

        public DateTime dtPergunta { get; set; }

        public string strRespostas { get; set; }

        public DateTime dtResposta { get; set; }

        public int ProdutoId { get; set; }
    }
}