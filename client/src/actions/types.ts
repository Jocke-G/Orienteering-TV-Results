export const CLASS_RESULTS_RECEIVED = "CLASS_RESULTS_RECEIVED";

export interface ClassResults {
  Id: number,
  ShortName: string,
  Results: ClassResult[],
}

export interface ClassResult {
  FirstName: string,
  LastName: string,
  Status: string,
  TotalTime: Date,
}

interface classResultsReceivedAction {
  type: typeof CLASS_RESULTS_RECEIVED,
  results: ClassResults
}

export type ResultsActionTypes = classResultsReceivedAction /* | otherAction */
