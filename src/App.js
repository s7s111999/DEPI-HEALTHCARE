import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import NavBar from './components/NavBar';
import Appointments from './components/Appointments';
import CreateRecord from './components/CreateRecord';
import ChangePassword from './components/ChangePassword';
import CreatePrescription from './components/CreatePrescription';
import DoctorCalendar from './components/DoctorCalendar';
import Login from './components/Login';
import EditProfile from './components/EditProfile';
import LandingPage from './components/LandingPage';
import MedicalDashboard from './components/MedicalDashboard';
import Dashboard from './components/Dashboard';
import PatientHistory from './components/PatientHistory';
import PrescriptionHistory from './components/PrescriptionHistory';
import Register from './components/Register';
import PrescriptionList from './components/PrescriptionList';
import Profile from './components/Profile';
import './App.css';

function App() {
  return (
    <Router>
      <Switch>
        <Route exact path="/" component={LandingPage} />
        <Route path="/login" component={Login} />
        <Route path="/register" component={Register} />
        <Route path="/dashboard" component={Dashboard} />
        <Route path="/appointments" component={Appointments} />
        <Route path="/create-record" component={CreateRecord} />
        <Route path="/change-password" component={ChangePassword} />
        <Route path="/create-prescription" component={CreatePrescription} />
        <Route path="/doctor-calendar" component={DoctorCalendar} />
        <Route path="/profile" component={Profile} />
        <Route path="/edit-profile" component={EditProfile} />
        <Route path="/medical-records" component={MedicalDashboard} />
        <Route path="/patient-history" component={PatientHistory} />
        <Route path="/prescription-list" component={PrescriptionList} />
        <Route path="/prescription-history" component={PrescriptionHistory} />
      </Switch>
    </Router>
  );
}

export default App;