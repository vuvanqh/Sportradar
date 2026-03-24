import { createBrowserRouter } from "react-router-dom";
import MainPage from "./pages/MainPage";
import EventPage from "./pages/EventPage";

const router = createBrowserRouter([
    {
        path: "/",
        element: <MainPage></MainPage>,
        children: [
        {
            index: true,
            element: <div>Home page</div>
        },
        {
            path: "events",
            element: <EventPage events={[]}/>
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