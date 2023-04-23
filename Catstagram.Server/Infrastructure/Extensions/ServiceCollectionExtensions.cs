using System.Text;
using Catstagram.Server.Data;
using Catstagram.Server.Data.Models;
using Catstagram.Server.Features.Cats;
using Catstagram.Server.Features.Identity;
using Catstagram.Server.Infrastructure.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Catstagram.Server.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static AppSettings GetApplicationSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
        services.Configure<AppSettings>(applicationSettingsConfiguration);
        var appSettings = applicationSettingsConfiguration.Get<AppSettings>();
        return appSettings;
    }

    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
        => services.AddDbContext<CatstagramDbContext>(options => options
            .UseSqlServer(configuration.GetDefaultConnectionString()));

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric= false;
                options.Password.RequireUppercase= false;
            })
            .AddEntityFrameworkStores<CatstagramDbContext>();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        AppSettings appSettings)
    {
        var key = Encoding.ASCII.GetBytes(appSettings.Secret);

        services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;
    }
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
            .AddTransient<ICatService, CatService>()
            .AddTransient<IIdentityService, IdentityService>();

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    => services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc(
            "v1", 
            new OpenApiInfo
            { 
                Title = "My Catstagram API", 
                Version ="v1"
            });
    });

    public static void AddApiControllers(this IServiceCollection services)
        => services
            .AddControllers(options => options
                .Filters
                .Add<ModelOrNotFoundActionFilter>());
}
