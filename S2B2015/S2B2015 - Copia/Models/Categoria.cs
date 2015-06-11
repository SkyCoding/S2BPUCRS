using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2B2015.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string strTitulo { get; set; }
        public string strDescrição { get; set; }

        public Categoria()
        { }

        public Categoria(string Titulo, string Descrição)
        {
            this.strTitulo = Titulo;
            this.strDescrição = Descrição;
        }
    }
}
