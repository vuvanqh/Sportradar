import type { TeamEvent } from "../../types/eventTypes";
import { useTeamPlayers } from "../../customHooks/useTeamDetails";
// export type TeamEvent = EventBase & {
//     eventType: "Team";
//     homeTeamId: string;
//     homeTeamName: string;
//     awayTeamId: string;
//     awayTeamName: string;

//     result?: TeamResult
// }

export default function TeamEventDetails({event}:{event:TeamEvent}){
    const {teamPlayers: homeTeamPlayers} = useTeamPlayers(event.homeTeamId);
    const {teamPlayers: awayTeamPlayers} = useTeamPlayers(event.awayTeamId);

    return <div className="team-event-details">
        {event.result && (
            <div className="team-score">
                {event.result.homeTeamScore} : {event.result.awayTeamScore}
            </div>)}

        <div className="teams-grid">    
            <div className="team-column">
                <p>{event.homeTeamName}</p>
                <ul>
                    {homeTeamPlayers.map(p => <li key={p.playerId}>
                        <span>{p.firstName} {p.lastName}</span>
                    </li>)}
                </ul>
            </div>
            <div className="team-column">
                <p>{event.awayTeamName}</p>
                <ul>
                    {awayTeamPlayers.map(p => <li key={p.playerId}>
                        <span>{p.firstName} {p.lastName}</span>
                    </li>)}
                </ul>
            </div>
        </div>
    </div>
}