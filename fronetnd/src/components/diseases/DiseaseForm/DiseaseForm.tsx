import React, { useState } from 'react';
import type { Disease, DiseaseCreate, DiseaseUpdate } from '../../../types/api';
import styles from './DiseaseForm.module.css';

interface DiseaseFormProps {
    disease?: Disease;
    onSubmit: (data: DiseaseCreate | DiseaseUpdate) => void;
    onCancel: () => void;
    isEditing?: boolean;
}

const DiseaseForm: React.FC<DiseaseFormProps> = ({
                                                     disease,
                                                     onSubmit,
                                                     onCancel,
                                                     isEditing = false
                                                 }) => {
    const [formData, setFormData] = useState({
        name: disease?.name || '',
        description: disease?.description || '',
        symptoms: disease?.symptoms || '',
    });

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        const submitData = isEditing
            ? formData
            : {
                name: formData.name,
                description: formData.description || undefined,
                symptoms: formData.symptoms || undefined,
            };

        onSubmit(submitData);
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    return (
        <form onSubmit={handleSubmit} className={styles.form}>
            <div className={styles.formGroup}>
                <label htmlFor="name" className={styles.label}>
                    Название болезни *
                </label>
                <input
                    type="text"
                    id="name"
                    name="name"
                    value={formData.name}
                    onChange={handleChange}
                    className={styles.input}
                    required
                />
            </div>

            <div className={styles.formGroup}>
                <label htmlFor="description" className={styles.label}>
                    Описание
                </label>
                <textarea
                    id="description"
                    name="description"
                    value={formData.description}
                    onChange={handleChange}
                    className={styles.textarea}
                    rows={3}
                />
            </div>

            <div className={styles.formGroup}>
                <label htmlFor="symptoms" className={styles.label}>
                    Симптомы
                </label>
                <textarea
                    id="symptoms"
                    name="symptoms"
                    value={formData.symptoms}
                    onChange={handleChange}
                    className={styles.textarea}
                    rows={3}
                    placeholder="List symptoms separated by commas"
                />
            </div>

            <div className={styles.formActions}>
                <button type="button" onClick={onCancel} className={styles.cancelButton}>
                    Отмена
                </button>
                <button type="submit" className={styles.submitButton}>
                    {isEditing ? 'Update' : 'Create'} Disease
                </button>
            </div>
        </form>
    );
};

export default DiseaseForm;