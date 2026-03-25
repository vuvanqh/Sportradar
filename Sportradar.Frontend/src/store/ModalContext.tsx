import { createContext, useState, type ReactNode} from "react";
import type { Event } from "../types/eventTypes";

type ContextType = {
    view: boolean
    edit: boolean
    event: Event|null
    createType: "FreeForAll" | "Team" | "OneOnOne" | null
    openView: (e: Event) => void
    closeView: () => void
    openEdit: (type: "FreeForAll" | "Team" | "OneOnOne") => void
    closeEdit: () => void
}

export const ModalContext = createContext<ContextType>(null!);


export default function ModalContextProvider({children}:{children:ReactNode}){
    const [view, setView] = useState(false);
    const [edit, setEdit] = useState(false);
    const [event, setEvent] = useState<Event|null>(null);
    const [createType, setCreateType] = useState<"FreeForAll" | "Team" | "OneOnOne" | null>(null);

    const ctxValue: ContextType = {
        view,
        edit,
        event,
        createType,
        openView: (e:Event) => {
            setView(true);
            setEvent(e);
            setEdit(false);
            setCreateType(null);
        },
        closeView: () => setView(false),
        openEdit: (type: "FreeForAll" | "Team" | "OneOnOne") => {
            setView(false);
            setEvent(null);
            setEdit(true);
            setCreateType(type);
        },
        closeEdit: () => {
            setEdit(false)
            setCreateType(null);
        }
    }
    return <ModalContext value={ctxValue}>
        {children}
    </ModalContext>
}