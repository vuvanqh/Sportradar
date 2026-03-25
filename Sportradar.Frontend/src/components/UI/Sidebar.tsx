import { useState, useEffect } from "react";
import { ModalContext } from "../../store/ModalContext";
import { useContext } from "react";
import { FilterContext } from "../../store/FilterContext";

type sidebarProps = {
    open: boolean,
    onClose: ()=>void
}

export default function Sidebar({ open, onClose }: sidebarProps){
    const [filterType, setFilterType] = useState("title");
    const [menuOpen, setMenuOpen] = useState(false);
    const {openEdit} = useContext(ModalContext);
    const {filter,setFilter, typeFilter, setType} = useContext(FilterContext);

    useEffect(() => {
        const handleKey = (e: KeyboardEvent) => {
            if (e.key === "Escape") 
                onClose();        
        };
        if (open) 
            window.addEventListener("keydown", handleKey);
        
        return () => window.removeEventListener("keydown", handleKey);
    }, [open, onClose]);
    function handleSelect(type: "OneOnOne" | "FreeForAll" | "Team") {
        setMenuOpen(false);
        onClose(); 
        openEdit(type); 
    }

    return <>
        <div className={`sidebar-backdrop ${open ? "visible" : ""}`} onClick={onClose} />

        <aside className={`sidebar ${open ? "open" : ""}`}>
            <button className="add-btn" onClick={()=>setMenuOpen(true)}>+ Add Event</button>
            {menuOpen && (
            <div className="add-menu">
                <button onClick={() => handleSelect("OneOnOne")}>1v1 Event</button>
                <button onClick={() => handleSelect("FreeForAll")}>Free For All</button>
                <button onClick={() => handleSelect("Team")}>Team Event</button>
            </div>
            )}
            <hr className="divider" />

            <div className="filters">
                <input onChange={e => setFilter(e.target.value)}
                    placeholder={`Filter by ${filterType}...`}
                    value={filter}/>

                <select onChange={(e) => setFilterType(e.target.value)}>
                    <option value="title">Title</option>
                    <option value="sport">Sport</option>
                    <option value="country">Country</option>
                    <option value="city">City</option>
                    <option value="competition">Competition</option>
                </select>
            </div>
            <div className="type-filters">
                <label className={typeFilter === "" ? "active" : ""}>
                    <input type="radio" name="filter" value="All" checked={typeFilter === ""} onChange={() => setType("")}/>
                    All
                </label>

                <label className={typeFilter === "OneOnOne" ? "active" : ""}>
                    <input type="radio" name="filter" value="OneOnOne" checked={typeFilter === "OneOnOne"} onChange={()=>setType("OneOnOne")}/>
                    OneOnOne
                </label>

                <label className={typeFilter === "Team" ? "active" : ""}>
                    <input type="radio" name="filter" value="Team" checked={typeFilter === "Team"} onChange={()=>setType("Team")}/>
                    Team
                </label>

                <label className={typeFilter === "FreeForAll" ? "active" : ""}>
                    <input type="radio" name="filter" value="FreeForAll" checked={typeFilter === "FreeForAll"} onChange={()=>setType("FreeForAll")}/>
                    FreeForAll
                </label>
            </div>
        </aside>
    </>
}