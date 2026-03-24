import { NavLink } from "react-router-dom"
import { useEffect, useState, useRef } from "react"

type navbarProps = {
    onSidebarOpen: () => void
}

export default function Navbar({onSidebarOpen}: navbarProps){
    const [show,setShow] = useState(true);
     const lastScrollY = useRef(0);

    useEffect(() => {
        const handleScroll = () => {
            const current = window.scrollY;

            setShow(current <= lastScrollY.current);
            lastScrollY.current = current;
        };

        window.addEventListener("scroll", handleScroll);
        return () => window.removeEventListener("scroll", handleScroll);
    }, []);

    function handle() {
        console.log("ok");
        onSidebarOpen();
    }

    return <nav className={`navbar ${show ? "show" : "hide"}`}>
        <div className="left">
            <header className="logo">Sportradar</header>
            <input placeholder="Search for events" className="search"/>
        </div>
        
        <div className="right">
            <NavLink to="/events" className="nav-link">Events</NavLink>
            <NavLink to="/competitions" className="nav-link">Competitions</NavLink>
            <NavLink to="/about-us" className="nav-link">About Us</NavLink>
            <button className="menu-btn" onClick={handle}>☰</button>
        </div>
    </nav>
}
