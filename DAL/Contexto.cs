using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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