import axios from "axios";

export const axiosClient = axios.create({
  baseURL: "http://localhost:5287/infotrack/api/",
  headers: {
    "Content-Type": "application/json"
  },
  timeout: 15000
});
