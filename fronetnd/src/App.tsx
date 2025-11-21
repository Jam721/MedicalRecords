import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Layout from './components/common/Layout/Layout';
import PatientsPage from './pages/Patients/PatientsPage';
import DoctorsPage from './pages/Doctors/DoctorsPage';
import DiseasesPage from './pages/Diseases/DiseasesPage';
import './App.css';

function App() {
    return (
        <Router>
            <Layout>
                <Routes>
                    <Route path="/" element={<PatientsPage />} />
                    <Route path="/patients" element={<PatientsPage />} />
                    <Route path="/doctors" element={<DoctorsPage />} />
                    <Route path="/diseases" element={<DiseasesPage />} />
                </Routes>
            </Layout>
        </Router>
    );
}

export default App;