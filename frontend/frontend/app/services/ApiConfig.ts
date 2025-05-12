import axios from "axios";

// const url = `http://localhost:${process.env.NEXT_PUBLIC_BACKEND_PORT}`;
const url = `http://localhost:4001/`;

const instance = axios.create({
    baseURL:url,
    withCredentials:true,
    headers: {
        "Content-Type": "application/json"
    }
});

instance.interceptors.request.use(
    (config) => {
      config.headers.Authorization = `Bearer ${localStorage.getItem("token")}`;
      
      return config
    }
);

instance.interceptors.response.use(
    (config) => {
        return config;
    },
    async (error) => {
        const originalRequest = {...error.config};
        originalRequest._isRetry = true; 
        if (
        // проверим, что ошибка именно из-за невалидного accessToken
        error.response.status === 401 && 
        error.config &&
        !error.config._isRetry
        ) {
        try {
            const token = localStorage.getItem("token");
            // запрос на обновление токенов
            const resp = await instance.put("/api/refresh",{token});
            // сохраняем новый accessToken в localStorage
            localStorage.setItem("token", resp.data.AccessJwt);
            // переотправляем запрос с обновленным accessToken
            return instance.request(originalRequest);
        } catch (error) {
            console.log("Auth error:");
            console.log(error);
        }
        }
        return error.response;
    }
);

export default instance;