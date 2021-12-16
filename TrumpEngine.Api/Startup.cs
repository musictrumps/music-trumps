using AspNetCore.Firebase.Authentication.Extensions;
using Firebase.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using TrumpEngine.Api.Configuration;
using TrumpEngine.Shared.Settings;

namespace TrumpEngine.Api
{
    public class Startup
    {
        readonly string allowSpecificOrigins = "_allowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private Settings _settings;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(allowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("*")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });

            services.AddControllers();
            _settings = Configuration.Get<Settings>();
            services.AddSingleton(_settings);
            new DependencyInjection(services).ConfigureData();

            services.AddSingleton<IFirebaseAuthService>(u => new FirebaseAuthService(
                new FirebaseAuthOptions
                {
                    WebApiKey = _settings.Firebase.WebApiKey
                }
            ));
            services.AddFirebaseAuthentication(_settings.Firebase.Issuer, _settings.Firebase.ProjectId);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(allowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
