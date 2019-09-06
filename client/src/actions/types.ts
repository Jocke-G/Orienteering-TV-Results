export const CLASS_RESULTS_RECEIVED = "CLASS_RESULTS_RECEIVED";

export interface ClassResults {
  Id: number;
  ShortName: string,
}

interface classResultsReceivedAction {
  type: typeof CLASS_RESULTS_RECEIVED,
  results: ClassResults
}

export type ResultsActionTypes = classResultsReceivedAction /* | otherAction */
