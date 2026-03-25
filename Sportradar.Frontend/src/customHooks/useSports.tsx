import {  useQuery } from "@tanstack/react-query";
import type { Sport } from "../types/sportType";
import { getSports } from "../api/sportApi";

export default function useSports(){
    const {data, isLoading} = useQuery<Sport[]>({
        queryKey: ["sports"],
        queryFn: getSports
    });

    return {
        sports: data??[],
        isLoading
    }
}