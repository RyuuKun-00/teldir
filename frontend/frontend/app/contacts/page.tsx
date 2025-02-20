"use client";


import "./pageContacts.css"
import  Button from "antd/es/button/button";
import { Contacts } from "../components/Contacts";
import { useEffect, useState } from "react";
import { ContactRequest, createContact, deleteContact, getAllContact, updateContact } from "../services/ConatctService";

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
    }

    return (
        <div style={{margin: "15px",flexGrow:1}}>
            <Button onClick={openModal}>Добавить контакт</Button>

            <Divider style={{borderColor: "silver",margin:"15px 0px"}}></Divider>

            <CreateUpdateConstact 
                mode={mode} 
                values={values} 
                isModelopen={isModalOpen} 
                handleCreate={handleCreateContact} 
                handleUpdate={handleUpdateContact}
                handleCancel={closeModal}
                
            />

                {loading 
                ?   <div className="load">
                        <Spin tip="Загрузка..."><div style={{padding:"30px"}}></div></Spin> 
                    </div>
                : <Contacts contacts={contacts} handleUpdate={openEditModal} handleDelete={handleDeleteContact}/>}
            
        </div>
    );
}