import type { TeamEvent } from "../../types/eventTypes";
// export type TeamEvent = EventBase & {
//     eventType: "Team";
//     homeTeamId: string;
//     homeTeamName: string;
//     awayTeamId: string;
//     awayTeamName: string;

//     result?: TeamResult
// }

export default function TeamEventDetails({event}:{event:TeamEvent}){
    return <div>
        {event.result?<div>{event.result.homeTeamScore} : {event.result.awayTeamScore}</div>:null}

        <div>
            <p>{event.homeTeamName}</p>
            <ul>
                {event.home}
            </ul>
        </div>
    </div>
}