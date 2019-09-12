import { CLASS_RESULTS_RECEIVED, classResultsReceivedAction, ClassResults } from "./types";

export function classResultsReceived(results: ClassResults) : classResultsReceivedAction {
  return {
    type: CLASS_RESULTS_RECEIVED,
    results: results
  };
}
