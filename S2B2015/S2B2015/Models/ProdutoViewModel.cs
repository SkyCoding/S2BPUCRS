using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S2B2015.Models
{
    public class ProdutoViewModel
    {
        public int ProdutoId { get; set; }
        public DateTime dtPublicação { get; set; }
        public string strTitulo { get; set; }
        public string strLink { get; set; }
        public string strDescrição { get; set; }

        public int nValidade { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria oCategoria { get; set; }
        public float Preco { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario ousuario { get; set; }

        public bool bAtivada { get; set; }
        public int nEstado { get; set; }
        public string strPergunta { get; set; }
        public int nPtId { get; set; }


        public int CompradorID { get; set; }
        public virtual Usuario oComprador { get; set; }

    }
}