import Modal from "./UI/Modal";
import type { Event } from "../types/eventTypes";
import { useState, useEffect } from "react";

type eventModalProps = {
    event: Event
}
export default function EventModal({event}: eventModalProps){
    const [edit,setEdit] = useState(false);

    const start = new Date(event.startTime);
    const end = new Date(event.endTime);
    const date = start.toDateString() === end.toDateString()? start.toLocaleDateString()
        : `${start.toLocaleDateString()} - ${end.toLocaleDateString()}`;

    const time = `${start.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" })} - 
                ${end.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" })}`;

    return <Modal>
        <form >
            <div>
                <img/>
                <div>
                    <p>{event.title}</p>
                    <p>{event.competitionName??""}</p>
                    <div>
                        <div>
                            <span>{event.status}</span>
                            <span>{event.sportName}</span>
                        </div>
                        <button>Edit</button>
                    </div>
                </div>
            </div>

            <hr/>

            <div>
                <div>
                    <p className="datetime">{date} • {time}</p>
                    <p className="location">{event.location.country}, {event.location.city}, {event.location.venue ?? ""}</p>
                </div>
                {event.description??""}
            </div>
        </form>
    </Modal>
}