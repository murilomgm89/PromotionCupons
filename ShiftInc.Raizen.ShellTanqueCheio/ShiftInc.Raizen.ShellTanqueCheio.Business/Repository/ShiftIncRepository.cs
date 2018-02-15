using ShiftInc.Raizen.ShellTanqueCheio.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShiftInc.Raizen.ShellTanqueCheio.Business.Repository
{
    public class ShiftIncRepository
    {
        public static T GetById<T>(int id) where T : class
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.Set<T>().Find(id);                
            }
        }

        public static IEnumerable<T> GetWhere<T>(Expression<Func<T, bool>> match) where T : class
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.Set<T>().Where(match).ToList();
            }
        }

        public static void Save<T>(T entity) where T : class
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                var entitySet = context.Set(typeof(T));

                string idPropertyName = "id" + typeof(T).Name.Split('_')[0];

                object entityID = GetPropValue(entity, idPropertyName);

                if (Convert.ToInt32(entityID) == default(int))
                {
                    entitySet.Add(entity);
                }
                else
                {   
                    entitySet.Attach(entity);
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                    foreach (var prop in entity.GetType().GetProperties())
                    {
                        if (Attribute.IsDefined(prop.PropertyType, typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute)))
                        {
                            object navProperty = GetPropValue(entity, prop.Name);
                            if (navProperty != null)
                            {
                                context.Entry(navProperty).State = System.Data.Entity.EntityState.Modified;
                            }
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        private static object GetPropValue(object obj, String name)
        {
            if (obj == null) { return null; }

            Type type = obj.GetType();
            PropertyInfo info = type.GetProperty(name);
            if (info == null) { return null; }

            obj = info.GetValue(obj, null);

            return obj;
        }

        private static void SetPropValue(object obj, String name, object value)
        {
            if (obj == null) { return; }

            Type type = obj.GetType();
            PropertyInfo info = type.GetProperty(name);
            if (info == null) { return; }

            info.SetValue(obj, value);
        }
    }
}
