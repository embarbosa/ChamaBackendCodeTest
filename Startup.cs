using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chama.WebApi.Controllers;
using Chama.WebApi.DTOs;
using Chama.WebApi.Mapping;
using Chama.WebApi.Repositories;
using Chama.WebApi.ServiceBus;
using Chama.WebApi.WorkerProcess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Chama.WebApi
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
            services.AddMvc();
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, ChamaWorkerProcessService>();
            services.AddDbContext<ChamaContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add our Config object so it can be injecteds
            services.AddTransient<ChamaContext>();
            services.AddTransient<IChamaQueueSender<SignUpDTO>, ChamaQueueSender<SignUpDTO>>();
            services.AddTransient<ICoursesRepository, CoursesRepository>();
            services.AddTransient<IUsersRepository, UsersRepositoriy>();
            services.AddTransient<UsersRepositoriy>();
            services.AddTransient<ICoursesController, CoursesController>();

            services.AddOptions();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
