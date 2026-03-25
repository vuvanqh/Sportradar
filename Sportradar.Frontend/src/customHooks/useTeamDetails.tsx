import { useQuery } from "@tanstack/react-query";
import type { Team } from "../types/teamTypes";
import { getAllTeams, getTeamById, getTeamPlayers, getTeamsBySport } from "../api/teamApi";
import type { PlayerPreview } from "../types/playerTypes";

export function useTeams(){
    const {data, isLoading} = useQuery<Team[]>({
        queryKey: ["teams"],
        queryFn: getAllTeams
    });

    return {
        teams: data??[],
        isLoading
    }
}

export function useTeamPlayers(teamId:string){
    const {data, isLoading} = useQuery<PlayerPreview[]>({
        queryKey: ["teams",teamId, "players"],
        queryFn: () => getTeamPlayers(teamId)
    });

    return {
        teamPlayers: data??[],
        isLoading
    }
}


export function useTeamsBySport(sportId:string){
    const {data, isLoading} = useQuery<PlayerPreview[]>({
        queryKey: ["teams",sportId],
        queryFn: () => getTeamsBySport(sportId)
    });

    return {
        teamsBySport: data??[],
        isLoading
    }
}

export function useTeamDetails(teamId:string){
    const {data, isLoading} = useQuery<PlayerPreview[]>({
        queryKey: ["teams",teamId],
        queryFn: () => getTeamById(teamId)
    });

    return {
        teamDetails: data??[],
        isLoading
    }
}