import { Link } from "react-router-dom";

function Sidebar() {
  return (
    <aside className="sidebar">
      <div className="brand">
        <h2>InterviewCoPilot</h2>
        <span>AI</span>
      </div>

      <nav className="sidebar-nav">
        <Link to="/">Dashboard</Link>
        <Link to="/companies">Companies</Link>
        <Link to="/job-applications">Job Applications</Link>
      </nav>
    </aside>
  );
}

export default Sidebar;
