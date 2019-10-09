using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BLL
{
    public class RepositorioEvaluacion : RepositorioBase<Evaluacion>
    {
        public override bool Guardar(Evaluacion evaluaciones)
        {
            bool paso = false;
            RepositorioBase<Estudiante> repositorio = new RepositorioBase<Estudiante>();
            Estudiante estudiantes = repositorio.Buscar(evaluaciones.EstudianteId);
            estudiantes.PuntosPerdidos += evaluaciones.TotalPerdido;
            if (repositorio.Modificar(estudiantes))
            {
                repositorio.Dispose();
                paso = base.Guardar(evaluaciones);
            }
            CalcularPromedio();
            return paso;
        }
        public override Evaluacion Buscar(int id)
        {
            Evaluacion Evaluaciones = new Evaluacion();
            Contexto db = new Contexto();
            try
            {

                Evaluaciones = db.Evaluaciones.Include(x => x.EvaluacionDetalle)
                    .Where(x => x.EvaluacionID == id).FirstOrDefault();
            }
            catch (Exception)
            { throw; }
            finally
            { db.Dispose(); }
            return Evaluaciones;
        }
        public override bool Modificar(Evaluacion evaluaciones)
        {
            bool paso = false;
            decimal PuntosPerdidos = 0;
            Evaluacion Anterior = Buscar(evaluaciones.EvaluacionID);
            RepositorioBase<Estudiante> repositorio = new RepositorioBase<Estudiante>();
            Estudiante estudiantes = repositorio.Buscar(evaluaciones.EstudianteId);
            Contexto db = new Contexto();

            try
            {
                using (Contexto contexto = new Contexto())
                {
                    foreach (var item in Anterior.EvaluacionDetalle.ToList())
                    {
                        if (!evaluaciones.EvaluacionDetalle.Exists(x => x.DetalleID == item.DetalleID))
                        {
                            contexto.Entry(item).State = EntityState.Deleted;
                            estudiantes.PuntosPerdidos -= item.Perdido;
                        }
                    }
                    contexto.SaveChanges();
                }
                foreach (var item in evaluaciones.EvaluacionDetalle)
                {
                    var estado = EntityState.Unchanged;
                    if (item.DetalleID == 0)
                    {
                        estado = EntityState.Added;
                        estudiantes.PuntosPerdidos += item.Perdido;
                    }
                    db.Entry(item).State = estado;
                }
                estudiantes.PuntosPerdidos += PuntosPerdidos;
                repositorio.Modificar(estudiantes);
                db.Entry(evaluaciones).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
                CalcularPromedio();
            }
            catch (Exception)
            { throw; }
            finally
            { db.Dispose(); }
            return paso;
        }
        public override bool Eliminar(int id)
        {
            bool paso = false;
            RepositorioBase<Estudiante> repositorio = new RepositorioBase<Estudiante>();
            Evaluacion evaluaciones = Buscar(id);
            Estudiante estudiantes = repositorio.Buscar(evaluaciones.EstudianteId);
            estudiantes.PuntosPerdidos -= evaluaciones.TotalPerdido;
            if (repositorio.Modificar(estudiantes))
            {
                repositorio.Dispose();
                paso = base.Eliminar(id);
            }
            CalcularPromedio();
            return paso;
        }
        public override List<Evaluacion> GetList(Expression<Func<Evaluacion, bool>> expression)
        {
            List<Evaluacion> ListaEvaluaciones = new List<Evaluacion>();
            Contexto db = new Contexto();
            try
            {
                foreach (var item in base.GetList(expression))
                {
                    ListaEvaluaciones.Add(Buscar(item.EvaluacionID));
                }
            }
            catch (Exception)
            { throw; }
            finally
            { db.Dispose(); }
            return ListaEvaluaciones;
        }
        public void CalcularPromedio()
        {
            RepositorioBase<Categoria> repositorio = new RepositorioBase<Categoria>();
            List<Categoria> ListaCategorias = repositorio.GetList(x => true);
            List<Evaluacion> ListaEvaluaciones = GetList(x => true);
            Dictionary<int, decimal> Dic = new Dictionary<int, decimal>();
            decimal TotalPuntosPerdidos = 0;
            foreach (var item in ListaCategorias.ToList())
            {
                Dic.Add(item.CategoriaId, 0);
            }
            foreach (var item in ListaEvaluaciones.ToList())
            {
                TotalPuntosPerdidos += item.TotalPerdido;
                item.EvaluacionDetalle.ForEach(x => Dic[x.CategoriaId] += x.Perdido);
            }
            foreach (var item in Dic)
            {
                int Repeticiones = 0;
                decimal Promedio = 0;
                ListaEvaluaciones.ForEach(x => x.EvaluacionDetalle.ForEach(t =>
                {
                    if (t.CategoriaId == item.Key)
                        Repeticiones++;
                }));
                Categoria categorias = repositorio.Buscar(item.Key);
                if (Repeticiones > 0)
                    Promedio = item.Value / Repeticiones;
                else
                    Promedio = 0;
                categorias.PromedioPerdida = Promedio;
                repositorio.Modificar(categorias);
            }
        }
    }
}