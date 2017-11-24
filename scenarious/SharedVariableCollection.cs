using System;
using System.Collections.Generic;
using System.Text;
using DotNetCoreXUnit1.scenarious;
using Xunit;


namespace DotNetCoreXUnit1
{
    [CollectionDefinition("SharedVariableCollection")]
    public class SharedVariableCollection : ICollectionFixture<LaunchSettingsFixture>
    {
    }
}
