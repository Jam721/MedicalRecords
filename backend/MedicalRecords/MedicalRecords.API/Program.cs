using MedicalRecords.API.Extensions;
using MedicalRecords.Application.Interfaces;
using MedicalRecords.Application.Interfaces.Services;
using MedicalRecords.Application.Services;
using MedicalRecords.Infrastructure.Repositories;
using MedicalRecords.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var services = builder.Services;
var configuration = builder.Configuration;

services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

services.AddDbContextExtension(configuration);
services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
services.AddScoped<IPatientService, PatientService>();
services.AddScoped<IDoctorService, DoctorService>();
services.AddScoped<IDiseaseService, DiseaseService>();

services.AddOpenApi();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseCors();
app.UseDbContextExtension();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();