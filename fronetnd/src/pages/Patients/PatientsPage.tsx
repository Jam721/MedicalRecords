import React, { useState, useEffect } from 'react';
import type { Patient, PatientCreate, PatientUpdate, Doctor, Disease } from '../../types/api'; // Добавлены типы
import { patientService, doctorService, diseaseService } from '../../services/api';
import PatientCard from '../../components/patients/PatientCard/PatientCard';
import PatientForm from '../../components/patients/PatientForm/PatientForm';
import Modal from '../../components/common/Modal/Modal';
import styles from './PatientsPage.module.css';

const PatientsPage: React.FC = () => {
    const [patients, setPatients] = useState<Patient[]>([]);
    const [doctors, setDoctors] = useState<Doctor[]>([]); // Исправлен тип
    const [diseases, setDiseases] = useState<Disease[]>([]); // Исправлен тип
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
    const [isEditModalOpen, setIsEditModalOpen] = useState(false);
    const [isAssignDoctorModalOpen, setIsAssignDoctorModalOpen] = useState(false);
    const [isManageDiseasesModalOpen, setIsManageDiseasesModalOpen] = useState(false);

    const [selectedPatient, setSelectedPatient] = useState<Patient | null>(null);
    const [selectedDoctorId, setSelectedDoctorId] = useState<number | ''>('');
    const [selectedDiseaseIds, setSelectedDiseaseIds] = useState<number[]>([]);

    useEffect(() => {
        loadData();
    }, []);

    const loadData = async () => {
        try {
            setLoading(true);
            const [patientsRes, doctorsRes, diseasesRes] = await Promise.all([
                patientService.getAll(),
                doctorService.getAll(),
                diseaseService.getAll()
            ]);
            setPatients(patientsRes.data);
            setDoctors(doctorsRes.data);
            setDiseases(diseasesRes.data);
        } catch (err) {
            setError('Не удалось загрузить данные');
        } finally {
            setLoading(false);
        }
    };

    const handleCreatePatient = async (patientData: PatientCreate) => {
        try {
            await patientService.create(patientData);
            await loadData();
            setIsCreateModalOpen(false);
        } catch (err) {
            setError('Не удалось создать пациента');
        }
    };

    const handleUpdatePatient = async (patientData: PatientUpdate) => {
        if (!selectedPatient) return;

        try {
            await patientService.update(selectedPatient.id, patientData);
            await loadData();
            setIsEditModalOpen(false);
            setSelectedPatient(null);
        } catch (err) {
            setError('Не удалось обновить данные пациента');
        }
    };

    const handleDeletePatient = async (id: number) => {
        if (!window.confirm('Вы уверены, что хотите удалить этого пациента?')) return;

        try {
            await patientService.delete(id);
            await loadData();
        } catch (err) {
            setError('Не удалось удалить пациента');
        }
    };

    const handleAssignDoctor = async () => {
        if (!selectedPatient || !selectedDoctorId) return;

        try {
            await patientService.assignDoctor(selectedPatient.id, selectedDoctorId);
            await loadData();
            setIsAssignDoctorModalOpen(false);
            setSelectedPatient(null);
            setSelectedDoctorId('');
        } catch (err) {
            setError('Не удалось назначить врача');
        }
    };

    const handleManageDiseases = async () => {
        if (!selectedPatient) return;

        try {
            // Удаляем текущие заболевания
            for (const disease of selectedPatient.diseases) {
                await patientService.removeDisease(selectedPatient.id, disease.id);
            }

            // Добавляем выбранные заболевания
            for (const diseaseId of selectedDiseaseIds) {
                await patientService.addDisease(selectedPatient.id, diseaseId);
            }

            await loadData();
            setIsManageDiseasesModalOpen(false);
            setSelectedPatient(null);
            setSelectedDiseaseIds([]);
        } catch (err) {
            setError('Не удалось обновить список заболеваний');
        }
    };

    if (loading) return <div className={styles.loading}>Загрузка...</div>;
    if (error) return <div className={styles.error}>{error}</div>;

    return (
        <div className={styles.page}>
            <div className={styles.pageHeader}>
                <h1>Управление пациентами</h1>
                <button
                    onClick={() => setIsCreateModalOpen(true)}
                    className={styles.createButton}
                >
                    Добавить пациента
                </button>
            </div>

            <div className={styles.patientsGrid}>
                {patients.map(patient => (
                    <PatientCard
                        key={patient.id}
                        patient={patient}
                        onEdit={(patient) => {
                            setSelectedPatient(patient);
                            setIsEditModalOpen(true);
                        }}
                        onDelete={handleDeletePatient}
                        onAssignDoctor={(patient) => {
                            setSelectedPatient(patient);
                            setSelectedDoctorId(patient.doctor?.id || '');
                            setIsAssignDoctorModalOpen(true);
                        }}
                        onManageDiseases={(patient) => {
                            setSelectedPatient(patient);
                            setSelectedDiseaseIds(patient.diseases.map(d => d.id));
                            setIsManageDiseasesModalOpen(true);
                        }}
                    />
                ))}
            </div>

            {patients.length === 0 && (
                <div className={styles.noData}>
                    Пациенты отсутствуют. Добавьте первого пациента.
                </div>
            )}

            {/* Модальное окно создания пациента */}
            <Modal
                isOpen={isCreateModalOpen}
                onClose={() => setIsCreateModalOpen(false)}
                title="Добавить пациента"
            >
                <PatientForm
                    onSubmit={handleCreatePatient}
                    onCancel={() => setIsCreateModalOpen(false)}
                />
            </Modal>

            {/* Модальное окно редактирования пациента */}
            <Modal
                isOpen={isEditModalOpen}
                onClose={() => {
                    setIsEditModalOpen(false);
                    setSelectedPatient(null);
                }}
                title="Редактировать пациента"
            >
                {selectedPatient && (
                    <PatientForm
                        patient={selectedPatient}
                        onSubmit={handleUpdatePatient}
                        onCancel={() => {
                            setIsEditModalOpen(false);
                            setSelectedPatient(null);
                        }}
                        isEditing={true}
                    />
                )}
            </Modal>

            {/* Модальное окно назначения врача */}
            <Modal
                isOpen={isAssignDoctorModalOpen}
                onClose={() => {
                    setIsAssignDoctorModalOpen(false);
                    setSelectedPatient(null);
                    setSelectedDoctorId('');
                }}
                title="Назначить врача"
            >
                {selectedPatient && (
                    <div className={styles.modalContent}>
                        <div className={styles.formGroup}>
                            <label className={styles.label}>Выберите врача</label>
                            <select
                                value={selectedDoctorId}
                                onChange={(e) => setSelectedDoctorId(Number(e.target.value))}
                                className={styles.select}
                            >
                                <option value="">Выберите врача</option>
                                {doctors.map(doctor => (
                                    <option key={doctor.id} value={doctor.id}>
                                        {doctor.firstName} {doctor.lastName} - {doctor.specialization}
                                    </option>
                                ))}
                            </select>
                        </div>
                        <div className={styles.modalActions}>
                            <button
                                onClick={() => setIsAssignDoctorModalOpen(false)}
                                className={styles.cancelButton}
                            >
                                Отмена
                            </button>
                            <button
                                onClick={handleAssignDoctor}
                                disabled={!selectedDoctorId}
                                className={styles.submitButton}
                            >
                                Назначить врача
                            </button>
                        </div>
                    </div>
                )}
            </Modal>

            {/* Модальное окно управления заболеваниями */}
            <Modal
                isOpen={isManageDiseasesModalOpen}
                onClose={() => {
                    setIsManageDiseasesModalOpen(false);
                    setSelectedPatient(null);
                    setSelectedDiseaseIds([]);
                }}
                title="Управление заболеваниями"
            >
                {selectedPatient && (
                    <div className={styles.modalContent}>
                        <div className={styles.formGroup}>
                            <label className={styles.label}>Выберите заболевания</label>
                            <div className={styles.checkboxGroup}>
                                {diseases.map(disease => (
                                    <label key={disease.id} className={styles.checkboxLabel}>
                                        <input
                                            type="checkbox"
                                            checked={selectedDiseaseIds.includes(disease.id)}
                                            onChange={(e) => {
                                                if (e.target.checked) {
                                                    setSelectedDiseaseIds(prev => [...prev, disease.id]);
                                                } else {
                                                    setSelectedDiseaseIds(prev => prev.filter(id => id !== disease.id));
                                                }
                                            }}
                                            className={styles.checkbox}
                                        />
                                        {disease.name}
                                    </label>
                                ))}
                            </div>
                        </div>
                        <div className={styles.modalActions}>
                            <button
                                onClick={() => setIsManageDiseasesModalOpen(false)}
                                className={styles.cancelButton}
                            >
                                Отмена
                            </button>
                            <button
                                onClick={handleManageDiseases}
                                className={styles.submitButton}
                            >
                                Обновить заболевания
                            </button>
                        </div>
                    </div>
                )}
            </Modal>
        </div>
    );
};

export default PatientsPage;