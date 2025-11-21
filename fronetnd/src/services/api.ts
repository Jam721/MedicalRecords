import axios from 'axios';
import type {
    Patient,
    PatientCreate,
    PatientUpdate,
    Doctor,
    DoctorCreate,
    DoctorUpdate,
    Disease,
    DiseaseCreate,
    DiseaseUpdate
} from '../types/api';

const API_BASE_URL = 'http://localhost:5290/api';

const api = axios.create({
    baseURL: API_BASE_URL,
});

export const patientService = {
    getAll: () => api.get<Patient[]>('/patients'),
    getById: (id: number) => api.get<Patient>(`/patients/${id}`),
    create: (patient: PatientCreate) => api.post<Patient>('/patients', patient),
    update: (id: number, patient: PatientUpdate) => api.put<Patient>(`/patients/${id}`, patient),
    delete: (id: number) => api.delete(`/patients/${id}`),
    assignDoctor: (patientId: number, doctorId: number) =>
        api.put<Patient>(`/patients/${patientId}/doctor/${doctorId}`),
    addDisease: (patientId: number, diseaseId: number) =>
        api.put<Patient>(`/patients/${patientId}/diseases/${diseaseId}`),
    removeDisease: (patientId: number, diseaseId: number) =>
        api.delete<Patient>(`/patients/${patientId}/diseases/${diseaseId}`),
};

export const doctorService = {
    getAll: () => api.get<Doctor[]>('/doctors'),
    getById: (id: number) => api.get<Doctor>(`/doctors/${id}`),
    getBySpecialization: (specialization: string) =>
        api.get<Doctor[]>(`/doctors/specialization/${specialization}`),
    create: (doctor: DoctorCreate) => api.post<Doctor>('/doctors', doctor),
    update: (id: number, doctor: DoctorUpdate) => api.put<Doctor>(`/doctors/${id}`, doctor),
    delete: (id: number) => api.delete(`/doctors/${id}`),
    addPatient: (doctorId: number, patientId: number) =>
        api.put<Doctor>(`/doctors/${doctorId}/patients/${patientId}`),
    removePatient: (doctorId: number, patientId: number) =>
        api.delete<Doctor>(`/doctors/${doctorId}/patients/${patientId}`),
};

export const diseaseService = {
    getAll: () => api.get<Disease[]>('/diseases'),
    getById: (id: number) => api.get<Disease>(`/diseases/${id}`),
    create: (disease: DiseaseCreate) => api.post<Disease>('/diseases', disease),
    update: (id: number, disease: DiseaseUpdate) => api.put<Disease>(`/diseases/${id}`, disease),
    delete: (id: number) => api.delete(`/diseases/${id}`),
};