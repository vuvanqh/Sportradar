import type { newLocation } from "./locationTypes";

type CreateEventBase = {
  title: string;
  locationId?: string;
  newLocation?: newLocation;
  startTime: string;
  endTime: string;
  sportId: string;
  eventType: "Team" | "OneOnOne" | "FreeForAll";

  description?: string;
  competitionId?: string;
};

export type CreateTeamEvent = CreateEventBase & {
    eventType: "Team";
    homeTeamId: string;
    awayTeamId: string;
}

export type CreateOneOnOneEvent = CreateEventBase & {
    eventType: "OneOnOne";
    homePlayerId: string;
    awayPlayerId: string;
}

export type CreateFreeForAllEvent = CreateEventBase & {
    eventType: "FreeForAll";
    participantIds: string[]
}

export type CreateEvent = CreateTeamEvent | CreateFreeForAllEvent | CreateOneOnOneEvent