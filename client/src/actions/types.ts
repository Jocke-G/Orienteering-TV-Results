export const CLASS_RESULTS_RECEIVED = "CLASS_RESULTS_RECEIVED";
export const SELECT_CLASS = "SELECT_CLASS";

export interface ClassResults {
  Id: number,
  ShortName: string,
  Results: ClassResult[],
}

export interface ClassResult {
  FirstName: string,
  LastName: string,
  Club: string,
  Status: string,
  TotalTime: Date,
  Ordinal: number,
}

export interface classResultsReceivedAction {
  type: typeof CLASS_RESULTS_RECEIVED,
  results: ClassResults,
}

export interface selectClassAction {
  type: typeof SELECT_CLASS,
  className: string,
}

export type ResultsActionTypes = classResultsReceivedAction | selectClassAction
