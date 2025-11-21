using MedicalRecords.Application.Interfaces;
using MedicalRecords.Application.Interfaces.Services;
using MedicalRecords.Domain.Models;

namespace MedicalRecords.Application.Services;

public class PatientService(
    IRepository<Patient> patientRepository,
    IRepository<Disease> diseaseRepository,
    IRepository<Doctor> doctorRepository)
    : IPatientService
{
    public async Task<List<Patient>> GetPatientsAsync(CancellationToken cancellationToken)
    {
        return await patientRepository.GetAllAsync(cancellationToken, 
            p=>p.Doctor!, 
            p=>p.Diseases);
    }

    public async Task<Patient?> GetPatientAsync(int id, CancellationToken cancellationToken)
    {
        return await patientRepository.GetByIdAsync(id, cancellationToken, 
            p=>p.Doctor!, 
            p=>p.Diseases);
    }

    public async Task<Patient> CreatePatientAsync(Patient patient, CancellationToken cancellationToken)
    {
        await patientRepository.AddAsync(patient, cancellationToken);
        return patient;
    }

    public async Task<Patient?> UpdatePatientAsync(int id, Action<Patient> updateAction, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.GetByIdAsync(id, cancellationToken);
        if (patient is null) return null;

        updateAction(patient);
        await patientRepository.UpdateAsync(patient, cancellationToken);
        return patient;
    }

    public async Task<Patient?> DeletePatientAsync(int id, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.GetByIdAsync(id, cancellationToken);
        if (patient is null) return null;

        await patientRepository.DeleteAsync(id, cancellationToken);
        return patient;
    }

    public async Task<Patient?> AssignDoctorAsync(int patientId, int doctorId, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.GetByIdAsync(patientId, cancellationToken);
        if (patient is null) return null;

        var doctor = await doctorRepository.GetByIdAsync(doctorId, cancellationToken);
        if (doctor is null) return null;

        patient.Doctor = doctor;
        patient.DoctorId = doctorId;
        await patientRepository.UpdateAsync(patient, cancellationToken);
        return patient;
    }

    public async Task<Patient?> AddDiseaseAsync(int patientId, int diseaseId, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.GetByIdAsync(patientId, cancellationToken);
        if (patient is null) return null;

        var disease = await diseaseRepository.GetByIdAsync(diseaseId, cancellationToken);
        if (disease is null) return null;

        patient.Diseases.Add(disease);
        await patientRepository.SaveChangesAsync(cancellationToken);
        return patient;
    }

    public async Task<Patient?> RemoveDiseaseAsync(int patientId, int diseaseId, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.GetByIdAsync(patientId, cancellationToken);
        if (patient is null) return null;

        var disease = await diseaseRepository.GetByIdAsync(diseaseId, cancellationToken);
        if (disease is null) return null;

        patient.Diseases.Remove(disease);
        await patientRepository.UpdateAsync(patient, cancellationToken);
        return patient;
    }
}