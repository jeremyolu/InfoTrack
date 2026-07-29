export default interface ResultResponse<T> {
  result: T;
  statusCode: number;
  message: string | null;
}
