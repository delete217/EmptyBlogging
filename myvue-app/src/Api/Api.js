import axios from "axios";


const createAxiosInstance = () => {
    const instance = axios.create({
        baseURL: "http://localhost:5077/api",
        timeout: 5000
    });

    // 在每次请求之前的请求头添加Token
    instance.interceptors.request.use(
        (config) => {
            const token = localStorage.getItem("Token");
            // 如果存在token 就添加到请求头
            if (token) {
                config.headers['Authorization'] = token;
            }

            return config;
        },
        (error) => {
            return Promise.reject(error)
        }
    );

    return instance
};
export default createAxiosInstance;  