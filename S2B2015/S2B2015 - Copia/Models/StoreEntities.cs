using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace S2B2015.Models
{
    public class StoreEntities : DbContext
    {


        public StoreEntities() : base("StoreConnection")
        {
        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pergunta> Perguntas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }

}

