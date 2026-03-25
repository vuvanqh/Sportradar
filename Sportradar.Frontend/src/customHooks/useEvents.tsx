import { useMutation, useQuery } from "@tanstack/react-query";
import { createEvent as ce, getEvents } from "../api/eventApi";
import type { Event } from "../types/eventTypes";
import queryClient from "../api/queryClient";
import { showSuccess, showError} from "../toastConfig";


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

export function useCreateEvent(){
    const {mutateAsync} = useMutation({
        mutationFn: ce,
        onSuccess: ()=>showSuccess("Successfully created event"),
        onError: ()=>showError("Failed to create event."),
        onSettled: () => queryClient.invalidateQueries({queryKey:["events"]})
    })

    return {
        createEvent: mutateAsync
    }
}