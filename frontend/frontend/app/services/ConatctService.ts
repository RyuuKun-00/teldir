export interface ContactRequest{
    name: string;
    number: string;
    description: string;
}

export const getAllContact = async ()=>{
    let response = await fetch("http://localhost:4001/Contacts");
    return response.json();
}

export const createContact = async (contactRequest: ContactRequest) =>{
    let id = await fetch("http://localhost:4001/Contacts",{
        method:"POST",
        headers:{
            "Content-Type": "application/json",
        },
        body: JSON.stringify(contactRequest),
    });

    return id;
}

export const updateContact = async (id: string,contactRequest: ContactRequest)=>{
    let _id = await fetch(`http://localhost:4001/Contacts/${id}`,{
        method: "PUT",
        headers:{
            "Content-Type": "application/json",
        },
        body: JSON.stringify(contactRequest),
    });

    return _id;
}

export const deleteContact = async (id: string) =>{
    let _id = await fetch(`http://localhost:4001/Contacts/${id}`,{
        method: "DELETE"
    });

    return _id;
}