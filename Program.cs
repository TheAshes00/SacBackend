using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SacBackend.Context;
using SacBackend.MongoConfig;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var strspecifiedOrigins = "_specifiedOrigins";
var strConnectionString = builder.Configuration.GetConnectionString("WebApiDatabase");

//                                                          // Get Configuration from appsettings 
var jwtSettings = builder.Configuration.GetSection("JWTSettings");
var key = jwtSettings["Key"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//                                                          // Add JWT configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    // Aquí puedes agregar una configuración de seguridad para Swagger (opcional)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese 'Bearer' [espacio] y luego el token JWT en el campo de texto a continuación.\n\nEjemplo: \"Bearer abcdef12345\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
    c.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddCors(options =>
    options.AddPolicy(strspecifiedOrigins,
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod();
            //policy.AllowAnyOrigin();
            policy.WithOrigins("https://nice-mud-08163d41e.5.azurestaticapps.net");
        })
);

//                                                          // Mongo DB config
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<MongoDbService>();

//                                                          // Context config
builder.Services.AddDbContext<CaafiContext>(options =>
{
    options.UseMySql(strConnectionString, ServerVersion.AutoDetect(strConnectionString));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(strspecifiedOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
