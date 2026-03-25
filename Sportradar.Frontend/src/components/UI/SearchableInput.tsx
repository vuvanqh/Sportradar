import { useState, useEffect } from "react";

type Props<T> = {
    items: T[];
    getLabel: (item: T) => string;
    getValue: (item: T) => string;
    name: string;
    placeholder?: string;
    disabled?: boolean;
};

export default function SearchSelect<T>({items, getLabel, getValue, name, placeholder = "Search...", disabled=false}: Props<T>) {
    const [query, setQuery] = useState("");
    const [selected, setSelected] = useState<T | null>(null);
    const [open, setOpen] = useState(false);

    const filtered = items.filter(i =>
        getLabel(i).toLowerCase().includes(query.toLowerCase())
    );

    useEffect(() => {
        function handleClick(_: MouseEvent) {
            setOpen(false);
        }
        document.addEventListener("click", handleClick);
        return () => document.removeEventListener("click", handleClick);
    }, []);

    return (
        <div className="search-select"> 
            <input value={query} placeholder={placeholder} onFocus={() => setOpen(true)} onChange={(e) => setQuery(e.target.value)} disabled={disabled}/>
            <input type="hidden" name={name} value={selected ? getValue(selected) : ""} disabled={disabled}/>

            {open && !disabled&& (
                <div className="options">
                    {filtered.slice(0, 10).map(item => (
                        <div key={getValue(item)}
                            onClick={() => {
                                setSelected(item);
                                setQuery(getLabel(item));
                                setOpen(false);
                            }}>
                            {getLabel(item)}
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
}