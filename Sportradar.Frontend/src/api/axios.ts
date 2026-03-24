import axios from "axios";

const apiUrl = import.meta.env.VITE_API_BASE_URL + "/api";

export let refreshPromise: Promise<string> | null = null;

export const apiClient = axios.create({
    baseURL: apiUrl,
    withCredentials: true
});

// apiClient.interceptors.request.use(config => {
//     const token = localStorage.getItem("token");
//     if(token)
//         config.headers.Authorization = `Bearer ${token}`;

//     return config;
// })
