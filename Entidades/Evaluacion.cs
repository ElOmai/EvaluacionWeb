using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Evaluacion
    {
        [Key]
        public int EvaluacionID { get; set; }
        public DateTime Fecha { get; set; }
        public int EstudianteId { get; set; }
        [ForeignKey("EstudianteId")]
        public virtual Estudiante Estudiantes { get; set; }
        public string NombreEstudiante { get; set; }
        public decimal TotalPerdido { get; set; }
        public virtual List<EvaluacionDetalle> EvaluacionDetalle { get; set; }

        public Evaluacion(int evaluacionID, DateTime fecha, int estudianteId, decimal totalPerdido)
        {
            EvaluacionID = evaluacionID;
            Fecha = fecha;
            EstudianteId = estudianteId;
            NombreEstudiante = string.Empty;
            Estudiantes = new Estudiante();
            TotalPerdido = totalPerdido;
            EvaluacionDetalle = new List<EvaluacionDetalle>();
        }
        public Evaluacion()
        {
            EvaluacionID = 0;
            Fecha = DateTime.Now;
            EstudianteId = 0;
            NombreEstudiante = string.Empty;
            TotalPerdido = 0;
            EvaluacionDetalle = new List<EvaluacionDetalle>();
        }
        public void AgregarButton(int detalleID, int evaluacionID, int categoriaId, decimal valor, decimal logrado, decimal perdido)
        {
            EvaluacionDetalle.Add(new EvaluacionDetalle(detalleID, evaluacionID, categoriaId, valor, logrado, perdido));
        }
        public void RemoverDetalle(int index)
        {
            this.EvaluacionDetalle.RemoveAt(index);
        }
    }

}