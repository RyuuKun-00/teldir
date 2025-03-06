"use client";

import '@ant-design/v5-patch-for-react-19';
import "./pageContacts.css"
import Button from "antd/es/button/button";
import Search from "antd/es/input/Search";
import { Contacts } from "../components/Contacts";
import { useEffect, useState } from "react";
import { ContactRequest, createContact, deleteContact, getAllContact, getSearchContact, updateContact } from "../services/ConatctService";
import { CreateUpdateConstact, Mode } from "../components/CreateUpdateContact";
import { Divider, Spin } from "antd";



export default function ContactsPage() {
    const defaultValues = {
        name: "",
        number: "",
        description: ""
    } as Contact;

    const [values, setValues] = useState<Contact>(defaultValues);

    const [contacts, setContacts] = useState<Contact[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
    const [mode, setMode] = useState<Mode>(Mode.Create);

    useEffect(() => {
        const getContacts = async () => {
            const contacts = await getAllContact();
            setLoading(false);
            setContacts(contacts);
        }

        getContacts();
    }, []);

    const handleCreateContact = async (request: ContactRequest) => {
        await createContact(request);
        closeModal();

        const contact = await getAllContact();
        setContacts(contact);
    }

    const handleUpdateContact = async (id: string, request: ContactRequest) => {
        await updateContact(id, request);
        closeModal();

        const contact = await getAllContact();
        setContacts(contact);
    }

    const handleDeleteContact = async (id: string) => {
        await deleteContact(id);
        closeModal();

        const contact = await getAllContact();
        setContacts(contact);
    }

    const setError = (id:string,isError:boolean)=>{
        const element = document.getElementById(id);
        if(element){
            element.style.display= isError?"inline":"none";
        }else{
            console.log(`Element \"${id}\" not found.`);
        }
    };

    const openEditModal = (contact: Contact) => {
        setMode(Mode.Edit);
        setValues(contact);
        setIsModalOpen(true);
    }

    const openModal = () => {
        setMode(Mode.Create);
        setIsModalOpen(true);
    }

    const closeModal = () => {
        setValues(defaultValues);
        setIsModalOpen(false);
        setError("name",false);
        setError("number",false);
    }

    const searchContacts = async (str:string)=>{
        setLoading(true);
        let contact;
        if(str==""){
            contact = await getAllContact();
        }else{
            contact=await getSearchContact(str);
        }
        setContacts(contact);
        setLoading(false);
    }
    return (
        <div style={{margin: "15px",flexGrow:1}}>
            <Button onClick={openModal}>Добавить контакт

            </Button>
            <Search placeholder="Текст для поиска" 
                onSearch={(str)=>searchContacts(str)} 
                style={{ width: 200, marginLeft: 15}} 
                allowClear={true}
            />

            <Divider style={{borderColor: "silver",margin:"15px 0px"}}></Divider>

            <CreateUpdateConstact 
                mode={mode} 
                values={values} 
                isModelopen={isModalOpen} 
                handleCreate={handleCreateContact} 
                handleUpdate={handleUpdateContact}
                handleCancel={closeModal}
                setError={setError}
            />

                {loading 
                ?   <div className="load">
                        <Spin tip="Загрузка..."><div style={{padding:"30px"}}></div></Spin> 
                    </div>
                :
                    <Contacts contacts={contacts} handleUpdate={openEditModal} handleDelete={handleDeleteContact}/>
                }
            
        </div>
    );
}