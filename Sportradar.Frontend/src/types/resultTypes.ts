
type ResultBase = {
    resultId: string;
    eventTitle: string;
    resultType: "Team" | "OneOnOne" | "FreeForAll";
}

export type OneOnOneResult = ResultBase & {
    resultType: "OneOnOne";
    homePlayerId: string;
    awayPlayerId: string;
    homePlayerFirstName: string;
    homePlayerLastName: string;
    awayPlayerFirstName: string;
    awayPlayerLastName: string;

    homePlayerScore: number;
    awayPlayerScore: number;
}

export type TeamResult = ResultBase & {
    resultType: "Team";
    homeTeamId: string;
    awayTeamId: string;
    homeTeamName: string;
    awayTeamName: string;
    
    homeTeamScore: number;
    awayTeamScore: number;
}

type FreeForAllResultEntry = {
    playerId: string;
    firstName: string;
    lastName: string;
    score: number;
}

export type FreeForAllResult = ResultBase & {
    resultType: "FreeForAll"
    results: FreeForAllResultEntry[],
    numberOfParticipants: number
}