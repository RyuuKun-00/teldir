"use client"
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import type { RootState } from "./store";

export interface User{
    id: string | null,
    email: string | null
}
// Определяем тип части состояния(среза/slice)
export interface IsAuthState {
    IsAuth: boolean,
    IsAuthProcess: boolean,
    User: User | null
  }
  
// Определение начального состояния, используя тип
const initialState: IsAuthState = {
    IsAuth: false,
    IsAuthProcess: false,
    User: null
}

export const isAuthSlice = createSlice({
    name: "Auth",
    initialState,
    reducers:{
        setIsAuth: (state,action: PayloadAction<boolean>) =>{
            state.IsAuth = action.payload;
        },
        setIsAuthProcess: (state,action: PayloadAction<boolean>) =>{
            state.IsAuthProcess = action.payload;
        },
        setUser: (state,action: PayloadAction<User|null>) =>{
            state.User = action.payload;
        },
    }
});

export const { setIsAuth,setIsAuthProcess,setUser} = isAuthSlice.actions;

export const User_value = (state: RootState) => state.Auth.User;
export const IsAuth_value = (state: RootState) => state.Auth.IsAuth;
export const IsAuthProcess_value  = (state: RootState) => state.Auth.IsAuthProcess;

export default isAuthSlice.reducer;