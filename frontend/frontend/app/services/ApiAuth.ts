import instance from "./ApiConfig";

export const Registration = async (email:string, password:string) => {
    return await instance.post("/api/auth/registration", {email, password});
};

export const Login = async (email:string, password:string) => {
        return await instance.post("/api/auth/login", {email, password})
    };
    
export const RefreshToken = async () => {
    const accessToken = localStorage.getItem("token");
        return await instance.put("/api/auth/refresh", accessToken);
    }
    ;
export const Logout= async () =>  {
        return await instance.delete("/api/auth/logout");
    };
