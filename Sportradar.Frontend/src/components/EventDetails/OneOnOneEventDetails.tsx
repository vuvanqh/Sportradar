import type { OneOnOneEvent } from "../../types/eventTypes";
// export type OneOnOneEvent = EventBase & {
//     eventType: "OneOnOne";
//     homePlayerId: string;
//     homePlayerFirstName: string;
//     homePlayerLastName: string;
//     awayPlayerId: string;
//     awayPlayerFirstName: string;
//     awayPlayerLastName: string;

//     result?: OneOnOneResult
// }

export default function OneOnOneEventDetails({event}:{event:OneOnOneEvent}){
    const winner = event.result && (event.result.homePlayerScore > event.result.awayPlayerScore
            ? "home" : event.result.homePlayerScore < event.result.awayPlayerScore ? "away" : null);

    return  <div className="oneonone-event-details">
        {event.result && (
                <div className="team-score">
                    {event.result.homePlayerScore} : {event.result.awayPlayerScore}
                </div>
            )}

        <div className="versus-row">
            <div className={`player ${winner === "home" ? "winner" : ""}`}>
                    <p>{event.homePlayerFirstName}</p>
                    <p>{event.homePlayerLastName}</p>
            </div>
            <div className="vs">VS</div>

            <div className={`player ${winner === "away" ? "winner" : ""}`}>
                <p>{event.awayPlayerFirstName}</p>
                <p>{event.awayPlayerLastName}</p>
            </div>
        </div>
    </div>
}