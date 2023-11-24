using EcoMonitor;
using EcoMonitor.Calculator;
using EcoMonitor.Data;
using EcoMonitor.Repository;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(option => {
    option.UseMySQL(builder.Configuration.GetConnectionString("MySQL"));
});

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddScoped<IEnvFactorRepository,EnvFactorRepository>();
builder.Services.AddScoped<IPassportRepository, PassportRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IRfcFactorRepository, RfcFactorRepository>();
builder.Services.AddScoped<CarcinogenicRiskCalculator, CarcinogenicRiskCalculator>();
builder.Services.AddAutoMapper(typeof(MapperConfig));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
