import { useQuery } from "@tanstack/react-query";
import { getEvents } from "../api/eventApi";
import type { Event } from "../types/eventTypes";

export default function useEvents(){
    const {data, isLoading} = useQuery<Event[]>({
        queryKey: ["events"],
        queryFn: getEvents
    });

    return {
        events: data??[],
        isLoading
    }
}