import type {Event} from "../types/eventTypes";

type eventPreviewCardProps = {
    event: Event
}

export default function EventPreviewCard({event}: eventPreviewCardProps){
    const loc = event.location
    const start = new Date(event.startTime);
    const end = new Date(event.endTime);
    const date = start.toDateString() === end.toDateString()? start.toLocaleDateString()
        : `${start.toLocaleDateString()} - ${end.toLocaleDateString()}`;

    const time = `${start.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" })} - 
                ${end.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" })}`;
                
    let content;
    switch(event.eventType){
        case "Team": 
            content = <p>{event.homeTeamName} vs {event.awayTeamName}</p>;
            break;          
        case "OneOnOne": 
            content = <p>{event.homePlayerFirstName} {event.homePlayerLastName} vs {event.awayPlayerFirstName} {event.awayPlayerLastName}</p>;
            break;
        case "FreeForAll": 
            content = <p>Number of Participants: {event.numberOfParticipants}</p>;
            break;
        default: 
            content = <p>Error</p>; 
            break;
    }
    return <article className="event-preview-card">
        <div className="event-header">
            <h3>{event.title}</h3>
            <div className="badges">
                <span  className={`status ${event.status.toLowerCase()}`}>{event.status}</span>
                <span className="sport">{event.sportName}</span>
            </div>
        </div>
        <hr/>

        <div className="event-body">
            <div className="event-meta">
                <p className="datetime">{date} • {time}</p>
                <p className="location">{loc.country}, {loc.city}, {loc.venue ?? ""}</p>
            </div>
            <div className="event-content">{content}</div>
        </div>
    </article>
}

//  return <div> - small card i mean not too small i want 4 to fit in  a page on a desktop and 3 if using ipad and 1 if using mobile phone
//         <div>  - vertical alignment very very very light blue background
//             <header>{event.title}</header> - quite big i suppose on the left
//             <p className={event.status}>{event.status}</p> - small centered depending on teh status - Upcoming = blue ongoing = green completed = orange/red make those the background but like pastel colour and more intense but not too intense round border and text colour 
//         </div>

    //line divider

//         <div>
//             <div>
//                 <div> 
//                     <p>{event.startTime}</p> - <p>{event.endTime}</p> - and how do i extract date from datetime
//                 </div>
//                 <div> - smaller than the above also how do i extract the time from a date time
//                     <p>{event.startTime}</p> - <p>{event.endTime}</p>
//                 </div>
//                 <div> - centraliezd a bit bigger than the 2nd div
//                     {loc.country}, {loc.city}, {loc.venue??""}
//                 </div>
//             </div>

//             <div> - idk tell me how youd style it
//                 {content}
//             </div>
//         </div>
//     </div>
// }