import axios from 'axios';

const BASE_URL = 'http://localhost:5078';
export const MINIO_URL = 'http://localhost:9000/images/';

const axiosInstance = axios.create({
    baseURL: BASE_URL
});

export const axiosPrivate = axios.create({
    baseURL: BASE_URL,
    headers: { 'Content-Type': 'application/json' }
})

axiosPrivate.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('authToken');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    }, 
    (error) => Promise.reject(error)
);

export default axiosInstance;