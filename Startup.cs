using GraphQLDemoBase.Authorization;
using GraphQLDemoBase.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace GraphQLDemoBase
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            string connectionString = Configuration.GetConnectionString("DBConnection");
            //services.AddHttpClient<ClientApiTest.Services.EnrollmentClient>();
            services.AddPooledDbContextFactory<AppDBContext>(options => options.UseSqlServer(connectionString));

            //https://stackoverflow.com/questions/66955768/using-addpooleddbcontextfactory-with-aspnetcore-identity
            services.AddScoped(p => p.GetRequiredService<IDbContextFactory<AppDBContext>>().CreateDbContext());


            services.AddControllers();
            //services.AddControllers(options =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();

            //    options.Filters.Add(new AuthorizeFilter(policy));
            //});



            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GraphQLDemoBase", Version = "v1" });
            });
            services.AddGraphQLServer()
                .AddAuthorization()
                .AddProjections()
                .AddFiltering()
                .AddQueryType<Query>()
                .AddTypeExtension<StudentQueries>();
            var section = Configuration.GetSection(nameof(TokenInfo));
            var tokenConfig = section.Get<TokenInfo>();
            services.AddSingleton(tokenConfig);


            //adding authentication
            var signingKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(tokenConfig.JWTSecretKey));

            services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters =
                    new TokenValidationParameters {
                        ValidIssuer = tokenConfig.JWTIssuer,
                        ValidAudience = tokenConfig.JWTIssuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey
                    };
            });

            services.AddScoped<ITokenManager, TokenManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GraphQLDemoBase v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();

                endpoints.MapGraphQL();
                endpoints.MapGraphQLGraphiQL();
                endpoints.MapGraphQLVoyager();
            });
        }
    }
}
