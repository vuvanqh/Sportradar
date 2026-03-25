import { useQuery } from "@tanstack/react-query";
import type { PlayerPreview } from "../types/playerTypes";
import { getAllPlayers } from "../api/playersApi";

export default function usePlayers(){
    const {data, isLoading} = useQuery<PlayerPreview[]>({
        queryKey: ["players"],
        queryFn: getAllPlayers
    });

    return {
        players: data??[],
        isLoading
    }
}