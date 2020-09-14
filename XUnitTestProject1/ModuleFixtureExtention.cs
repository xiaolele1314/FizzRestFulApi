using System;
using System.Collections.Generic;
using Fizz.SalesOrder.Models;

namespace Test
{
    public static class ModuleFixtureExtention
    {
        public static T MockEmptyEntity<T>(this ModuleFixture fixture, Action<T> action)
            where T:SalesCommonBase
        {
            using(var dbContext = fixture.CreatContext())
            {
                T entity = Activator.CreateInstance(typeof(T)) as T;
                action.Invoke(entity);
                dbContext.Set<T>().Add(entity);
                dbContext.SaveChanges();
                return entity;
                
            }
        }

        public static List<T> MockEnitities<T>(this ModuleFixture fixture, Action<T, int> action, int count )
            where T:SalesCommonBase
        {           
            List<T> entities = new List<T>();
            using (var dbContext = fixture.CreatContext())
            {
                for(int i=0; i<count; i++)
                {
                    T entity = Activator.CreateInstance(typeof(T)) as T;
                    action.Invoke(entity, i);

                    dbContext.Set<T>().Add(entity);
                    dbContext.SaveChanges();

                    entities.Add(entity);
                }                
                return entities;

            }
        }
    }
}
