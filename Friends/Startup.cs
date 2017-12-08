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

            InitializeDatabase(app);
            app.UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<FriendDbContext>();
            services.AddScoped<IFriendRepository, FriendRepository>();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<FriendDbContext>();
                dbContext.Database.EnsureCreated();

                var repository = serviceScope.ServiceProvider.GetRequiredService<IFriendRepository>();

                // seed data
                repository.AddFriend(new Models.Friend { Name = "Noah", Email = "xxx.com", BirthDate = new System.DateTime(1990, 10, 10) });
                repository.Save();
            }
        }
    }
}