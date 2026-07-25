import { axiosClient } from "../clients/axiosClient";

import type { ResultsResponse } from "../types/ResultsResponse";
import type { Location } from "../types/Location";
import type { Solicitor } from "../types/Solicitor";

export async function fetchLocations(): Promise<ResultsResponse<Location>> {
  const { data } = await axiosClient.get("/locations");
  return data;
}

export async function fetchSolicitors(location: string): Promise<ResultsResponse<Solicitor>> {
  const { data } = await axiosClient.get("/solicitors", { params: { location } });
  return data;
}