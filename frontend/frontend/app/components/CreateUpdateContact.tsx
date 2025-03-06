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
    setError: (id:string,isError:boolean)=>void;
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
    handleUpdate,
    setError
}:Props)=>{
    const [name,setName] = useState<string>("");
    const [number,setNumber] = useState<string>("");
    const [description,setDescription] = useState<string>("");
    const [processing,setProcessing] = useState<Boolean>(false);

    useEffect(()=>{
        setName(values.name);
        setNumber(values.number);
        setDescription(values.description);
    },[values]);

    const handleOnOk = async ()=>{
        var isValid = true;
        if(name == ""){
            setError("name",true);
            isValid = false;
        }else setError("name",false);
        if(number == ""){
            setError("number",true);
            isValid = false;
        }else setError("number",false);
        if(!isValid){
            setProcessing(false);
            return;
        } 
        const contactRequest  = {name,number,description};
        (mode == Mode.Create ? handleCreate(contactRequest) : handleUpdate(values.id,contactRequest));
        setProcessing(false);
    }

    return (
        <Modal title={mode === Mode.Create ? "Добавить контакт" : "Изменить контакт"} 
            open={isModelopen}
            cancelText = "Отмена"
            onOk={()=>{
                if(processing) return;
                setProcessing(true);
                handleOnOk();
            }}
            onCancel={()=>{handleCancel();}}
            closable={false}
            maskClosable={false}
        >
            <div className="contact__modal">
                
                <Input
                    classNames={{}}
                    value={name}
                    onChange={(e)=>setName(e.target.value)}
                    placeholder="Имя"
                />
                <p className="set_error" id="name">Поле "Имя" не может быть пустым.</p>
                
                <Input 
                    value={number}
                    onChange={(e)=>setNumber(e.target.value)}
                    placeholder="Номер"
                />
                <p className="set_error" id="number">Поле "Номер" не может быть пустым.</p>
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