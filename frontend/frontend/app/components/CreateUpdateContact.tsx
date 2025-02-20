import Modal from "antd/es/modal/Modal";
import { ContactRequest } from "../services/ConatctService";
import { useEffect, useState } from "react";
import Input from "antd/es/input/Input";
import TextArea from "antd/es/input/TextArea";
/* eslint-disable */
interface Props{
    mode: Mode;
    values: Contact;
    isModelopen: boolean;
    handleCancel: () => void;
    handleCreate: (contactRequest: ContactRequest) => void;
    handleUpdate: (id: string, contactRequest:ContactRequest) => void;
}

export enum Mode{
    Create,
    Edit
}

export const CreateUpdateConstact = ({
    mode,
    values,
    isModelopen,
    handleCancel,
    handleCreate,
    handleUpdate
}:Props)=>{
    const [name,setName] = useState<string>("");
    const [number,setNumber] = useState<string>("");
    const [description,setDescription] = useState<string>("");

    useEffect(()=>{
        setName(values.name);
        setNumber(values.number);
        setDescription(values.description);
    },[values]);

    const handleOnOk = async ()=>{
        const contactRequest  = {name,number,description};

        (mode == Mode.Create ? handleCreate(contactRequest) : handleUpdate(values.id,contactRequest));
    }

    return (
        <Modal title={mode === Mode.Create ? "Добавить контакт" : "Изменить контакт"} 
            open={isModelopen}
            cancelText = "Отмена"
            onOk={handleOnOk}
            onCancel={handleCancel}
        >
            <div className="contact__modal">
                <Input
                    classNames={{}}
                    value={name}
                    onChange={(e)=>setName(e.target.value)}
                    placeholder="Имя"
                />
                <Input 
                    value={number}
                    onChange={(e)=>setNumber(e.target.value)}
                    placeholder="Номер"
                />
                <TextArea
                    value={description}
                    onChange={(e)=>setDescription(e.target.value)}
                    autoSize={{minRows:3, maxRows:3}}
                    placeholder="Описание"
                />
            </div>
        </Modal>
    )
}