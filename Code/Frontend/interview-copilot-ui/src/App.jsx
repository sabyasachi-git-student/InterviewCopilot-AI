import { BrowserRouter, Routes, Route } from "react-router-dom";
import Sidebar from "./components/Sidebar";
import Dashboard from "./pages/Dashboard";
import Companies from "./pages/Companies";
import "./App.css";

function App() {
  return (
    <BrowserRouter>
      <div className="app-container">
        <Sidebar />

        <main className="content">
          <Routes>
            <Route path="/" element={<Dashboard />} />
            <Route path="/companies" element={<Companies />} />
          </Routes>
        </main>
      </div>
    </BrowserRouter>
  );
}

export default App;
