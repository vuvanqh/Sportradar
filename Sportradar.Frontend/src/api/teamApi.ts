import { apiClient } from "./axios";

const teamUrl = "/team"

export const getAllTeams = async () => (await apiClient.get(teamUrl)).data
export const getTeamById = async (id:string) => (await apiClient.get(teamUrl + `/${id}`)).data
export const getTeamPlayers = async (id: string) => (await apiClient.get(teamUrl + `/${id}/players`)).data
export const getTeamsBySport = async (id:string) => (await apiClient.get(teamUrl + `/sport/${id}`)).data