import {  useQuery } from "@tanstack/react-query";
import type { Competition } from "../types/competitionTypes";
import { getAllCompetitions } from "../api/competitionApi";

export default function useCompetitions(){
    const {data, isLoading} = useQuery<Competition[]>({
        queryKey: ["competitions"],
        queryFn: getAllCompetitions
    });

    return {
        competitions: data??[],
        isLoading
    }
}