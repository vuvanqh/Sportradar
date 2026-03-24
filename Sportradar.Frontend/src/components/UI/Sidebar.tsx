import { useState, useEffect } from "react";

type sidebarProps = {
    open: boolean,
    onClose: ()=>void
}

export default function Sidebar({ open, onClose }: sidebarProps){
    const [filterType, setFilterType] = useState("title");
    const [value, setValue] = useState("");
    
    useEffect(() => {
        const handleKey = (e: KeyboardEvent) => {
            if (e.key === "Escape") 
                onClose();        
        };
        if (open) 
            window.addEventListener("keydown", handleKey);
        
        return () => window.removeEventListener("keydown", handleKey);
    }, [open, onClose]);

    return <>
        <div className={`sidebar-backdrop ${open ? "visible" : ""}`} onClick={onClose} />

        <aside className={`sidebar ${open ? "open" : ""}`}>
            <button className="add-btn">+ Add Event</button>
            
            <hr className="divider" />

            <div className="filters">
                <input onChange={e => setValue(e.target.value)}
                    placeholder={`Filter by ${filterType}...`}
                    value={value}/>

                <select onChange={(e) => setFilterType(e.target.value)}>
                    <option value="title">Title</option>
                    <option value="sport">Sport</option>
                    <option value="country">Country</option>
                    <option value="city">City</option>
                    <option value="competition">Competition</option>
                </select>
            </div>
        </aside>
    </>
}