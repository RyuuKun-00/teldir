
import Card from "antd/es/card/Card"
import Collapse from "antd/es/collapse/Collapse"
import ConfigProvider from "antd/es/config-provider/index"
import { CaretRightOutlined } from '@ant-design/icons';
import { ContactTitle } from "./ContactTitle";
import { Button } from "antd";
import {FormOutlined , DeleteOutlined} from '@ant-design/icons';

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
                    <ConfigProvider
                        key={contact.id} 
                        theme={{
                            components: {
                            Card: {
                                bodyPadding:0
                            },
                            },
                        }}
                    >
                        <Card 
                            key={contact.id} 
                            title = {<ContactTitle name={contact.name} number={contact.number}/>}
                            variant={"outlined"}
                            style={{
                                marginBottom: "15px"
                            }}
                            actions={[
                                
                            ]}
                        >
                            {contact.description!=""&&
                                <Collapse 
                                    style={{
                                        margin:"0px",
                                        padding:"0px"
                                    }}
                                    bordered = {true}
                                    ghost
                                    size="large"
                                    items={
                                        [{ 
                                            label: 'Описание', 
                                            children: <p>{contact.description}</p> }]
                                        }
                                    expandIcon={({ isActive }) => <CaretRightOutlined rotate={isActive ? 90 : 0} />}
                                />
                            }
                            <div className="card__buttons">
                                <Button onClick={()=>handleUpdate(contact)}
                                    style={{flex:1, padding: "10px 30px 10px 35px"}}
                                    icon ={<FormOutlined style={{ fontSize: 20}}/>}
                                    type="link"
                                    size="large"
                                    >
                                </Button>
                                <Button onClick={()=>handleDelete(contact.id)}
                                    danger
                                    style={{flex:1,padding: "10px 30px 10px 30px"}}
                                    icon ={<DeleteOutlined style={{fontSize: 20}}/>}
                                    type="link"
                                    size="large"
                                    >
                                </Button>
                            </div>
                        </Card>
                    </ConfigProvider>
                )
            })} 
        </div>
    );
}