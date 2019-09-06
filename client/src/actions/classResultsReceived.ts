import { CLASS_RESULTS_RECEIVED, ClassResults } from "./types";

export function classResultsReceived(results: ClassResults) {
  return {
    type: CLASS_RESULTS_RECEIVED,
    results: results
  };
}
