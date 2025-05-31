// src/api/auth.js
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
    console.log("Registering user with data:", formData);
    const res = await fetch(`${API_URL}/register`, {
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
            apartNumber: formData.apartment,
            houseNumber: formData.houseNumber
          }
        }),
      });      
  return res;
};
