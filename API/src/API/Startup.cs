using API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(
                setup =>
                {
                    setup.SwaggerDoc(
                            "v1",
                            new Microsoft.OpenApi.Models.OpenApiInfo()
                            {
                                Title = "API Stefanini",
                                Version = "v1",
                                Description = "API Orders Management",
                                Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                                {
                                    Email = "contact@stefanini.com",
                                    Name = "Stefanini Order Management"
                                },
                                License = new Microsoft.OpenApi.Models.OpenApiLicense()
                                {
                                    Name = "MIT"
                                }
                            }
                        );
                }
            );
        services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("Database"));
        services.AddScoped<Context, Context>();

        services.AddCors(c =>
        {
            c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

        services.AddControllersWithViews().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .AddNewtonsoftJson(opt => {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger(x => x.SerializeAsV2 = true);
        app.UseSwaggerUI(
                setupAction =>
                {
                    setupAction.SwaggerEndpoint("/swagger/v1/swagger.json", "Api V1");
                    setupAction.RoutePrefix = String.Empty;
                }
            );
        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}