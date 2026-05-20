import { useEffect, useState } from "react";
import {
  getCompanies,
  addCompany,
  updateCompany,
  deleteCompany,
} from "../services/companyService";

function Companies() {
  const [showForm, setShowForm] = useState(false);
  const [companies, setCompanies] = useState([]);
  const [editingCompanyId, setEditingCompanyId] = useState(null);
  const [errorMessage, setErrorMessage] = useState("");

  const emptyForm = {
    userId: 1,
    companyName: "",
    industry: "",
    location: "",
    website: "",
    notes: "",
  };

  const [formData, setFormData] = useState(emptyForm);

  useEffect(() => {
    loadCompanies();
  }, []);

  async function loadCompanies() {
    const data = await getCompanies();
    setCompanies(data);
  }

  function handleChange(e) {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  }

  function handleEdit(company) {
    setErrorMessage("");
    setEditingCompanyId(company.companyId);
    setShowForm(true);

    setFormData({
      userId: company.userId,
      companyName: company.companyName,
      industry: company.industry || "",
      location: company.location || "",
      website: company.website || "",
      notes: company.notes || "",
    });
  }

  async function handleDelete(companyId) {
    const confirmDelete = window.confirm(
      "Are you sure you want to delete this company?",
    );

    if (!confirmDelete) {
      return;
    }

    try {
      setErrorMessage("");
      await deleteCompany(companyId);
      await loadCompanies();
    } catch (error) {
      setErrorMessage(error.message);
    }
  }

  async function handleSubmit(e) {
    e.preventDefault();

    try {
      setErrorMessage("");

      if (editingCompanyId) {
        await updateCompany(editingCompanyId, {
          companyName: formData.companyName,
          industry: formData.industry,
          location: formData.location,
          website: formData.website,
          notes: formData.notes,
        });
      } else {
        await addCompany(formData);
      }

      setFormData(emptyForm);
      setEditingCompanyId(null);
      setShowForm(false);
      await loadCompanies();
    } catch (error) {
      setErrorMessage(error.message);
    }
  }

  function handleCancel() {
    setFormData(emptyForm);
    setEditingCompanyId(null);
    setShowForm(false);
    setErrorMessage("");
  }

  return (
    <div className="companies-page">
      <div className="page-header-row">
        <div>
          <h1>Companies</h1>
          <p>Manage companies where you are applying or interviewing.</p>
        </div>

        <button
          className="primary-button"
          onClick={() => {
            setShowForm(true);
            setEditingCompanyId(null);
            setFormData(emptyForm);
            setErrorMessage("");
          }}
        >
          + Add Company
        </button>
      </div>

      {errorMessage && <div className="error-box">{errorMessage}</div>}

      {showForm && (
        <form className="form-card" onSubmit={handleSubmit}>
          <h2>{editingCompanyId ? "Edit Company" : "Add Company"}</h2>

          <div className="form-grid">
            <div className="form-group">
              <label>Company Name</label>
              <input
                name="companyName"
                value={formData.companyName}
                onChange={handleChange}
                type="text"
                placeholder="Example: Microsoft"
                required
              />
            </div>

            <div className="form-group">
              <label>Industry</label>
              <input
                name="industry"
                value={formData.industry}
                onChange={handleChange}
                type="text"
                placeholder="Example: Technology"
              />
            </div>

            <div className="form-group">
              <label>Location</label>
              <input
                name="location"
                value={formData.location}
                onChange={handleChange}
                type="text"
                placeholder="Example: Hyderabad"
              />
            </div>

            <div className="form-group">
              <label>Website</label>
              <input
                name="website"
                value={formData.website}
                onChange={handleChange}
                type="text"
                placeholder="Example: https://microsoft.com"
              />
            </div>

            <div className="form-group full-width">
              <label>Notes</label>
              <textarea
                name="notes"
                value={formData.notes}
                onChange={handleChange}
                placeholder="Any notes about this company"
              ></textarea>
            </div>
          </div>

          <div className="form-actions">
            <button
              type="button"
              className="secondary-button"
              onClick={handleCancel}
            >
              Cancel
            </button>

            <button type="submit" className="primary-button">
              {editingCompanyId ? "Update Company" : "Save Company"}
            </button>
          </div>
        </form>
      )}

      <div className="table-card">
        <table className="data-table">
          <thead>
            <tr>
              <th>Company Name</th>
              <th>Industry</th>
              <th>Location</th>
              <th>Website</th>
              <th>Notes</th>
              <th>Actions</th>
            </tr>
          </thead>

          <tbody>
            {companies.length === 0 ? (
              <tr>
                <td colSpan="6" className="empty-state">
                  No companies found.
                </td>
              </tr>
            ) : (
              companies.map((company) => (
                <tr key={company.companyId}>
                  <td>{company.companyName}</td>
                  <td>{company.industry}</td>
                  <td>{company.location}</td>
                  <td>{company.website}</td>
                  <td>{company.notes}</td>
                  <td>
                    <button
                      className="link-button"
                      onClick={() => handleEdit(company)}
                    >
                      Edit
                    </button>

                    <button
                      className="danger-link-button"
                      onClick={() => handleDelete(company.companyId)}
                    >
                      Delete
                    </button>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default Companies;
