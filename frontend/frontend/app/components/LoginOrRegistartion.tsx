import Modal from "antd/es/modal/Modal";
import Input from "antd/es/input";
import Form from "antd/es/form";
import {useState} from "react";
/* eslint-disable */
interface Props{
    mode:ModalType;
    isModelopen:boolean;
    handleCancel: ()=>void;
    handleRegister: (email:string,password:string)=>void;
    handleLogin: (email:string,password:string)=>void;
}

export enum ModalType{
    Login,
    Registration
}

const formItemLayout = {
    labelCol: {
      xs: { span: 24 },
      sm: { span: 8 },
    },
    wrapperCol: {
      xs: { span: 24 },
      sm: { span: 16 },
    },
  };

export const LoginOrRegistration = (
    {
        mode,
        isModelopen,
        handleCancel,
        handleLogin,
        handleRegister
    }:Props)=>{

    const [processing,setProcessing] = useState<boolean>(false);
    const [form] = Form.useForm();

    const handleOnOk = ()=>{
        form.submit();
    }

    
    const onFinish =async (values: any) => {
        if(mode == ModalType.Registration){
            await handleRegister(values.email,values.password);
        }else{
            await handleLogin(values.email,values.password);
        }
        setProcessing(false);
        form.resetFields();
        handleCancel();
      };

    return (
        <Modal title={mode === ModalType.Login ? "Вход" : "Регистрация"} 
            open={isModelopen}
            cancelText = "Отмена"
            onOk={()=>{
                if(processing) return;
                setProcessing(true);
                handleOnOk();
            }}
            onCancel={()=>{handleCancel()}}
            closable={false}
            maskClosable={false}
        >
        <Form
        {...formItemLayout}
        form={form}
        name="register"
        onFinish={onFinish}
        initialValues={{ prefix: '86' }}
        style={{}}
        scrollToFirstError
        >
        <Form.Item
            name="email"
            label="Почта"
            rules={[
            {
                type: 'email',
                message: 'Введена некорректная почта!',
            },
            {
                required: true,
                message: 'Пожалуйста, введите почту!',
            },
            ]}
        >
            <Input />
        </Form.Item>

        <Form.Item
            name="password"
            label="Пароль"
            rules={[
            {
                required: true,
                message: 'Пожалуйста, введите пароль!',
            },
            ]}
            hasFeedback
        >
            <Input.Password />
        </Form.Item>
        {mode==ModalType.Registration &&
            <Form.Item
                name="confirm"
                label="Повтоите пароль"
                dependencies={['password']}
                hasFeedback
                rules={[
                {
                    required: true,
                    message: 'Пожалуйста, повторите ввод пароля!',
                },
                ({ getFieldValue }) => ({
                    validator(_, value) {
                    if (!value || getFieldValue('password') === value) {
                        return Promise.resolve();
                    }
                    return Promise.reject(new Error('The new password that you entered do not match!'));
                    },
                }),
                ]}
            >
                <Input.Password />
            </Form.Item>
        }
        </Form>
    </Modal>
    );
};