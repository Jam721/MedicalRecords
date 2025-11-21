import React, { useState } from 'react';
import type { Patient, PatientCreate, PatientUpdate } from '../../../types/api';
import styles from './PatientForm.module.css';

interface PatientFormProps {
    patient?: Patient;
    onSubmit: (data: PatientCreate | PatientUpdate) => void;
    onCancel: () => void;
    isEditing?: boolean;
}

const PatientForm: React.FC<PatientFormProps> = ({
                                                     patient,
                                                     onSubmit,
                                                     onCancel,
                                                     isEditing = false
                                                 }) => {
    const [formData, setFormData] = useState({
        firstName: patient?.firstName || '',
        lastName: patient?.lastName || '',
        age: patient?.age || '',
        phoneNumber: patient?.phoneNumber || '',
    });

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        const submitData = isEditing
            ? { ...formData, age: formData.age ? Number(formData.age) : undefined }
            : {
                firstName: formData.firstName,
                lastName: formData.lastName,
                age: formData.age ? Number(formData.age) : undefined,
                phoneNumber: formData.phoneNumber || undefined,
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
                <label htmlFor="age" className={styles.label}>
                    Возраст
                </label>
                <input
                    type="number"
                    id="age"
                    name="age"
                    value={formData.age}
                    onChange={handleChange}
                    className={styles.input}
                    min="0"
                    max="150"
                />
            </div>

            <div className={styles.formGroup}>
                <label htmlFor="phoneNumber" className={styles.label}>
                    Номер телефона
                </label>
                <input
                    type="tel"
                    id="phoneNumber"
                    name="phoneNumber"
                    value={formData.phoneNumber}
                    onChange={handleChange}
                    className={styles.input}
                />
            </div>

            <div className={styles.formActions}>
                <button type="button" onClick={onCancel} className={styles.cancelButton}>
                    Отмена
                </button>
                <button type="submit" className={styles.submitButton}>
                    {isEditing ? 'Update' : 'Create'} Patient
                </button>
            </div>
        </form>
    );
};

export default PatientForm;