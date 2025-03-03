export interface ContactRequest{
    name: string;
    number: string;
    description: string;
}

const url = `http://localhost:${process.env.NEXT_PUBLIC_BACKEND_PORT}/api`;

export const getAllContact = async ()=>{
    const response = await fetch(`${url}/Contacts`);
    return response.json();
}

export const createContact = async (contactRequest: ContactRequest) =>{
    const id = await fetch(`${url}/Contacts`,{
        method:"POST",
        headers:{
            "Content-Type": "application/json",
        },
        body: JSON.stringify(contactRequest),
    });

    return id;
}

export const updateContact = async (id: string,contactRequest: ContactRequest)=>{
    const _id = await fetch(`${url}/Contacts/${id}`,{
        method: "PUT",
        headers:{
            "Content-Type": "application/json",
        },
        body: JSON.stringify(contactRequest),
    });

    return _id;
}

export const deleteContact = async (id: string) =>{
    const _id = await fetch(`${url}/Contacts/${id}`,{
        method: "DELETE"
    });

    return _id;
}