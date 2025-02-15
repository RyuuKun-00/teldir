
import Card from "antd/es/card/Card"
import { ContactTitle } from "./ContactTitle";
import { Button } from "antd";
import {FormOutlined , DeleteOutlined} from '@ant-design/icons';
import { Variants } from "antd/es/config-provider";

interface Props{
    contacts: Contact[];
    handleDelete:(id:string)=>void;
    handleUpdate:(contact:Contact)=>void;
}


export const Contacts = ({contacts,handleDelete,handleUpdate}:Props) =>{
    return (
        <div className="cards" >
            {contacts.map((contact)=>{
                return (
                    <Card 
                        key={contact.id} 
                        title = {<ContactTitle name={contact.name} number={contact.number}/>}
                        variant={"outlined"}
                        style={{marginBottom: "15px"}}
                        actions={[
                            
                        ]}
                    >
                        <p className="card__description">{contact.description}</p>
                        <div className="card__buttons">
                            <Button onClick={()=>handleUpdate(contact)}
                                style={{flex:1, padding: "0px 30px 0px 10px"}}
                                icon ={<FormOutlined style={{ fontSize: 20}}/>}
                                type="link"
                                size="large"
                                >
                            </Button>
                            <Button onClick={()=>handleDelete(contact.id)}
                                danger
                                style={{flex:1,padding: "0px 30px 0px 30px"}}
                                icon ={<DeleteOutlined style={{fontSize: 20}}/>}
                                type="link"
                                size="large"
                                >
                            </Button>
                        </div>
                    </Card>
                )
            })} 
        </div>
    );
}