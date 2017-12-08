using Friends.Data;
using Friends.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Friends
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            InitializeDatabase(app, env);
            app.UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<FriendDbContext>();
            services.AddScoped<IFriendRepository, FriendRepository>();
        }

        private void InitializeDatabase(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<FriendDbContext>();

                if (!env.IsProduction())
                {
                    dbContext.Database.EnsureDeleted();
                    dbContext.Database.EnsureCreated();

                    // seed data
                    var repository = serviceScope.ServiceProvider.GetRequiredService<IFriendRepository>();
                    repository.AddFriend(new Models.Friend { Name = "Kailey", Email = "kailey@test.com", BirthDate = new System.DateTime(1991, 8, 13) });
                    repository.AddFriend(new Models.Friend { Name = "Rad", Email = "rad@test.com", BirthDate = new System.DateTime(1992, 9, 07) });
                    repository.AddFriend(new Models.Friend { Name = "Gale", Email = "gale@test.com", BirthDate = new System.DateTime(1993, 10, 5) });
                    repository.AddFriend(new Models.Friend { Name = "Walter", Email = "walter@test.com", BirthDate = new System.DateTime(1994, 11, 24) });
                    repository.Save();
                }
                else
                {
                    dbContext.Database.EnsureCreated();
                }
            }
        }
    }
}