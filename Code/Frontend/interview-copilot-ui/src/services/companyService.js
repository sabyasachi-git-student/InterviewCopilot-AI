const API_URL = "https://localhost:7042/api/Companies";

export async function getCompanies() {
  const response = await fetch(API_URL);

  if (!response.ok) {
    throw new Error("Failed to fetch companies");
  }

  return await response.json();
}

export async function addCompany(company) {
  const response = await fetch(API_URL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(company),
  });

  if (!response.ok) {
    throw new Error("Failed to add company");
  }

  return await response.json();
}

export async function updateCompany(companyId, company) {
  const response = await fetch(`${API_URL}/${companyId}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(company),
  });

  if (!response.ok) {
    throw new Error("Failed to update company");
  }

  return await response.json();
}

export async function deleteCompany(companyId) {
  const response = await fetch(`${API_URL}/${companyId}`, {
    method: "DELETE",
  });

  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || "Failed to delete company");
  }
}
