function Dashboard() {
  return (
    <div className="dashboard-page">
      <div className="page-header">
        <h1>Dashboard</h1>
        <p>
          Track your job applications, interviews, questions, and AI-generated
          preparation insights.
        </p>
      </div>

      <div className="dashboard-cards">
        <div className="dashboard-card">
          <span>Total Applications</span>
          <h2>0</h2>
        </div>

        <div className="dashboard-card">
          <span>Companies</span>
          <h2>0</h2>
        </div>

        <div className="dashboard-card">
          <span>Interviews</span>
          <h2>0</h2>
        </div>

        <div className="dashboard-card">
          <span>AI Answers</span>
          <h2>0</h2>
        </div>
      </div>
    </div>
  );
}

export default Dashboard;
