using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RepositorioEvaluacion : RepositorioBase<Evaluaciones>
    {
        public override Evaluaciones Buscar(int id)
        {
            Evaluaciones Evaluaciones = new Evaluaciones();
            Contexto db = new Contexto();
            try
            {

                Evaluaciones = db.Evaluaciones.Include(x => x.DetalleEvaluaciones)
                    .Where(x => x.EvaluacionID == id).FirstOrDefault();
            }
            catch (Exception)
            { throw; }
            finally
            { db.Dispose(); }
            return Evaluaciones;
        }
        public override bool Modificar(Evaluaciones entity)
        {
            bool paso = false;
            Evaluaciones Anterior = Buscar(entity.EvaluacionID);
            Contexto db = new Contexto();
            try
            {
                using (Contexto contexto = new Contexto())
                {
                    foreach (var item in Anterior.DetalleEvaluaciones.ToList())
                    {
                        if (!entity.DetalleEvaluaciones.Exists(x => x.DetalleID == item.DetalleID))
                        {
                            contexto.Entry(item).State = EntityState.Deleted;
                        }
                    }
                    contexto.SaveChanges();
                }
                foreach (var item in entity.DetalleEvaluaciones)
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