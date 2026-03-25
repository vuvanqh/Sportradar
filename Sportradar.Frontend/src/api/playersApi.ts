import { apiClient } from "./axios";

const playerUrl = "/player"

export const getAllPlayers = async () => (await apiClient.get(playerUrl)).data

