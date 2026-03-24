import type { PlayerPreview } from "./playerTypes";
import type { FreeForAllResult, OneOnOneResult, TeamResult } from "./resultTypes";
import type { location } from "./locationTypes";

type EventBase = {
  eventId: string;
  title: string;
  location: location;
  startTime: string;
  endTime: string;
  status: string;
  sportName: string;
  eventType: "Team" | "OneOnOne" | "FreeForAll";

  description?: string;
  competitionId?: string;
  competitionName?: string;
};

export type TeamEvent = EventBase & {
    eventType: "Team";
    homeTeamId: string;
    homeTeamName: string;
    awayTeamId: string;
    awayTeamName: string;

    result?: TeamResult
}

export type OneOnOneEvent = EventBase & {
    eventType: "OneOnOne";
    homePlayerId: string;
    homePlayerFirstName: string;
    homePlayerLastName: string;
    awayPlayerId: string;
    awayPlayerFirstName: string;
    awayPlayerLastName: string;

    result?: OneOnOneResult
}

export type FreeForAllEvent = EventBase & {
    eventType: "FreeForAll";
    participants: PlayerPreview[]
    numberOfParticipants: number;

    result?: FreeForAllResult
}

export type Event = TeamEvent | FreeForAllEvent | OneOnOneEvent