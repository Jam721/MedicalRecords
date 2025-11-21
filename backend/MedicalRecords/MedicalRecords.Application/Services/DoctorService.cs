using MedicalRecords.Application.Interfaces;
using MedicalRecords.Application.Interfaces.Services;
using MedicalRecords.Domain.Models;

namespace MedicalRecords.Application.Services;

public class DoctorService(
    IRepository<Doctor> doctorRepository,
    IRepository<Patient> patientRepository)
    : IDoctorService
{
    public async Task<List<Doctor>> GetAllDoctorsAsync(CancellationToken cancellationToken)
    {
        return await doctorRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Doctor?> GetDoctorByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await doctorRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<List<Doctor>> GetDoctorsBySpecializationAsync(string specialization, CancellationToken cancellationToken)
    {
        var doctors = await doctorRepository.GetAllAsync(cancellationToken);
        return doctors
            .Where(d => d.Specialization.Contains(specialization, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public async Task<Doctor> CreateDoctorAsync(Doctor doctor, CancellationToken cancellationToken)
    {
        await doctorRepository.AddAsync(doctor, cancellationToken);
        return doctor;
    }

    public async Task<Doctor?> UpdateDoctorAsync(int id, Action<Doctor> updateAction, CancellationToken cancellationToken)
    {
        var doctor = await doctorRepository.GetByIdAsync(id, cancellationToken);
        if (doctor is null) return null;

        updateAction(doctor);
        await doctorRepository.UpdateAsync(doctor, cancellationToken);
        return doctor;
    }

    public async Task<Doctor?> DeleteDoctorAsync(int id, CancellationToken cancellationToken)
    {
        var doctor = await doctorRepository.GetByIdAsync(id, cancellationToken);
        if (doctor is null) return null;

        await doctorRepository.DeleteAsync(id, cancellationToken);
        return doctor;
    }

    public async Task<Doctor?> AddPatientToDoctorAsync(int doctorId, int patientId, CancellationToken cancellationToken)
    {
        var doctor = await doctorRepository.GetByIdAsync(doctorId, cancellationToken);
        if (doctor is null) return null;

        var patient = await patientRepository.GetByIdAsync(patientId, cancellationToken);
        if (patient is null) return null;

        doctor.Patients.Add(patient);
        await doctorRepository.UpdateAsync(doctor, cancellationToken);
        return doctor;
    }

    public async Task<Doctor?> RemovePatientFromDoctorAsync(int doctorId, int patientId, CancellationToken cancellationToken)
    {
        var doctor = await doctorRepository.GetByIdAsync(doctorId, cancellationToken);
        if (doctor is null) return null;

        var patient = await patientRepository.GetByIdAsync(patientId, cancellationToken);
        if (patient is null) return null;

        doctor.Patients.Remove(patient);
        await doctorRepository.UpdateAsync(doctor, cancellationToken);
        return doctor;
    }
}