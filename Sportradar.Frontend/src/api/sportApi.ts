import { apiClient } from "./axios";

const sport = "/sport"

export const getSports = async () => (await apiClient.get(sport)).data