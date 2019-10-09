using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Serializable]
    public class EvaluacionDetalle
    {
        [Key]
        public int DetalleID { get; set; }
        public int EvaluacionID { get; set; }
        [ForeignKey("EvaluacionID")]
        public virtual Evaluacion Evaluaciones { get; set; }
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public virtual Categoria Categorias { get; set; }
        [NotMapped]
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
        public decimal Logrado { get; set; }
        public decimal Perdido { get; set; }

        public EvaluacionDetalle(int detalleID, int evaluacionID, int categoriaId, decimal valor, decimal logrado, decimal perdido)
        {
            DetalleID = detalleID;
            EvaluacionID = evaluacionID;
            CategoriaId = categoriaId;
            Categoria = string.Empty;
            Valor = valor;
            Logrado = logrado;
            Perdido = perdido;
        }
        public EvaluacionDetalle()
        {
            DetalleID = 0;
            EvaluacionID = 0;
            CategoriaId = 0;
            Categoria = string.Empty;
            Valor = 0;
            Logrado = 0;
            Perdido = 0;
        }
    }
}
