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
        public virtual Estudiantes Estudiantes { get; set; }
        public decimal TotalPerdido { get; set; }
        public virtual List<DetalleEvaluacion> DetalleEvaluacion { get; set; }

        public Evaluacion(int evaluacionID, DateTime fecha, int estudianteId, decimal totalPerdido)
        {
            EvaluacionID = evaluacionID;
            Fecha = fecha;
            EstudianteId = estudianteId;
            Estudiantes = new Estudiantes();
            TotalPerdido = totalPerdido;
            DetalleEvaluacion = new List<DetalleEvaluacion>();
        }
        public Evaluacion()
        {
            EvaluacionID = 0;
            Fecha = DateTime.Now;
            EstudianteId = 0;
            TotalPerdido = 0;
            DetalleEvaluacion = new List<DetalleEvaluacion>();
        }
        public void AgregarDetalle(int detalleID, int evaluacionID, int categoriaId, decimal valor, decimal logrado, decimal perdido)
        {
            DetalleEvaluacion.Add(new DetalleEvaluacion(detalleID, evaluacionID, categoriaId, valor, logrado, perdido));
        }
        public void RemoverDetalle(int index)
        {
            this.DetalleEvaluacion.RemoveAt(index);
        }
    }

}