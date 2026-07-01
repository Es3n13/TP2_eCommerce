import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Layout from './components/Layout';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import DashboardPage from './pages/DashboardPage';
import DeclarationFormPage from './pages/DeclarationFormPage';
import AgentQueuePage from './pages/AgentQueuePage';
import AgentReviewPage from './pages/AgentReviewPage';

function App() {
  return (
    <Router>
      <Routes>
        {/* Public Route */}
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />

        {/* User Routes */}
        <Route element={<Layout role="user" />}>
          <Route path="/dashboard" element={<DashboardPage />} />
          <Route path="/declare" element={<DeclarationFormPage />} />
        </Route>

        {/* Agent Routes */}
        <Route element={<Layout role="agent" />}>
          <Route path="/agent-queue" element={<AgentQueuePage />} />
          <Route path="/agent/review/:id" element={<AgentReviewPage />} />
        </Route>

        {/* Default Redirect */}
        <Route path="*" element={<LoginPage />} />
      </Routes>
    </Router>
  );
}

export default App;
