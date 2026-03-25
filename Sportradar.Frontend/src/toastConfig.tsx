import { toast, type ToastOptions, Slide } from "react-toastify";

const baseConfig: ToastOptions = {
  position: "top-right",
  autoClose: 2500,
  hideProgressBar: true,
  closeOnClick: true,
  pauseOnHover: true,
  draggable: false,
  theme: "light",
  transition: Slide, 
};

export const showSuccess = (message: string) =>
  toast.success(message, {
    ...baseConfig,
  });

export const showError = (message: string) =>
  toast.error(message, {
    ...baseConfig,
  });

export const showInfo = (message: string) =>
  toast.info(message, {
    ...baseConfig,
  });