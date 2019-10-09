using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Serializable]
    public class Estudiante
    {
        [Key]
        public int EstudianteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        
        public string NombreCompleto
        {
            get { return $"{Nombre} {Apellido}"; }
        }
        public decimal PuntosPerdidos { get; set; }
        public DateTime Fecha { get; set; }
        public Estudiante()
        {
            EstudianteId = 0;
            Nombre = string.Empty;
            Apellido = string.Empty;
            PuntosPerdidos = 0;
            Fecha = DateTime.Now;
        }
        public Estudiante(int estudianteId, string nombre, string apellido, decimal puntosPerdidos, DateTime fecha)
        {
            EstudianteId = estudianteId;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Apellido = apellido ?? throw new ArgumentNullException(nameof(apellido));
            PuntosPerdidos = puntosPerdidos;
            Fecha = fecha;
        }
       
    }
}
