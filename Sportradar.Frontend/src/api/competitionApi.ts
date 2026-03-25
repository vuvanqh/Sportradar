import { apiClient } from "./axios";

const competitionUrl = "/competition"

export const getAllCompetitions = async () => (await apiClient.get(competitionUrl)).data
export const getAllCompetitionsBySport = async (id: string) => (await apiClient.get(competitionUrl + `/${id}`)).data