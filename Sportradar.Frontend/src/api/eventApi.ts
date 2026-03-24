import { apiClient } from "./axios";

const eventUrl = "/event"

export const getEvents = async () => (await apiClient.get(eventUrl)).data