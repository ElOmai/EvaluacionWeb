using System;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    [Serializable]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        public string Descripcion { get; set; }
        public decimal PromedioPerdida { get; set; }
        public DateTime Fecha { get; set; }

        public Categoria()
        {
            CategoriaId = 0;
            Descripcion = string.Empty;
            PromedioPerdida = 0;
            Fecha = DateTime.Now;
        }

        public Categoria(int categoriaId, string descripcion, decimal promedioPerdida, DateTime fecha)
        {
            CategoriaId = categoriaId;
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
            PromedioPerdida = promedioPerdida;
            Fecha = fecha;
        }
    }
}
