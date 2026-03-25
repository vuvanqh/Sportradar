import EventPreviewCard from "../components/EventPreviewCard";
import useEvents from "../customHooks/useEvents";
import { useContext } from "react";
import { FilterContext } from "../store/FilterContext";
// type eventPageProps = {
//     events: Event[]
// }

// export const mockEvents: Event[] = [
//   {
//     eventId: "1",
//     title: "Barcelona vs Real Madrid",
//     eventType: "Team",
//     status: "Upcoming",
//     sportName: "Football",
//     startTime: "2026-03-25T18:00:00Z",
//     endTime: "2026-03-25T20:00:00Z",
//     location: {
//       venue: "Camp Nou",
//       city: "Barcelona",
//       country: "Spain"
//     },
//     homeTeamId: "t1",
//     homeTeamName: "Barcelona",
//     awayTeamId: "t2",
//     awayTeamName: "Real Madrid",
//     result: undefined
//   },
//   {
//     eventId: "2",
//     title: "Djokovic vs Nadal",
//     eventType: "OneOnOne",
//     status: "Ongoing",
//     sportName: "Tennis",
//     startTime: "2026-03-24T14:00:00Z",
//     endTime: "2026-03-24T16:00:00Z",
//     location: {
//       venue: "Court 1",
//       city: "Paris",
//       country: "France"
//     },
//     homePlayerId: "p1",
//     homePlayerFirstName: "Novak",
//     homePlayerLastName: "Djokovic",
//     awayPlayerId: "p2",
//     awayPlayerFirstName: "Rafael",
//     awayPlayerLastName: "Nadal",
//     result: {
//       resultId: "r1",
//       eventTitle: "Djokovic vs Nadal",
//       resultType: "OneOnOne",
//       homePlayerId: "p1",
//       awayPlayerId: "p2",
//       homePlayerFirstName: "Novak",
//       homePlayerLastName: "Djokovic",
//       awayPlayerFirstName: "Rafael",
//       awayPlayerLastName: "Nadal",
//       homePlayerScore: 2,
//       awayPlayerScore: 1
//     }
//   },
//   {
//     eventId: "3",
//     title: "Chess Open",
//     eventType: "FreeForAll",
//     status: "Completed",
//     sportName: "Chess",
//     startTime: "2026-03-20T10:00:00Z",
//     endTime: "2026-03-20T18:00:00Z",
//     location: {
//       venue: "Main Hall",
//       city: "Warsaw",
//       country: "Poland"
//     },
//     participants: [],
//     numberOfParticipants: 12,
//     result: {
//       resultId: "r2",
//       eventTitle: "Chess Open",
//       resultType: "FreeForAll",
//       results: [],
//       numberOfParticipants: 12
//     }
//   },
//   {
//     eventId: "4",
//     title: "Chess Open",
//     eventType: "FreeForAll",
//     status: "Completed",
//     sportName: "Chess",
//     startTime: "2026-03-20T10:00:00Z",
//     endTime: "2026-03-20T18:00:00Z",
//     location: {
//       venue: "Main Hall",
//       city: "Warsaw",
//       country: "Poland"
//     },
//     participants: [],
//     numberOfParticipants: 12,
//     result: {
//       resultId: "r2",
//       eventTitle: "Chess Open",
//       resultType: "FreeForAll",
//       results: [],
//       numberOfParticipants: 12
//     }
//   }
// ];
const statusOrder: Record<string, number> = {
  Ongoing: 0,
  Upcoming: 1,
  Completed: 2
};

export default function EventPage(){
    const {events, isLoading} = useEvents();
    const {filter, typeFilter} = useContext(FilterContext);
    if(isLoading) return;
    let filteredEvents = events.filter(e => e.title.toLowerCase().includes(filter.toLowerCase()))
        .filter(e => typeFilter === "" ? true : e.eventType === typeFilter)
        .sort((a, b) => statusOrder[a.status] - statusOrder[b.status]);
    return <div className="event-grid">
            {filteredEvents.map(e=><EventPreviewCard event={e} key={e.eventId}/>)}
    </div>
}