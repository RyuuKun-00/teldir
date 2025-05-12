
import '@ant-design/v5-patch-for-react-19';
import type { Metadata } from "next";
import "./globals.css";

import Layout, { Content, Footer, Header } from "antd/es/layout/layout";
import { Account } from './components/Account';
import { Menu } from "antd";
import Link from "next/link";
import ReactReduxProvider from "./components/ReactReduxProvider"

export const metadata: Metadata = {
  title: "Telephone Directory",
  description: "Pet-project",
};

const items =[
  {key: "home",label: <Link href={"/"}>Стартовая страница</Link>},
  {key: "contact",label: <Link href={"/contacts"}>Контакты</Link>}
];

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <Layout style={{minHeight: "100vh"}}>
          <ReactReduxProvider>
            <Header style={{ display: 'flex', alignItems: 'center'}}>
              <Menu 
                theme="dark"
                mode="horizontal"
                items={items}
                style={{flex: 1, minWidth: 0}}
              />
            <Account/>
            </Header>
            <Content style={{padding: "0 5vw",display:"flex"}}>{children}</Content>
            <Footer style={{textAlign:"center"}}>{(new Date()).getFullYear()} RyuuKun</Footer>
          </ReactReduxProvider>
        </Layout>
      </body>
    </html>
  );
}
