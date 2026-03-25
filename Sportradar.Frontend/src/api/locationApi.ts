import { apiClient } from "./axios";

const locationUrl = "/location"

export const getAllLocations = async () => (await apiClient.get(locationUrl)).data
export const getAllCountries = async () => (await apiClient.get(locationUrl + "/countries")).data
export const getContryCities = async (country: string) => (await apiClient.get(locationUrl + `/countries/${country}`)).data
export const getVenues = async (country: string, city:string) => (await apiClient.get(locationUrl + `/countries/${country}/${city}`)).data