import React from 'react';
import type { Doctor } from '../../../types/api';
import styles from './DoctorCard.module.css';

interface DoctorCardProps {
    doctor: Doctor;
    onEdit: (doctor: Doctor) => void;
    onDelete: (id: number) => void;
    onManagePatients: (doctor: Doctor) => void;
    onViewBySpecialization: (specialization: string) => void;
}

const DoctorCard: React.FC<DoctorCardProps> = ({
                                                   doctor,
                                                   onEdit,
                                                   onDelete,
                                                   onViewBySpecialization,
                                               }) => {
    return (
        <div className={styles.card}>
            <div className={styles.cardHeader}>
                <div>
                    <h3 className={styles.name}>
                        {doctor.firstName} {doctor.lastName}
                    </h3>
                    <p className={styles.specialization}>{doctor.specialization}</p>
                </div>
                <div className={styles.actions}>
                    <button
                        onClick={() => onEdit(doctor)}
                        className={styles.editButton}
                    >
                        Изменить
                    </button>
                    <button
                        onClick={() => onDelete(doctor.id)}
                        className={styles.deleteButton}
                    >
                        Удалить
                    </button>
                </div>
            </div>

            <div className={styles.cardBody}>
                <div className={styles.details}>
                    <p><strong>Лицензия:</strong> {doctor.licenseNumber}</p>

                </div>

                <div className={styles.managementActions}>
                    <button
                        onClick={() => onViewBySpecialization(doctor.specialization)}
                        className={styles.secondaryButton}
                    >
                        Фильтровать по его специализации
                    </button>

                </div>
            </div>
        </div>
    );
};

export default DoctorCard;