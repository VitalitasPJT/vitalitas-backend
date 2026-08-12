using API.Authorization;
using API.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Extensions;
using Application.Extensions;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

ValidateJwtConfiguration(builder.Configuration);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("Aluno", new OpenApiInfo
    {
        Title = "Vitalitas API - Aluno",
        Version = "v1"
    });

    options.SwaggerDoc("Administrativo", new OpenApiInfo
    {
        Title = "Vitalitas API - Administrativo",
        Version = "v1"
    });

    options.SwaggerDoc("Gestor", new OpenApiInfo
    {
        Title = "Vitalitas API - Gestor",
        Version = "v1"
    });

    options.DocInclusionPredicate((documentName, apiDescription) =>
    {
        var groupName = apiDescription.GroupName;

        if (string.IsNullOrWhiteSpace(groupName))
        {
            return true;
        }

        return string.Equals(groupName, documentName, StringComparison.OrdinalIgnoreCase);
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Use o header Authorization com o formato: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    options.SchemaFilter<PerfilRequestExampleSchemaFilter>();
    options.OperationFilter<CriarUsuarioExamplesOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RoleClaimType = "Role",
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

// Fail-safe por padrão (ADR-0012): endpoints sem [Authorize]/[AllowAnonymous]
// exigem usuário autenticado em vez de ficarem públicos por omissão.
//
// Policies nomeadas (em vez de [Authorize(Roles = "...")] soltos nos controllers):
// a matriz de permissões por funcionalidade é a fonte de verdade de quem pode
// chamar cada policy — ver docs/adr para o mapeamento completo.
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy(AuthorizationPolicies.PodeGerenciarUsuarios, policy =>
        policy.RequireRole("Gestor", "Administrador"));

    options.AddPolicy(AuthorizationPolicies.PodeGerenciarInstrutores, policy =>
        policy.RequireRole("Gestor"));

    options.AddPolicy(AuthorizationPolicies.PodeGerenciarAlunos, policy =>
        policy.RequireRole("Gestor", "Administrador"));

    options.AddPolicy(AuthorizationPolicies.PodeEditarFichaMedica, policy =>
        policy.RequireRole("Instrutor"));

    options.AddPolicy(AuthorizationPolicies.PodeTrocarSenha, policy =>
        policy.RequireRole("Aluno"));

    options.AddPolicy(AuthorizationPolicies.PodeAtualizarObjetivoAluno, policy =>
        policy.RequireRole("Aluno", "Gestor", "Administrador"));

    options.AddPolicy(AuthorizationPolicies.PodeVerLogs, policy =>
        policy.RequireRole("Gestor", "Administrador"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/Aluno/swagger.json", "Aluno");
        options.SwaggerEndpoint("/swagger/Administrativo/swagger.json", "Administrativo");
        options.SwaggerEndpoint("/swagger/Gestor/swagger.json", "Gestor");
    });

    // Seed idempotente (dados de referência, ex.: catálogo de planos) — só em
    // Development. Não substitui `dotnet ef database update`: o schema em si
    // precisa das migrations aplicadas antes (ver README/ADR-0010).
    using var seedScope = app.Services.CreateScope();
    var dbContext = seedScope.ServiceProvider.GetRequiredService<Infrastructure.Database.Context.AppDbContext>();
    await Infrastructure.Database.Seed.DbSeeder.SeedAsync(dbContext);
}

app.UseCors("AllowReact");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

static void ValidateJwtConfiguration(IConfiguration configuration)
{
    var jwtKey = configuration["Jwt:Key"];
    var jwtIssuer = configuration["Jwt:Issuer"];
    var jwtAudience = configuration["Jwt:Audience"];
    var jwtDuration = configuration["Jwt:DurationInMinutes"];

    if (string.IsNullOrWhiteSpace(jwtKey))
    {
        throw new InvalidOperationException(
            "JWT configuration is invalid: 'Jwt:Key' is missing or empty. Configure it in the API project settings or user-secrets.");
    }

    if (string.IsNullOrWhiteSpace(jwtIssuer))
    {
        throw new InvalidOperationException(
            "JWT configuration is invalid: 'Jwt:Issuer' is missing or empty.");
    }

    if (string.IsNullOrWhiteSpace(jwtAudience))
    {
        throw new InvalidOperationException(
            "JWT configuration is invalid: 'Jwt:Audience' is missing or empty.");
    }

    if (!int.TryParse(jwtDuration, out var durationInMinutes) || durationInMinutes <= 0)
    {
        throw new InvalidOperationException(
            "JWT configuration is invalid: 'Jwt:DurationInMinutes' must be a positive integer.");
    }
}

// Necessário pro WebApplicationFactory<Program> dos testes de integração
// (tests/API.IntegrationTests) enxergar essa classe de fora do assembly —
// top-level statements geram um Program implícito e internal por padrão.
public partial class Program { }
