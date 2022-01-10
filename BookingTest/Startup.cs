using AutoMapper;
using BookingTest.BLL.Automapper;
using BookingTest.BLL.Data;
using BookingTest.BLL.Helper;
using BookingTest.BLL.Services;
using BookingTest.DatabaseSeed;
using BookingTest.DLL.Database;
using BookingTest.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookingTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDataContext>(options =>
            {
                if (Configuration.GetValue<bool>("Logging:DbSensitiveLogging"))
                {
                    options.EnableSensitiveDataLogging();
                }
                if (Configuration.GetValue<bool>("Debug:InMemoryDatabase"))
                {
                    options.UseInMemoryDatabase();
                }
                else
                {
                    options.UseMySql(
                        Configuration.GetConnectionString("MainDb"),
                        mySqlOptions =>
                        {
                            mySqlOptions.ServerVersion(new Version(5, 5, 62), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql);
                        }
                    );
                }
            });
            services.AddCors(options =>
           {
               options.AddPolicy("CORSPolicy", builder =>
               {
                   builder.AllowAnyOrigin();
                   builder.AllowAnyHeader();
                   builder.AllowAnyMethod();
                   builder.AllowCredentials();
                   builder.WithOrigins("http://localhost:8080", "http://localhost:80");
               });
           });
            services.AddMvcCore()
                            .AddAuthorization()
                            .AddJsonFormatters(x => x.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                            .AddApiExplorer();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration.GetSection("JwtAuthentication")["JwtIssuer"],
                    ValidAudience = Configuration.GetSection("JwtAuthentication")["JwtAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JwtAuthentication")["JwtKey"])),
                    ClockSkew = TimeSpan.Zero,
                };
                config.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            ConfigureSwagger(services);
            ConfigureSelfServices(services);
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });
            ConvertersInit.Init();
            services.Configure<JWTAuthentication>(Configuration.GetSection("JwtAuthentication"));
            services.AddScoped(sp => sp.GetService<IOptionsSnapshot<JWTAuthentication>>().Value);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                                                    IConfiguration configuration,
                                                    ApplicationDataContext dataContext, IUserService userService, IRoomService roomService)
        {
            dataContext.Database.Migrate();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "BookingTest API V1");
            });

            app.UseCors("CORSPolicy");

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();


            app.UseMvc();
            Seeder.SeedAsync(roomService, userService).GetAwaiter().GetResult();

        }

        private void ConfigureSelfServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IScheduledRoomService, ScheduledRoomService>();
            services.AddTransient<IImageService, ImageService>();


            services.AddTransient<IJWTHelper, JWTHelper>();
        }
        public void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info()
                {
                    Version = "v1",
                    Title = "BookingTest API",
                    Description = "BookingTest server API.",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact()
                    {
                        Name = "VRealSoft",
                        Email = string.Empty,
                        Url = ""
                    },
                    License = new License()
                    {
                        Name = "Use under License",
                        Url = ""
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme()
                    {
                        In = "header",
                        Description = "Please enter JWT with Bearer into field",
                        Name = "Authorization",
                        Type = "apiKey"
                    });

                options.AddSecurityRequirement(
                    new Dictionary<string, IEnumerable<string>>()
                    {
                        { "Bearer", Enumerable.Empty<string>() },
                    });

            });
        }
    }
}
