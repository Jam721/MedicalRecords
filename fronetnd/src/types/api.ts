export interface Patient {
    id: number;
    firstName: string;
    lastName: string;
    age: number;
    phoneNumber: string;
    doctor?: Doctor | null;
    diseases: Disease[];
}

export interface PatientCreate {
    firstName: string;
    lastName: string;
    age?: number;
    phoneNumber?: string;
}

export interface PatientUpdate {
    firstName?: string;
    lastName?: string;
    age?: number;
    phoneNumber?: string;
}

export interface Doctor {
    id: number;
    firstName: string;
    lastName: string;
    specialization: string;
    licenseNumber: string;
    patients: Patient[];
}

export interface DoctorCreate {
    firstName: string;
    lastName: string;
    specialization: string;
    licenseNumber: string;
}

export interface DoctorUpdate {
    firstName?: string;
    lastName?: string;
    specialization?: string;
    licenseNumber?: string;
}

export interface Disease {
    id: number;
    name: string;
    description: string;
    symptoms: string;
}

export interface DiseaseCreate {
    name: string;
    description?: string;
    symptoms?: string;
}

export interface DiseaseUpdate {
    name?: string;
    description?: string;
    symptoms?: string;
}