using System;
using Cassandra.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.DataStaxAstraMappings;
using Persistence.Interface;
using SummArts.Helpers;
using SummArts.Models;
using SummArts.Persistence;

namespace SummArts
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
            services.AddRazorPages();

            if (bool.Parse(Configuration["AstraEnabled"]))
            {
                MappingConfiguration.Global.Define<SummaryMappings>();
                services.AddSingleton<IDataStaxAstraDbConnection, DataStaxAstraDbConnection>();
                services.AddScoped<IRepository<Summary, int>, DataStaxAstraSummaryRepository>();
            }
            else
            {
                services.AddDbContext<SummArtsContext>(options => options.UseSqlite(Configuration.GetConnectionString("SummArtsContext")));
                services.AddScoped<IRepository<Summary, int>, SQLLiteSummaryRepository>();
            }

            services.AddScoped<IDateProvider, DateProvider>();
            services.AddScoped<ISummarizer, Summarizer>();
            services.AddScoped<ISentimentAnalyzer, SentimentAnalyzer>();
            services.AddSingleton<IArticleProvider, ArticleProvider>();
            services.AddSingleton<IHttpClient, HttpClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
