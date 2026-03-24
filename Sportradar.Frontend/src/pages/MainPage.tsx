import Navbar from "../components/UI/Navbar";
import { Outlet } from "react-router-dom";
import Sidebar from "../components/UI/Sidebar";
import { useState } from "react";

export default function MainPage(){
    const [sidebarOpen, setSidebarOpen] = useState(false);

    return <>
        <Navbar onSidebarOpen={() => setSidebarOpen(prev => !prev)}/>
        <Sidebar open={sidebarOpen} onClose={()=>setSidebarOpen(false)}/>

        <header className="hero">
            <div className="hero-content">
                <h1>Your Fabourite Sport Events</h1>
                <p>Discover competitions, follow matches, and stay updated</p>
                <a href="#content">Learn More</a>
            </div>
        </header>

        <main className="page-content"  id="content">
                <Outlet/>
        </main>

        <footer className="footer">
        <div className="footer-content">
            <p>© {new Date().getFullYear()} All rights reserved.</p>

            <div className="footer-links">
            <a href="#">Privacy</a>
            <a href="#">Terms</a>
            <a href="#">Contact</a>
            </div>
        </div>
        </footer>
    </>
}
