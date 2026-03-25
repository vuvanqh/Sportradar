import Modal from "./UI/Modal";
import type { Event } from "../types/eventTypes";
import TeamEventDetails from "./EventDetails/TeamEventDetails";
import OneOnOneEventDetails from "./EventDetails/OneOnOneEventDetails";
import type { ComponentType } from "react";
import FreeForAllEventDetails from "./EventDetails/FreeForAllEventDetails";

type eventModalProps = {
    event: Event,
    open: boolean,
    onClose: ()=>void
}
export default function EventModal({event, open = false, onClose}: eventModalProps){
    const start = new Date(event.startTime);
    const end = new Date(event.endTime);
    const date = start.toDateString() === end.toDateString()? start.toLocaleDateString()
        : `${start.toLocaleDateString()} - ${end.toLocaleDateString()}`;

    const time = `${start.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" })} - 
                ${end.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" })}`;

    let Details: ComponentType<any>;
    switch(event.eventType){
        case "Team": Details = TeamEventDetails; break;
        case "OneOnOne": Details = OneOnOneEventDetails; break;
        case "FreeForAll": Details = FreeForAllEventDetails; break;
        default: return null;
    }

    return <Modal open={open} onClose={onClose}>
        <div className="event-modal">
             <div className="event-modal-body">
                <div>
                    <img/>
                    <div className="event-modal-header">
                        <h2>{event.title}</h2>
                        <p className="event-sub">{event.competitionName ?? ""}</p>
                        <div className="badges">
                            <span className={`status ${event.status.toLowerCase()}`}>
                                {event.status}
                            </span>
                            <span className="sport">{event.sportName}</span>
                        </div>
                    </div>
                </div>

                <hr/>

                <div className="event-section">
                    <p className="datetime">{date} • {time}</p>
                    <p className="location">
                        {event.location.country}, {event.location.city}, {event.location.venue ?? ""}
                    </p>
                </div>
                    <div className="event-section">
                    <Details event = {event}/>
                </div>
                    {event.description && (
                    <div className="event-section">
                        <p className="description">{event.description}</p>
                    </div>
                )}
                
            </div>
        </div>
    </Modal>
}