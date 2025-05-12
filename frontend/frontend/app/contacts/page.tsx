"use client";

import '@ant-design/v5-patch-for-react-19';
import "./pageContacts.css"
import Button from "antd/es/button/button";
import Search from "antd/es/input/Search";
import Pagination from "antd/es/pagination/Pagination";
import { Contacts } from "../components/Contacts";
import { useEffect, useState } from "react";
import { ContactRequest, createContact, deleteContact, getContact, getSearchContact, updateContact } from "../services/ConatctService";
import { CreateUpdateConstact, Mode } from "../components/CreateUpdateContact";
import { Divider, Spin } from "antd";
import {IsAuth_value,IsAuthProcess_value} from "../store/AuthSlice"
import {useAppSelector} from "../store/hooks"

/* eslint-disable */
export default function ContactsPage() {
    const defaultValues = {
        userId: "",
        isGlobal: false,
        name: "",
        number: "",
        description: ""
    } as Contact;


    const IsAuth = useAppSelector(IsAuth_value);
    const IsAuthProcess = useAppSelector(IsAuthProcess_value);

    const [values, setValues] = useState<Contact>(defaultValues);
    const [contacts, setContacts] = useState<Contact[]>([]);
    const [count, setCount] = useState<number>(0);
    const [current, setCurrent] = useState<number>(0);
    const [search, setSearch] = useState<string>("");
    const [loading, setLoading] = useState<boolean>(true);
    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
    const [mode, setMode] = useState<Mode>(Mode.Create);

    

    useEffect(() => {
        const getContacts = async () => {
            const data = await getSearchContact(search);
            setCurrent(1);
            setLoading(false);
            setContacts(data.contacts);
            setCount(data.count);
        }
        if(!IsAuthProcess){
            getContacts();
        }
    },[IsAuth]);

    const handleCreateContact = async (request: ContactRequest) => {
        await createContact(request);
        handleAfterProcessingContact();
    }

    const handleUpdateContact = async (id: string, request: ContactRequest) => {
        await updateContact(id, request);
        handleAfterProcessingContact();
    }

    const handleDeleteContact = async (id: string) => {
        await deleteContact(id);
        handleAfterProcessingContact();
    }

    const handleAfterProcessingContact = async () =>{
        closeModal();
        const data = await getContact(current);
        setContacts(data.contacts);
        setCount(data.count);
    };

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
        onChange(current,20);
    }

    const searchContacts = async (str:string)=>{
        setLoading(true);
        setSearch(str);
        let data;
        if(str==""){
            data = await getContact();
        }else{
            data=await getSearchContact(str);
        }
        setContacts(data.contacts);
        setCount(data.count);
        setCurrent(1);
        setLoading(false);
    }
/* eslint-disable */
    const onChange = async (page:number, pageSize:number) =>{
        let data;
        if(search==""){
            data = await getContact(page);
        }else{
            data=await getSearchContact(search,page);
        }
        setContacts(data.contacts);
        setCount(data.count);
        setCurrent(page);
    }
    return (
        <div style={{margin: "2vw",flexGrow:1}}>
            {IsAuth &&
                <Button onClick={openModal} style={{marginRight: 15}}>Добавить контакт

                </Button>
            }
            <Search placeholder="Текст для поиска" 
                onSearch={(str)=>searchContacts(str)} 
                style={{ width: 200}} 
                allowClear={true}
            />

            <Divider style={{borderColor: "silver",margin:"15px 0px"}}></Divider>
            {IsAuth &&
                <CreateUpdateConstact 
                mode={mode} 
                values={values} 
                isModelopen={isModalOpen} 
                handleCreate={handleCreateContact} 
                handleUpdate={handleUpdateContact}
                handleCancel={closeModal}
                setError={setError}
            />
            }
            

                {loading && IsAuthProcess
                ?   <div className="load">
                        <Spin tip="Загрузка..."><div style={{padding:"30px"}}></div></Spin> 
                    </div>
                :
                    <div>
                    <Contacts contacts={contacts} handleUpdate={openEditModal} handleDelete={handleDeleteContact}/>
                    <Pagination onChange={onChange} pageSize={40} total={count} align="center" current={current} showTotal={(total) => `Всего ${total} контактов`} showQuickJumper/>
                    </div>
                }
            
        </div>
    );
}