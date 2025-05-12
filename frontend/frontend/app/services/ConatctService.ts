export interface ContactRequest{
    isGlobal:boolean
    name: string;
    number: string;
    description: string;
}

import  instance  from "./ApiConfig";

export const getContact = async (page:number|null=null)=>{

    const params = new URLSearchParams();
    if(page){
        params.append('page', page.toString());
    }
    const response = await instance.get(`/api/contacts`,{params});
    return response.data;
}

export const getSearchContact = async (searchString:string,page:number|null=null)=>{
    const params = new URLSearchParams();
    params.append('search', searchString);
    if(page){
        params.append('page', page.toString());
    }
    
    const response = await instance.get(`/api/contacts/search`,{params});
    return response.data;
}

export const createContact = async (contactRequest: ContactRequest) =>{

    const response = await instance.post(`/api/contacts`,contactRequest);

    return response.data;
}

export const updateContact = async (id: string,contactRequest: ContactRequest)=>{
    const params = new URLSearchParams();
    params.append('id', id);
    const response = await instance.put(`/api/contacts`,contactRequest,{params});

    return response.data;
}

export const deleteContact = async (id: string) =>{
    const params = new URLSearchParams();
    params.append('id', id);
    const response = await instance.delete(`/api/contacts`,{params});

    return response.data;
}