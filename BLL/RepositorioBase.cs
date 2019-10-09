using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RepositorioBase<T> : IDisposable, IRepository<T> where T : class
    {
        public RepositorioBase()
        {

        }
        ~RepositorioBase()
        {

        }
        public virtual T Buscar(int id)
        {
            T entity;
            Contexto db = new Contexto();
            try
            {
                entity = db.Set<T>().Find(id);
            }
            catch (Exception)
            { throw; }
            finally
            { db.Dispose(); }
            return entity;
        }

        public void Dispose()
        {

        }

        public virtual bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                T entity = db.Set<T>().Find(id); ;
                db.Set<T>().Remove(entity);
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            { throw; }
            finally
            { db.Dispose(); }
            return paso;
        }
        public virtual List<T> GetList(Expression<Func<T, bool>> expression)
        {
            Contexto db = new Contexto();
            List<T> Lista = new List<T>();
            try
            {
                Lista = db.Set<T>().AsNoTracking().Where(expression).ToList();
            }
            catch (Exception)
            { throw; }
            finally
            { db.Dispose(); }
            return Lista;
        }
        public virtual bool Guardar(T entity)
        {
            Contexto _db = new Contexto();
            bool paso = false;
            try
            {
                if (_db.Set<T>().Add(entity) != null)
                    paso = _db.SaveChanges() > 0;
            }
            catch (Exception)
            { throw; }
            finally
            { _db.Dispose(); }
            return paso;
        }
        public virtual bool Modificar(T entity)
        {
            Contexto _db = new Contexto();
            bool paso = false;
            try
            {
                _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                paso = (_db.SaveChanges() > 0);
            }
            catch (Exception)
            { throw; }
            finally
            { _db.Dispose(); }
            return paso;
        }
    }
}
