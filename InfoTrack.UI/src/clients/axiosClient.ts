// axiosClient.ts
import axios, { HttpStatusCode } from "axios";

export const axiosClient = axios.create({
  baseURL: "https://localhost:7206/infotrack/api/",
  headers: {
    "Content-Type": "application/json"
  },
  timeout: 15000,
  withCredentials: true
});

let onUnauthorized: (() => void) | null = null;

export const setUnauthorizedHandler = (handler: () => void) => {
  onUnauthorized = handler;
};

axiosClient.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === HttpStatusCode.Unauthorized && onUnauthorized) {
      onUnauthorized();
    }
    return Promise.reject(error);
  }
);