// src/api/authFetch.js

const API_URL = "http://localhost:5062/api";

let isRefreshing = false;
let failedQueue = [];

const processQueue = (error, token = null) => {
  failedQueue.forEach((prom) => {
    if (error) {
      prom.reject(error);
    } else {
      prom.resolve(token);
    }
  });

  failedQueue = [];
};

export const authFetch = async (url, options = {}) => {
  const token = getAccessToken();

  const headers = {
    ...options.headers,
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  };

  try {
    const res = await fetch(`${API_URL}${url}`, {
      ...options,
      headers,
      credentials: "include",
    });

    if (res.status !== 401) return res;

    if (!isRefreshing) {
      isRefreshing = true;

      try {
        const refreshRes = await fetch(`${API_URL}/Auth/refresh-token`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          credentials: "include",
          body: JSON.stringify({
            refreshToken: getRefreshToken(),
          }),
        });

        if (!refreshRes.ok) {
          throw new Error("Refresh failed");
        }

        const data = await refreshRes.json();
        setAccessToken(data.token);
        processQueue(null, data.token);
        return authFetch(url, options);
      } catch (err) {
        processQueue(err, null);
        throw err;
      } finally {
        isRefreshing = false;
      }
    }

    return new Promise((resolve, reject) => {
      failedQueue.push({
        resolve: (newToken) => {
          resolve(
            authFetch(url, {
              ...options,
              headers: {
                ...options.headers,
                Authorization: `Bearer ${newToken}`,
              },
            })
          );
        },
        reject,
      });
    });
  } catch (err) {
    throw err;
  }
};
