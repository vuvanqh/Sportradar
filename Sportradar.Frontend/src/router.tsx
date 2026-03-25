import { createBrowserRouter } from "react-router-dom";
import MainPage from "./pages/MainPage";
import EventPage from "./pages/EventPage";
import ModalContextProvider from "./store/ModalContext";
import FilterContextProvider from "./store/FilterContext";

const router = createBrowserRouter([
    {
        path: "/",
        element: <FilterContextProvider><ModalContextProvider><MainPage/></ModalContextProvider></FilterContextProvider>,
        children: [
        {
            index: true,
            element: <div>Home page</div>
        },
        {
            path: "events",
            element: <EventPage/>
        },
        {
            path: "competitions",
            element: <div>Competitions page</div>
        },
        {
            path: "about-us",
            element: <div>Competitions page</div>
        }
    ]
    }
])

export default router;