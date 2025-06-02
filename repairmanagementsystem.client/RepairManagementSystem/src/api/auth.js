const API_URL = "http://localhost:5062/api/Auth";

export const loginUser = async (data) => {
  const res = await fetch(`${API_URL}/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    credentials: "include",
    body: JSON.stringify(data),
  });

  if (!res.ok) {
    const text = await res.text();
    throw new Error(text || "Login failed");
  }

  return await res.json();
};

export const registerUser = async (formData) => {
  return await fetch(`${API_URL}/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    credentials: "include",
    body: JSON.stringify({
      email: formData.email,
      password: formData.password,
      firstName: formData.name,
      lastName: formData.surname,
      number: formData.phone,
      address: {
        country: formData.country,
        city: formData.city,
        postalCode: formData.postalCode,
        street: formData.street,
        houseNumber: formData.houseNumber,
        apartNumber: formData.apartment
      }
    })
  });
};

export const refreshAccessToken = async () => {
    const res = await fetch("http://localhost:5062/api/Auth/refresh", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      credentials: "include"
    });
  
    if (!res.ok) return null;
  
    const data = await res.json();
    return data.token;
};

export const logoutUser = async (accessToken) => {
  const performLogout = async (token) => {
    return fetch(`${API_URL}/logout`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      credentials: "include"
    });
  };

  let response = await performLogout(accessToken);

  if (response.status === 401) {
    const refreshed_token = await refreshAccessToken();
    if (refreshed_token) {
      response = await performLogout(refreshed_token);
    }
  }

  return response;
};
