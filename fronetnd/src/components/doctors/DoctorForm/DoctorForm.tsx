import React, { useState } from 'react';
import type { Doctor, DoctorCreate, DoctorUpdate } from '../../../types/api';
import styles from './DoctorForm.module.css';

interface DoctorFormProps {
    doctor?: Doctor;
    onSubmit: (data: DoctorCreate | DoctorUpdate) => void;
    onCancel: () => void;
    isEditing?: boolean;
}

const DoctorForm: React.FC<DoctorFormProps> = ({
                                                   doctor,
                                                   onSubmit,
                                                   onCancel,
                                                   isEditing = false
                                               }) => {
    const [formData, setFormData] = useState({
        firstName: doctor?.firstName || '',
        lastName: doctor?.lastName || '',
        specialization: doctor?.specialization || '',
        licenseNumber: doctor?.licenseNumber || '',
    });

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        const submitData = isEditing
            ? formData
            : {
                firstName: formData.firstName,
                lastName: formData.lastName,
                specialization: formData.specialization,
                licenseNumber: formData.licenseNumber,
            };

        onSubmit(submitData);
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    return (
        <form onSubmit={handleSubmit} className={styles.form}>
            <div className={styles.formGroup}>
                <label htmlFor="firstName" className={styles.label}>
                    Имя *
                </label>
                <input
                    type="text"
                    id="firstName"
                    name="firstName"
                    value={formData.firstName}
                    onChange={handleChange}
                    className={styles.input}
                    required
                />
            </div>

            <div className={styles.formGroup}>
                <label htmlFor="lastName" className={styles.label}>
                    Фамилия *
                </label>
                <input
                    type="text"
                    id="lastName"
                    name="lastName"
                    value={formData.lastName}
                    onChange={handleChange}
                    className={styles.input}
                    required
                />
            </div>

            <div className={styles.formGroup}>
                <label htmlFor="specialization" className={styles.label}>
                    Специализация *
                </label>
                <input
                    type="text"
                    id="specialization"
                    name="specialization"
                    value={formData.specialization}
                    onChange={handleChange}
                    className={styles.input}
                    required
                />
            </div>

            <div className={styles.formGroup}>
                <label htmlFor="licenseNumber" className={styles.label}>
                    Номер лицензии *
                </label>
                <input
                    type="text"
                    id="licenseNumber"
                    name="licenseNumber"
                    value={formData.licenseNumber}
                    onChange={handleChange}
                    className={styles.input}
                    required
                />
            </div>

            <div className={styles.formActions}>
                <button type="button" onClick={onCancel} className={styles.cancelButton}>
                    Отмена
                </button>
                <button type="submit" className={styles.submitButton}>
                    {isEditing ? 'Update' : 'Create'} Doctor
                </button>
            </div>
        </form>
    );
};

export default DoctorForm;