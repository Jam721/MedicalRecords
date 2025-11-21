import React from 'react';
import type { Patient } from '../../../types/api';
import styles from './PatientCard.module.css';

interface PatientCardProps {
    patient: Patient;
    onEdit: (patient: Patient) => void;
    onDelete: (id: number) => void;
    onAssignDoctor: (patient: Patient) => void;
    onManageDiseases: (patient: Patient) => void;
}

const PatientCard: React.FC<PatientCardProps> = ({
                                                     patient,
                                                     onEdit,
                                                     onDelete,
                                                     onAssignDoctor,
                                                     onManageDiseases,
                                                 }) => {
    return (
        <div className={styles.card}>
            <div className={styles.cardHeader}>
                <h3 className={styles.name}>
                    {patient.firstName} {patient.lastName}
                </h3>
                <div className={styles.actions}>
                    <button
                        onClick={() => onEdit(patient)}
                        className={styles.editButton}
                    >
                        Изменить
                    </button>
                    <button
                        onClick={() => onDelete(patient.id)}
                        className={styles.deleteButton}
                    >
                        Удалить
                    </button>
                </div>
            </div>

            <div className={styles.cardBody}>
                <div className={styles.details}>
                    <p><strong>Возраст:</strong> {patient.age}</p>
                    <p><strong>Телефон:</strong> {patient.phoneNumber}</p>
                    <p><strong>Доктор:</strong> {patient.doctor ? `${patient.doctor.firstName} ${patient.doctor.lastName}` : 'Not assigned'}</p>
                    <p>
                        <strong>Болезни:</strong> {patient.diseases.length > 0
                        ? patient.diseases.map(d => d).join(', ')
                        : 'None'
                    }
                    </p>
                </div>

                <div className={styles.managementActions}>
                    <button
                        onClick={() => onAssignDoctor(patient)}
                        className={styles.secondaryButton}
                    >
                        Выбрать доктора
                    </button>
                    <button
                        onClick={() => onManageDiseases(patient)}
                        className={styles.secondaryButton}
                    >
                        Добавить болезнь
                    </button>
                </div>
            </div>
        </div>
    );
};

export default PatientCard;