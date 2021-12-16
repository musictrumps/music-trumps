using Microsoft.Extensions.DependencyInjection;
using TrumpEngine.Api.Security;
using TrumpEngine.Scraper.Core;
using TrumpEngine.Scraper.Data;

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
            ServiceCollection.AddSingleton<IAccountService, AccountService>();

        }
    }
}
