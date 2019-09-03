using GraphQL.Types;
using GraphQL.With.Rest.Api.ServiceLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GraphQL.With.Rest.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                         
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        //This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<IDataRepository, DataRepository>();
            services.AddScoped<IGraphQLReaderService, GraphQLReaderService>();
            services.AddScoped<IGraphQLAuthResolver, GraphQLAuthResolver>();
            services.AddScoped<ICustomAuthService, CustomAuthService>();
            services.AddScoped<IProvideClaimsPrincipal, GraphQLUserContext>();
            services.AddScoped<TestGraphQueryResolver>();
            services.AddScoped<UserDetailGraphType>();
            services.AddScoped<AddressGraphType>();
            services.AddScoped<CreditCardInfoGraphType>();
            services.AddScoped<CreditCardTransactionGraphType>();
            services.AddScoped<GraphQLReaderService>();          

            services.AddScoped<ISchema, TestGraphSchema>();
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();           
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }   

}
