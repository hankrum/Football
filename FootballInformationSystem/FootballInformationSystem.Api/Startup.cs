using FootballInformationSystem.Data;
using FootballInformationSystem.Data.Repository;
using FootballInformationSystem.Data.Services;
using FootballInformationSystem.Data.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FootballInformationSystem.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            this.RegisterData(services);
            this.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        private void RegisterData(IServiceCollection services)
        {
            services.AddDbContext<MsSqlDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("FootballContext"),
                b => b.MigrationsAssembly("FootballInformationSystem.Api")));

            services.BuildServiceProvider().GetService<MsSqlDbContext>().Database.Migrate();

            services.AddScoped(typeof(IEfRepository<>), typeof(EFRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ITeamsService, TeamsService>();
            services.AddTransient<IMapper, Mapper>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
