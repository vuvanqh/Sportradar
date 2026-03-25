import {  useQuery } from "@tanstack/react-query";
import { getAllCountries, getAllLocations, getContryCities, getVenues } from "../api/locationApi";
import type { location } from "../types/locationTypes";

export function useLocations(){
    const {data, isLoading} = useQuery<location[]>({
        queryKey: ["locations"],
        queryFn: getAllLocations
    });

    return {
        locations: data??[],
        isLoading
    }
}

export function useLocationCountries(){
    const {data, isLoading} = useQuery<string[]>({
        queryKey: ["location","countries"],
        queryFn: getAllCountries
    });

    return {
        countries: data??[],
        isLoading
    }
}

export function useLocationCities(country: string){
    const {data, isLoading} = useQuery<string[]>({
        queryKey: ["location",country , "cities"],
        queryFn: () => getContryCities(country)
    });

    return {
        cities: data??[],
        isLoading
    }
}

export function useVenues(country: string, city: string){
    const {data, isLoading} = useQuery<string[]>({
        queryKey: ["location",country, city, "venues"],
        queryFn: () => getVenues(country, city)
    });

    return {
        cities: data??[],
        isLoading
    }
}