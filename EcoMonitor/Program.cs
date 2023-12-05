using EcoMonitor;
using EcoMonitor.Calculator;
using EcoMonitor.Data;
using EcoMonitor.Repository;
using EcoMonitor.Repository.IRepository;
using EcoMonitor.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(option => {
    option.UseMySQL(builder.Configuration.GetConnectionString("GoogleMySQL"));
    //option.UseMySQL(builder.Configuration.GetConnectionString("GoogleMySQLPublic"));
    //option.UseMySQL(builder.Configuration.GetConnectionString("MySQL"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    //options.Password.RequireDigit = true;
    //options.Password.RequireLowercase = true;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

//[AUTH CODE BLOCK]

//builder.Services.AddAuthentication(auth =>
//    {
//        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    }
//).AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidAudience = builder.Configuration["AuthSettings:Audience"],
//            ValidIssuer = builder.Configuration["AuthSettings:Issuer"],
//            RequireExpirationTime = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"])),
//            ValidateIssuerSigningKey = true
//        };
//    }
//);

builder.Services.AddScoped<IEnvFactorRepository,EnvFactorRepository>();
builder.Services.AddScoped<IPassportRepository, PassportRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IRfcFactorRepository, RfcFactorRepository>();
builder.Services.AddScoped<CarcinogenicRiskCalculator, CarcinogenicRiskCalculator>();
builder.Services.AddScoped<NonCarcinogenicRiskCalculator, NonCarcinogenicRiskCalculator>();
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
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
            new List<string>()
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
