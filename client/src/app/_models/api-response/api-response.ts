import { ApiErrorObject } from "./api-error-object";

export interface ApiResponse<T> {
  requestUrl: string;
  result: T;
  error: ApiErrorObject;
  status: boolean;
  statusCode: number;
}
