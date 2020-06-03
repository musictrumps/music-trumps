using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TrumpEngine.Core;
using TrumpEngine.Data;
using TrumpEngine.Data.Providers.Interface;

namespace TrumpEngine.Api.Configuration
{
    public class DependencyInjection
    {
        private IServiceCollection ServiceCollection { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public DependencyInjection(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;

        }

        public void ConfigureData()
        {
            ServiceCollection.AddTransient<BandCore>();
            ServiceCollection.AddTransient<BandData>();
        }
    }
}
