using Entidades;
using System.Data.Entity;

namespace DAL
{
    public class Contexto : DbContext
    {
        public virtual DbSet<Evaluacion> Evaluaciones { get; set; }
        public virtual DbSet<Estudiante> Estudiantes { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }

        public Contexto() : base("ConStr")
        {

        }
    }
}