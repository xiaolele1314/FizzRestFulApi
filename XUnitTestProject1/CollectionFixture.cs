using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    using Xunit;

    [CollectionDefinition("CollectionFixture")]
    public class CollectionFixture:ICollectionFixture<ModuleFixture>
    {

    }
}
