import Navbar from "../components/UI/Navbar";
import { Outlet } from "react-router-dom";
import Sidebar from "../components/UI/Sidebar";
import { useState } from "react";

export default function MainPage(){
    const [sidebarOpen, setSidebarOpen] = useState(false);

    return <>
        <Navbar onSidebarOpen={() => setSidebarOpen(prev => !prev)}/>
        <Sidebar open={sidebarOpen} onClose={()=>setSidebarOpen(false)}/>
        <Outlet/>
    </>
}
