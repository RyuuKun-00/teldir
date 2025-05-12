"use client"
import { configureStore } from "@reduxjs/toolkit";
import AuthReduser from "./AuthSlice";

export const store = configureStore({
    reducer:{
        Auth: AuthReduser
    }
});

export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch