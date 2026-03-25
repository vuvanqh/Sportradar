import { createPortal } from "react-dom";
import { useRef, useEffect } from "react";

type ModalProps = {
  children: React.ReactNode;
  open: boolean;
  onClose: ()=>void;
};

export default function Modal({children, open, onClose} :ModalProps)
{
    const dialog = useRef<HTMLDialogElement|null>(null);

    useEffect(() => {
        const current = dialog.current;
        if (!current) return;
        if (open && !current.open) 
            current.showModal();
        
        if (!open && current.open) {
            current.close();
            onClose();
        }
        
        const handleClose = () => {
            current.close();
            onClose();
        };

       current.addEventListener("close", handleClose);

        return () => current.removeEventListener("close", handleClose);
    }, [open]);

    return createPortal(
    <dialog ref={dialog}>
        {children}
    </dialog>, document.getElementById("modal")!)
}