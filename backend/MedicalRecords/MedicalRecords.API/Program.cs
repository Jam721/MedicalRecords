using MedicalRecords.API.Extendions;
using MedicalRecords.Application.Interfaces;
using MedicalRecords.Application.Interfaces.Services;
using MedicalRecords.Application.Services;
using MedicalRecords.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

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
