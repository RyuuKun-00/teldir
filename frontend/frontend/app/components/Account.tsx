"use client"
import '@ant-design/v5-patch-for-react-19';
import {useAppDispatch,useAppSelector} from "../store/hooks"
import {User} from "../store/AuthSlice"
import Button from "antd/es/button"
import {IsAuth_value,IsAuthProcess_value,setIsAuth,setIsAuthProcess, setUser} from "../store/AuthSlice"
import {useEffect, useState} from "react"
import {ConfigProvider} from "antd"
import {RefreshToken,Registration,Login,Logout} from "../services/ApiAuth"
import {LoginOrRegistration,ModalType} from "../components/LoginOrRegistartion"
/* eslint-disable */
export const Account = ()=>{
    
    const Auth_dispatch = useAppDispatch();
    const IsAuth = useAppSelector(IsAuth_value);
    const IsAuthProcess = useAppSelector(IsAuthProcess_value);
    const [user,setU]=useState<string>("");
    const [mode,setMode] = useState<ModalType>(ModalType.Login);
    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);

    useEffect( ()=> {
        const getUser = async ()=>{
            Auth_dispatch(setIsAuthProcess(true));
            const res = await RefreshToken();
            if(res.status==200){
                handleAuthOk(res.data);
            }else{
                handleAuthBad();
            }
            Auth_dispatch(setIsAuthProcess(false));
        };

        getUser();

    },[]);



    const handleCancel = async ()=>{
        setIsModalOpen(false);
    };

    const handleAuthOk = (data:any)=>{
        const user:User={ id:data.id, email: data.email};
        localStorage.setItem("token",data.accessJwt);
        Auth_dispatch(setUser(user));
        Auth_dispatch(setIsAuth(true));
        setU(data.email);
    }

    const handleAuthBad = () => {
        localStorage.removeItem("token");
        Auth_dispatch(setIsAuth(false));
        Auth_dispatch(setUser(null));
        setU("");
    };

    const handleRegister= async (email:string,password:string)=>{
        Auth_dispatch(setIsAuthProcess(true));
        const res = await Registration(email,password);
        
        if(res.status==200){
            handleAuthOk(res.data);
        }else{
            alert(res.statusText);
        }
        Auth_dispatch(setIsAuthProcess(false));
    };

    const handleLogin= async (email:string,password:string)=>{
        Auth_dispatch(setIsAuthProcess(true));
        const res = await Login(email,password);
        if(res.status==200){
            handleAuthOk(res.data);
        }else{
            alert(res.statusText);
        }
        Auth_dispatch(setIsAuthProcess(false));
    };

    const handleLogout = async ()=>{
        Auth_dispatch(setIsAuthProcess(true));
        const res = await Logout();
        if(res.status==200){
            handleAuthBad();
        }else{
            alert(res.statusText);
        }
        Auth_dispatch(setIsAuthProcess(false));
    };

    const openRegisterModal = () => {
            setMode(ModalType.Registration);
            setIsModalOpen(true);
    };

    const openloginModal = () => {
        setMode(ModalType.Login);
        setIsModalOpen(true);
    };

    return (
        <div className="account">
            {IsAuthProcess == false &&
            <ConfigProvider
            theme={{
                components: {
                Button: {
                    defaultColor: "rgba(255, 255, 255, 0.65)",
                    defaultHoverColor: "rgba(255, 255, 255, 1)",
                    defaultBg: "transparent",
                    defaultHoverBg: "transparent",
                    defaultBorderColor: "transparent",
                    defaultHoverBorderColor: "transparent",
                    defaultActiveBg: "transparent",
                    defaultActiveBorderColor:"transparent",
                    defaultActiveColor: "rgba(255, 255, 255, 0.65)",
                },
                },
            }}
            >
                {IsAuth
                ?
                <div className="account_title">
                    <Button >{user}</Button>
                    <p style={{color:"rgba(255, 255, 255, 0.65)"}}>/</p>
                    <Button onClick={()=>{handleLogout()}} >Выход</Button>
                    
                    
                </div>
                :
                <div className="account_title">
                    <Button onClick={()=>{openloginModal()}} >Вход </Button>
                    <p style={{color:"rgba(255, 255, 255, 0.65)"}}>/</p>
                    <Button onClick={()=>{openRegisterModal()}}> Регистрация</Button>
                </div>
                }
            </ConfigProvider>
            }

            <LoginOrRegistration
                mode={mode}
                isModelopen={isModalOpen}
                handleCancel={handleCancel}
                handleRegister={handleRegister}
                handleLogin={handleLogin}
            />
        </div>
        
    );
}