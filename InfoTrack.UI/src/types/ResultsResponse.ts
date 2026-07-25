export interface ResultsResponse<T> {
  count: number;
  results: T[];
  statusCode: number;
  message: string | null;
}
