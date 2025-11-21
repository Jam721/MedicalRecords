import React from 'react';
import type { Disease } from '../../../types/api';
import styles from './DiseaseCard.module.css';

interface DiseaseCardProps {
    disease: Disease;
    onEdit: (disease: Disease) => void;
    onDelete: (id: number) => void;
}

const DiseaseCard: React.FC<DiseaseCardProps> = ({
                                                     disease,
                                                     onEdit,
                                                     onDelete,
                                                 }) => {
    return (
        <div className={styles.card}>
            <div className={styles.cardHeader}>
                <h3 className={styles.name}>{disease.name}</h3>
                <div className={styles.actions}>
                    <button
                        onClick={() => onEdit(disease)}
                        className={styles.editButton}
                    >
                        Изменить
                    </button>
                    <button
                        onClick={() => onDelete(disease.id)}
                        className={styles.deleteButton}
                    >
                        Удалить
                    </button>
                </div>
            </div>

            <div className={styles.cardBody}>
                <div className={styles.details}>
                    <div className={styles.description}>
                        <strong>Описание:</strong>
                        <p>{disease.description || 'No description available'}</p>
                    </div>
                    <div className={styles.symptoms}>
                        <strong>Симптомы:</strong>
                        <p>{disease.symptoms || 'No symptoms listed'}</p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default DiseaseCard;