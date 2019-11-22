using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorksOData.Database;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Swashbuckle.AspNetCore.Swagger;

namespace AdventureWorksOData
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
            services.AddMvc(builder => builder.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddOData();

            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/blabla"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/blabla"));
                }
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddDbContext<AdventureWorksContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc(builder =>
            {
                builder.MapODataServiceRoute("odata", "odata", GetEdmModel(app.ApplicationServices));
                builder.EnableDependencyInjection();

                // Global OData options
                builder.Filter().Count().Expand().OrderBy().Select();
            });
        }

        private static IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder(serviceProvider);
            ConfigureEntities(builder);
            return builder.GetEdmModel();
        }

        private static void ConfigureEntities(ODataModelBuilder builder)
        {
            builder.EntitySet<Address>("Address");
            builder.EntitySet<AddressType>("AddressType");
            builder.EntitySet<BusinessEntity>("BusinessEntity");
            builder.EntitySet<BusinessEntityAddress>("BusinessEntityAddress");
            builder.EntitySet<BusinessEntityContact>("BusinessEntityContact");
            builder.EntitySet<ContactType>("ContactType");
            builder.EntitySet<CountryRegion>("CountryRegion");
            builder.EntitySet<EmailAddress>("EmailAddress");
            builder.EntitySet<Password>("Password");
            builder.EntitySet<Person>("Person");
            builder.EntitySet<PersonPhone>("PersonPhone");
            builder.EntitySet<PhoneNumberType>("PhoneNumberType");
            builder.EntitySet<StateProvince>("StateProvince");
        }
    }
}
