import { createContext, useState, type ReactNode} from "react";

type ContextType = {
    filter: string
    typeFilter: "Team" | "OneOnOne" | "FreeForAll"|""
    setType: (type:"Team" | "OneOnOne" | "FreeForAll"|"") => void
    setFilter: (flt: string) => void
}

export const FilterContext = createContext<ContextType>(null!);


export default function FilterContextProvider({children}:{children:ReactNode}){
    const [filter, setFilter] = useState("");
    const [typeFilter, setType] = useState<"Team" | "OneOnOne" | "FreeForAll"|"">("");
    const ctxValue: ContextType = {
        filter,
        typeFilter,
        setType: (type:"Team" | "OneOnOne" | "FreeForAll"|"") => setType(type),
        setFilter: (flt: string) => setFilter(flt)
    }
    return <FilterContext value={ctxValue}>
        {children}
    </FilterContext>
}