using DAL;
using Entidades;
using System;
using System.Data.Entity;
using System.Linq;

namespace BLL
{
    public class RepositorioEvaluacion : RepositorioBase<Evaluacion>
    {
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
        public override bool Modificar(Evaluacion entity)
        {
            bool paso = false;
            Evaluacion Anterior = Buscar(entity.EvaluacionID);
            Contexto db = new Contexto();
            try
            {
                using (Contexto contexto = new Contexto())
                {
                    foreach (var item in Anterior.EvaluacionDetalle.ToList())
                    {
                        if (!entity.EvaluacionDetalle.Exists(x => x.DetalleID == item.DetalleID))
                        {
                            contexto.Entry(item).State = EntityState.Deleted;
                        }
                    }
                    contexto.SaveChanges();
                }
                foreach (var item in entity.EvaluacionDetalle)
                {
                    var estado = EntityState.Unchanged;
                    if (item.DetalleID == 0)
                        estado = EntityState.Added;
                    db.Entry(item).State = estado;
                }
                db.Entry(entity).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            { throw; }
            finally
            { db.Dispose(); }
            return paso;
        }
    }
}