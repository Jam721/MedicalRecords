import React, { useState, useEffect } from 'react';
import type { Disease, DiseaseCreate, DiseaseUpdate } from '../../types/api';
import { diseaseService } from '../../services/api';
import DiseaseCard from '../../components/diseases/DiseaseCard/DiseaseCard';
import DiseaseForm from '../../components/diseases/DiseaseForm/DiseaseForm';
import Modal from '../../components/common/Modal/Modal';
import styles from './DiseasesPage.module.css';

const DiseasesPage: React.FC = () => {
    const [diseases, setDiseases] = useState<Disease[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
    const [isEditModalOpen, setIsEditModalOpen] = useState(false);

    const [selectedDisease, setSelectedDisease] = useState<Disease | null>(null);

    useEffect(() => {
        loadData();
    }, []);

    const loadData = async () => {
        try {
            setLoading(true);
            const response = await diseaseService.getAll();
            setDiseases(response.data);
        } catch (err) {
            setError('Не удалось загрузить заболевания');
        } finally {
            setLoading(false);
        }
    };

    const handleCreateDisease = async (diseaseData: DiseaseCreate) => {
        try {
            await diseaseService.create(diseaseData);
            await loadData();
            setIsCreateModalOpen(false);
        } catch (err) {
            setError('Не удалось создать заболевание');
        }
    };

    const handleUpdateDisease = async (diseaseData: DiseaseUpdate) => {
        if (!selectedDisease) return;

        try {
            await diseaseService.update(selectedDisease.id, diseaseData);
            await loadData();
            setIsEditModalOpen(false);
            setSelectedDisease(null);
        } catch (err) {
            setError('Не удалось обновить заболевание');
        }
    };

    const handleDeleteDisease = async (id: number) => {
        if (!window.confirm('Вы уверены, что хотите удалить это заболевание?')) return;

        try {
            await diseaseService.delete(id);
            await loadData();
        } catch (err) {
            setError('Не удалось удалить заболевание');
        }
    };

    if (loading) return <div className={styles.loading}>Загрузка...</div>;
    if (error) return <div className={styles.error}>{error}</div>;

    return (
        <div className={styles.page}>
            <div className={styles.pageHeader}>
                <h1>Управление заболеваниями</h1>
                <button
                    onClick={() => setIsCreateModalOpen(true)}
                    className={styles.createButton}
                >
                    Добавить заболевание
                </button>
            </div>

            <div className={styles.diseasesGrid}>
                {diseases.map(disease => (
                    <DiseaseCard
                        key={disease.id}
                        disease={disease}
                        onEdit={(disease) => {
                            setSelectedDisease(disease);
                            setIsEditModalOpen(true);
                        }}
                        onDelete={handleDeleteDisease}
                    />
                ))}
            </div>

            {diseases.length === 0 && (
                <div className={styles.noData}>
                    Заболевания отсутствуют. Добавьте первое заболевание.
                </div>
            )}

            {/* Модальное окно создания заболевания */}
            <Modal
                isOpen={isCreateModalOpen}
                onClose={() => setIsCreateModalOpen(false)}
                title="Добавить заболевание"
            >
                <DiseaseForm
                    onSubmit={handleCreateDisease}
                    onCancel={() => setIsCreateModalOpen(false)}
                />
            </Modal>

            {/* Модальное окно редактирования заболевания */}
            <Modal
                isOpen={isEditModalOpen}
                onClose={() => {
                    setIsEditModalOpen(false);
                    setSelectedDisease(null);
                }}
                title="Редактировать заболевание"
            >
                {selectedDisease && (
                    <DiseaseForm
                        disease={selectedDisease}
                        onSubmit={handleUpdateDisease}
                        onCancel={() => {
                            setIsEditModalOpen(false);
                            setSelectedDisease(null);
                        }}
                        isEditing={true}
                    />
                )}
            </Modal>
        </div>
    );
};

export default DiseasesPage;