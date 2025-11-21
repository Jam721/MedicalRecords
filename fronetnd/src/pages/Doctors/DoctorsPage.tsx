import React, { useState, useEffect } from 'react';
import type { Doctor, DoctorCreate, DoctorUpdate, Patient } from '../../types/api'; // Добавлен тип Patient
import { doctorService, patientService } from '../../services/api';
import DoctorCard from '../../components/doctors/DoctorCard/DoctorCard';
import DoctorForm from '../../components/doctors/DoctorForm/DoctorForm';
import Modal from '../../components/common/Modal/Modal';
import styles from './DoctorsPage.module.css';

const DoctorsPage: React.FC = () => {
    const [doctors, setDoctors] = useState<Doctor[]>([]);
    const [filteredDoctors, setFilteredDoctors] = useState<Doctor[]>([]);
    const [patients, setPatients] = useState<Patient[]>([]); // Исправлен тип
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
    const [isEditModalOpen, setIsEditModalOpen] = useState(false);
    const [isManagePatientsModalOpen, setIsManagePatientsModalOpen] = useState(false);
    const [isSpecializationFilterModalOpen, setIsSpecializationFilterModalOpen] = useState(false);

    const [selectedDoctor, setSelectedDoctor] = useState<Doctor | null>(null);
    const [selectedPatientIds, setSelectedPatientIds] = useState<number[]>([]);
    const [specializationFilter, setSpecializationFilter] = useState('');
    const [isFiltered, setIsFiltered] = useState(false);

    useEffect(() => {
        loadData();
    }, []);

    const loadData = async () => {
        try {
            setLoading(true);
            const [doctorsRes, patientsRes] = await Promise.all([
                doctorService.getAll(),
                patientService.getAll()
            ]);
            setDoctors(doctorsRes.data);
            setFilteredDoctors(doctorsRes.data);
            setPatients(patientsRes.data);
        } catch (err) {
            setError('Не удалось загрузить данные');
        } finally {
            setLoading(false);
        }
    };

    const handleCreateDoctor = async (doctorData: DoctorCreate) => {
        try {
            await doctorService.create(doctorData);
            await loadData();
            setIsCreateModalOpen(false);
        } catch (err) {
            setError('Не удалось создать врача');
        }
    };

    const handleUpdateDoctor = async (doctorData: DoctorUpdate) => {
        if (!selectedDoctor) return;

        try {
            await doctorService.update(selectedDoctor.id, doctorData);
            await loadData();
            setIsEditModalOpen(false);
            setSelectedDoctor(null);
        } catch (err) {
            setError('Не удалось обновить данные врача');
        }
    };

    const handleDeleteDoctor = async (id: number) => {
        if (!window.confirm('Вы уверены, что хотите удалить этого врача?')) return;

        try {
            await doctorService.delete(id);
            await loadData();
        } catch (err) {
            setError('Не удалось удалить врача');
        }
    };

    const handleManagePatients = async () => {
        if (!selectedDoctor) return;

        try {
            // Удаляем текущих пациентов
            for (const patient of selectedDoctor.patients) {
                await doctorService.removePatient(selectedDoctor.id, patient.id);
            }

            // Добавляем выбранных пациентов
            for (const patientId of selectedPatientIds) {
                await doctorService.addPatient(selectedDoctor.id, patientId);
            }

            await loadData();
            setIsManagePatientsModalOpen(false);
            setSelectedDoctor(null);
            setSelectedPatientIds([]);
        } catch (err) {
            setError('Не удалось обновить список пациентов');
        }
    };

    const handleViewBySpecialization = async (specialization: string) => {
        setSpecializationFilter(specialization);
        setIsSpecializationFilterModalOpen(true);
    };

    const handleFilterBySpecialization = async () => {
        if (!specializationFilter.trim()) {
            setFilteredDoctors(doctors);
            setIsFiltered(false);
            setIsSpecializationFilterModalOpen(false);
            return;
        }

        try {
            const response = await doctorService.getBySpecialization(specializationFilter);
            setFilteredDoctors(response.data);
            setIsFiltered(true);
            setIsSpecializationFilterModalOpen(false);
        } catch (err) {
            setError('Не удалось отфильтровать врачей по специализации');
        }
    };

    const clearFilter = () => {
        setFilteredDoctors(doctors);
        setIsFiltered(false);
        setSpecializationFilter('');
    };

    if (loading) return <div className={styles.loading}>Загрузка...</div>;
    if (error) return <div className={styles.error}>{error}</div>;

    return (
        <div className={styles.page}>
            <div className={styles.pageHeader}>
                <div>
                    <h1>Управление врачами</h1>
                    {isFiltered && (
                        <div className={styles.filterInfo}>
                            <span>Показаны врачи со специализацией: "{specializationFilter}"</span>
                            <button onClick={clearFilter} className={styles.clearFilterButton}>
                                Сбросить фильтр
                            </button>
                        </div>
                    )}
                </div>
                <div className={styles.headerActions}>
                    <button
                        onClick={() => setIsSpecializationFilterModalOpen(true)}
                        className={styles.filterButton}
                    >
                        Фильтр по специализации
                    </button>
                    <button
                        onClick={() => setIsCreateModalOpen(true)}
                        className={styles.createButton}
                    >
                        Добавить врача
                    </button>
                </div>
            </div>

            <div className={styles.doctorsGrid}>
                {filteredDoctors.map(doctor => (
                    <DoctorCard
                        key={doctor.id}
                        doctor={doctor}
                        onEdit={(doctor) => {
                            setSelectedDoctor(doctor);
                            setIsEditModalOpen(true);
                        }}
                        onDelete={handleDeleteDoctor}
                        onManagePatients={(doctor) => {
                            setSelectedDoctor(doctor);
                            setSelectedPatientIds(doctor.patients.map(p => p.id));
                            setIsManagePatientsModalOpen(true);
                        }}
                        onViewBySpecialization={handleViewBySpecialization}
                    />
                ))}
            </div>

            {filteredDoctors.length === 0 && (
                <div className={styles.noData}>
                    {isFiltered ? 'Врачи с такой специализацией не найдены' : 'Врачи отсутствуют'}
                </div>
            )}

            {/* Модальное окно создания врача */}
            <Modal
                isOpen={isCreateModalOpen}
                onClose={() => setIsCreateModalOpen(false)}
                title="Добавить врача"
            >
                <DoctorForm
                    onSubmit={handleCreateDoctor}
                    onCancel={() => setIsCreateModalOpen(false)}
                />
            </Modal>

            {/* Модальное окно редактирования врача */}
            <Modal
                isOpen={isEditModalOpen}
                onClose={() => {
                    setIsEditModalOpen(false);
                    setSelectedDoctor(null);
                }}
                title="Редактировать врача"
            >
                {selectedDoctor && (
                    <DoctorForm
                        doctor={selectedDoctor}
                        onSubmit={handleUpdateDoctor}
                        onCancel={() => {
                            setIsEditModalOpen(false);
                            setSelectedDoctor(null);
                        }}
                        isEditing={true}
                    />
                )}
            </Modal>

            {/* Модальное окно управления пациентами */}
            <Modal
                isOpen={isManagePatientsModalOpen}
                onClose={() => {
                    setIsManagePatientsModalOpen(false);
                    setSelectedDoctor(null);
                    setSelectedPatientIds([]);
                }}
                title="Управление пациентами врача"
            >
                {selectedDoctor && (
                    <div className={styles.modalContent}>
                        <p>Управление пациентами для д-ра {selectedDoctor.firstName} {selectedDoctor.lastName}</p>
                        <div className={styles.formGroup}>
                            <label className={styles.label}>Выберите пациентов</label>
                            <div className={styles.checkboxGroup}>
                                {patients.map(patient => (
                                    <label key={patient.id} className={styles.checkboxLabel}>
                                        <input
                                            type="checkbox"
                                            checked={selectedPatientIds.includes(patient.id)}
                                            onChange={(e) => {
                                                if (e.target.checked) {
                                                    setSelectedPatientIds(prev => [...prev, patient.id]);
                                                } else {
                                                    setSelectedPatientIds(prev => prev.filter(id => id !== patient.id));
                                                }
                                            }}
                                            className={styles.checkbox}
                                        />
                                        {patient.firstName} {patient.lastName} (Возраст: {patient.age})
                                    </label>
                                ))}
                            </div>
                        </div>
                        <div className={styles.modalActions}>
                            <button
                                onClick={() => setIsManagePatientsModalOpen(false)}
                                className={styles.cancelButton}
                            >
                                Отмена
                            </button>
                            <button
                                onClick={handleManagePatients}
                                className={styles.submitButton}
                            >
                                Обновить пациентов
                            </button>
                        </div>
                    </div>
                )}
            </Modal>

            {/* Модальное окно фильтра по специализации */}
            <Modal
                isOpen={isSpecializationFilterModalOpen}
                onClose={() => {
                    setIsSpecializationFilterModalOpen(false);
                    setSpecializationFilter('');
                }}
                title="Фильтр по специализации"
            >
                <div className={styles.modalContent}>
                    <div className={styles.formGroup}>
                        <label className={styles.label}>Специализация</label>
                        <input
                            type="text"
                            value={specializationFilter}
                            onChange={(e) => setSpecializationFilter(e.target.value)}
                            className={styles.input}
                            placeholder="Введите специализацию..."
                        />
                    </div>
                    <div className={styles.modalActions}>
                        <button
                            onClick={() => setIsSpecializationFilterModalOpen(false)}
                            className={styles.cancelButton}
                        >
                            Отмена
                        </button>
                        <button
                            onClick={handleFilterBySpecialization}
                            className={styles.submitButton}
                        >
                            Применить фильтр
                        </button>
                    </div>
                </div>
            </Modal>
        </div>
    );
};

export default DoctorsPage;