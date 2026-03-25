import type { CreateEvent } from "../types/createEventTypes";
import { apiClient } from "./axios";

const eventUrl = "/event"

export const getEvents = async () => (await apiClient.get(eventUrl)).data
export const createEvent = async (event: CreateEvent) => {
    console.log("posting");
    return (await apiClient.post(eventUrl+`/create/${event.eventType}`,event)).data
}


