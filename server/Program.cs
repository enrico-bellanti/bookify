using System.Security.Claims;
using System.Text.Json;
using Bookify.Data;
using Bookify.Repositories;
using Bookify.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using static Bookify.Repositories.IRepository;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

var builder = WebApplication.CreateBuilder(args);

// Configurazione DbContext
builder.Services.AddDbContext<BookifyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurazione Keycloak JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var baseUrl = builder.Configuration["Keycloak:BaseUrl"];
    var realm = builder.Configuration["Keycloak:Realm"];
    var authority = $"{baseUrl}/realms/{realm}";
    options.Authority = authority; // es: "https://your-keycloak-server/realms/your-realm"
    options.Audience = builder.Configuration["Keycloak:ClientId"]; // client-id configurato in Keycloak
    options.RequireHttpsMetadata = builder.Environment.IsProduction(); // false in ambiente dev
    options.SaveToken = true;

    // Opzionale: configurazione per validazione del token
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidAudiences = new[] { builder.Configuration["Keycloak:ClientId"], "account" },
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };

    // Add event handling for role mapping
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            // Get the ClaimsIdentity
            var identity = context.Principal.Identity as ClaimsIdentity;
            if (identity == null) return Task.CompletedTask;

            // Extract resource_access claims which contain client roles
            var resourceAccess = context.Principal.FindFirst("resource_access")?.Value;
            if (!string.IsNullOrEmpty(resourceAccess))
            {
                try
                {
                    // Parse the JSON
                    var resourceAccessJson = JsonDocument.Parse(resourceAccess);

                    // Get the client roles - use your client ID as configured in Keycloak
                    var clientId = builder.Configuration["Keycloak:ClientId"]; // "bookify-back-end"
                    if (resourceAccessJson.RootElement.TryGetProperty(clientId, out var clientAccess) &&
                        clientAccess.TryGetProperty("roles", out var clientRoles))
                    {
                        // Add each role as a role claim
                        foreach (var role in clientRoles.EnumerateArray())
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, role.GetString()));
                        }
                    }
                }
                catch (JsonException)
                {
                    // Log or handle parsing errors
                }
            }

            // Also add realm roles if needed
            var realmAccess = context.Principal.FindFirst("realm_access")?.Value;
            if (!string.IsNullOrEmpty(realmAccess))
            {
                try
                {
                    var realmAccessJson = JsonDocument.Parse(realmAccess);
                    if (realmAccessJson.RootElement.TryGetProperty("roles", out var realmRoles))
                    {
                        foreach (var role in realmRoles.EnumerateArray())
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, role.GetString()));
                        }
                    }
                }
                catch (JsonException)
                {
                    // Log or handle parsing errors
                }
            }

            return Task.CompletedTask;
        }
    };


});


// Configurazione Swagger con supporto JWT
builder.Services.AddSwaggerGen(options =>
{
    var baseUrl = builder.Configuration["Keycloak:BaseUrl"];
    var realm = builder.Configuration["Keycloak:Realm"];
    var authority = $"{baseUrl}/realms/{realm}";

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bookify API",
        Version = "v1",
        Description = "API del back-end di Bookify"
    });

    // Definisci lo schema di sicurezza JWT
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            // Per Direct Access Grants (Resource Owner Password Flow)
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri($"{authority}/protocol/openid-connect/token"),
                AuthorizationUrl = new Uri($"{authority}/protocol/openid-connect/auth"),
                Scopes = new Dictionary<string, string>
                {
                    { "openid", "OpenID Connect scope" },
                    { "profile", "Profile information" },
                    { "email", "Email information" }
                }
            },

            // Per il flusso Authorization Code (pi� sicuro)
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"{authority}/protocol/openid-connect/auth"),
                TokenUrl = new Uri($"{authority}/protocol/openid-connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "openid", "OpenID Connect scope" },
                    { "profile", "Profile information" },
                    { "email", "Email information" }
                }
            }
        }
    });

    // Applica il requisito di sicurezza globalmente
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new[] { "openid", "profile", "email" }
        }
    });

    // Opzionale: aggiungi supporto per includere esempi di richieste (requires Swashbuckle.AspNetCore.Filters)
    //options.ExampleFilters();

    // Opzionale: aggiungi supporto per includere commenti XML dalla documentazione del codice
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (System.IO.File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// Add services to the container.
builder.Services.AddScoped<IKeycloakUserService, KeycloakUserService>();
builder.Services.AddScoped<IAccommodationService, AccommodationService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IUserService, UserService>();
//repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IAccommodationRepository, AccommodationRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

//Cloudinary cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
builder.Services.AddSingleton<Cloudinary>(sp => {
    Cloudinary cloudinary = new Cloudinary(builder.Configuration["Cloudinary:BaseUrl"]);
    cloudinary.Api.Secure = true;
    return cloudinary;
});

var app = builder.Build();

// Apply migrations at startup
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<BookifyDbContext>();
        dbContext.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

        // Configurazione OAuth per Swagger UI
        options.OAuthClientId(builder.Configuration["Keycloak:ClientId"]); // client ID configurato in Keycloak
        options.OAuthClientSecret(builder.Configuration["Keycloak:Secret"]); // se il tuo client � configurato come confidential
        options.OAuthUsePkce(); // Consigliato per maggiore sicurezza con Authorization Code Flow
        options.OAuthScopeSeparator(" ");

        // Utile per il debug
        options.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
