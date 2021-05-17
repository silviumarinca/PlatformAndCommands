using System.Data;
using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using CommanderGQL.GraphQL.Platforms;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CommanderGQL
{
    public class Startup
    {

        private readonly IConfiguration _configration;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            this._configration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
          services.AddPooledDbContextFactory<AppDbContext>(opts =>
          {
              opts.UseSqlServer(_configration.GetConnectionString("CommandConStr"));
          });
          services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddType<PlatformType>()
            .AddType<CommanderGQL.GraphQL.Command.CommandType>()
            .AddMutationType<Mutation>()
            .AddSubscriptionType<Subscription>()
            
           // .AddProjections()
            .AddFiltering()
            
            .AddSorting()
            .AddInMemorySubscriptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
              endpoints.MapGraphQL();
            });
            app.UseGraphQLVoyager(new VoyagerOptions{
                    GraphQLEndPoint = "/graphql"
              },path:"/graphql-voyager");
        }
    }
}
