using System;
using System.Collections.Generic;
using Fizz.SalesOrder.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Test
{
    public class TestBase
    {
        protected ModuleFixture ModuleFixture { get; }

        protected SalesContext SalesContext { get; }

        public TestBase(ModuleFixture fixture)
        {
            
            this.ModuleFixture = fixture;
            this.SalesContext = fixture.Services.GetRequiredService<SalesContext>();
            fixture.Reset();
        }

      
        protected T GetRequiredService<T>()
        {
            return this.ModuleFixture.Services.GetRequiredService<T>();
        }

        protected T MockEmptyEntity<T>(Action<T> action)
            where T:SalesCommonBase
        {
            return this.ModuleFixture.MockEmptyEntity<T>(action);
        }

        protected List<T> MockEntities<T>(Action<T, int> action, int count)
            where T:SalesCommonBase
        {
            return this.ModuleFixture.MockEnitities<T>(action, count);
        }
    }

}
