export interface ApiErrorObject {
  message: unknown;
  exceptionMessage: string;
  innerExceptionMessage: string;
  code: number | null;
  details: string;
}
