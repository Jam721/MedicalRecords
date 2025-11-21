import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import styles from './Navbar.module.css';

const Navbar: React.FC = () => {
    const location = useLocation();

    return (
        <nav className={styles.navbar}>
            <div className={styles.navbarBrand}>
                <Link to="/" className={styles.brandLink}>
                    Medical Records
                </Link>
            </div>
            <ul className={styles.navbarNav}>
                <li className={styles.navItem}>
                    <Link
                        to="/patients"
                        className={`${styles.navLink} ${location.pathname === '/patients' ? styles.active : ''}`}
                    >
                        Пациенты
                    </Link>
                </li>
                <li className={styles.navItem}>
                    <Link
                        to="/doctors"
                        className={`${styles.navLink} ${location.pathname === '/doctors' ? styles.active : ''}`}
                    >
                        Докора
                    </Link>
                </li>
                <li className={styles.navItem}>
                    <Link
                        to="/diseases"
                        className={`${styles.navLink} ${location.pathname === '/diseases' ? styles.active : ''}`}
                    >
                        Болезни
                    </Link>
                </li>
            </ul>
        </nav>
    );
};

export default Navbar;