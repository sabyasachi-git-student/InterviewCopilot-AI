import { useEffect, useState } from "react";

function JobApplications() {
  const [jobApplications, setJobApplications] = useState([]);
  const [companies, setCompanies] = useState([]);

  const [formData, setFormData] = useState({
    companyId: "",
    jobTitle: "",
    jobDescriptionUrl: "",
    applicationDate: "",
    applicationStatus: "",
    expectedSalary: "",
    notes: "",
  });

  const [editingId, setEditingId] = useState(null);

  const apiBaseUrl = "https://localhost:7042/api";

  useEffect(() => {
    fetchJobApplications();
    fetchCompanies();
  }, []);

  const fetchJobApplications = async () => {
    const response = await fetch(`${apiBaseUrl}/JobApplications`);
    const data = await response.json();
    setJobApplications(data);
  };

  const fetchCompanies = async () => {
    const response = await fetch(`${apiBaseUrl}/Companies`);
    const data = await response.json();
    setCompanies(data);
  };

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const resetForm = () => {
    setFormData({
      companyId: "",
      jobTitle: "",
      jobDescriptionUrl: "",
      applicationDate: "",
      applicationStatus: "",
      expectedSalary: "",
      notes: "",
    });

    setEditingId(null);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const payload = {
      companyId: Number(formData.companyId),
      jobTitle: formData.jobTitle,
      jobDescriptionUrl: formData.jobDescriptionUrl,
      applicationDate: formData.applicationDate,
      applicationStatus: formData.applicationStatus,
      expectedSalary: formData.expectedSalary
        ? Number(formData.expectedSalary)
        : null,
      notes: formData.notes,
    };

    if (editingId === null) {
      await fetch(`${apiBaseUrl}/JobApplications`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      });
    } else {
      await fetch(`${apiBaseUrl}/JobApplications/${editingId}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      });
    }

    resetForm();
    fetchJobApplications();
  };

  const handleEdit = (job) => {
    setEditingId(job.jobApplicationId);

    setFormData({
      companyId: job.companyId,
      jobTitle: job.jobTitle,
      jobDescriptionUrl: job.jobDescriptionUrl || "",
      applicationDate: job.applicationDate?.split("T")[0],
      applicationStatus: job.applicationStatus,
      expectedSalary: job.expectedSalary || "",
      notes: job.notes || "",
    });
  };

  const handleDelete = async (id) => {
    const confirmDelete = window.confirm(
      "Are you sure you want to delete this job application?",
    );

    if (!confirmDelete) return;

    await fetch(`${apiBaseUrl}/JobApplications/${id}`, {
      method: "DELETE",
    });

    fetchJobApplications();
  };

  return (
    <div style={{ padding: "20px" }}>
      <h2>Job Applications</h2>

      <form onSubmit={handleSubmit} style={{ marginBottom: "30px" }}>
        <div>
          <label>Company</label>
          <br />
          <select
            name="companyId"
            value={formData.companyId}
            onChange={handleChange}
            required
          >
            <option value="">Select Company</option>
            {companies.map((company) => (
              <option key={company.companyId} value={company.companyId}>
                {company.companyName}
              </option>
            ))}
          </select>
        </div>

        <br />

        <div>
          <label>Job Title</label>
          <br />
          <input
            type="text"
            name="jobTitle"
            value={formData.jobTitle}
            onChange={handleChange}
            required
          />
        </div>

        <br />

        <div>
          <label>Job Description URL</label>
          <br />
          <input
            type="text"
            name="jobDescriptionUrl"
            value={formData.jobDescriptionUrl}
            onChange={handleChange}
          />
        </div>

        <br />

        <div>
          <label>Application Date</label>
          <br />
          <input
            type="date"
            name="applicationDate"
            value={formData.applicationDate}
            onChange={handleChange}
            required
          />
        </div>

        <br />

        <div>
          <label>Application Status</label>
          <br />
          <select
            name="applicationStatus"
            value={formData.applicationStatus}
            onChange={handleChange}
            required
          >
            <option value="">Select Status</option>
            <option value="Applied">Applied</option>
            <option value="Shortlisted">Shortlisted</option>
            <option value="Interview Scheduled">Interview Scheduled</option>
            <option value="Rejected">Rejected</option>
            <option value="Offer Received">Offer Received</option>
          </select>
        </div>

        <br />

        <div>
          <label>Expected Salary</label>
          <br />
          <input
            type="number"
            name="expectedSalary"
            value={formData.expectedSalary}
            onChange={handleChange}
          />
        </div>

        <br />

        <div>
          <label>Notes</label>
          <br />
          <textarea
            name="notes"
            value={formData.notes}
            onChange={handleChange}
          />
        </div>

        <br />

        <button type="submit">
          {editingId === null
            ? "Add Job Application"
            : "Update Job Application"}
        </button>

        {editingId !== null && (
          <button
            type="button"
            onClick={resetForm}
            style={{ marginLeft: "10px" }}
          >
            Cancel
          </button>
        )}
      </form>

      <table border="1" cellPadding="8" cellSpacing="0">
        <thead>
          <tr>
            <th>Company</th>
            <th>Job Title</th>
            <th>Status</th>
            <th>Application Date</th>
            <th>Expected Salary</th>
            <th>Job URL</th>
            <th>Notes</th>
            <th>Actions</th>
          </tr>
        </thead>

        <tbody>
          {jobApplications.map((job) => (
            <tr key={job.jobApplicationId}>
              <td>{job.companyName}</td>
              <td>{job.jobTitle}</td>
              <td>{job.applicationStatus}</td>
              <td>{job.applicationDate?.split("T")[0]}</td>
              <td>{job.expectedSalary}</td>
              <td>
                {job.jobDescriptionUrl && (
                  <a href={job.jobDescriptionUrl} target="_blank">
                    View
                  </a>
                )}
              </td>
              <td>{job.notes}</td>
              <td>
                <button onClick={() => handleEdit(job)}>Edit</button>
                <button
                  onClick={() => handleDelete(job.jobApplicationId)}
                  style={{ marginLeft: "8px" }}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default JobApplications;
