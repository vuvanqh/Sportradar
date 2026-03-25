import type { FreeForAllEvent } from "../../types/eventTypes";
// export type FreeForAllEvent = EventBase & {
//     eventType: "FreeForAll";
//     participants: PlayerPreview[]
//     numberOfParticipants: number;

//     result?: FreeForAllResult
// }

export default function FreeForAllEventDetails({event}:{event:FreeForAllEvent}){
    const sortedResults = event.result ? [...event.result.results].sort((a, b) => b.score - a.score) : [];


    return <div className="ffa-event-details">
        <p className="participants-count">Participants: {event.numberOfParticipants}</p>

        {event.result && <div className="ranking-section">
            <h3>Ranking</h3>
            <ul className="ranking-list ">
                {sortedResults.map((r,i) => <li key={`${r.playerId} - ${i}`} className={`ranking-item rank-${i + 1}`}>
                    <span className="rank">#{i + 1}</span>
                    <span className="name">{r.firstName} {r.lastName}</span>
              
                </li>)}
            </ul>
        </div>}
    </div>
}