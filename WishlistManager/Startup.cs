using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WishlistManager.Data;
using WishlistManager.Models;

namespace WishlistManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            WishlistDbContext _context = new WishlistDbContext();
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            new WishlistDataInitializer(_context).InitializeData();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList")); //Testcontext for experimenting
            services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("Users"));
            services.AddDbContext<WishlistContext>(opt => opt.UseInMemoryDatabase("Wishlists"));
            services.AddDbContext<WishContext>(opt => opt.UseInMemoryDatabase("Wishes"));
            services.AddDbContext<MessageContext>(opt => opt.UseInMemoryDatabase("Messages"));
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }); ;
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
