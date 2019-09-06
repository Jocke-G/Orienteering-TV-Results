import { CLASS_RESULTS_RECEIVED, ResultsActionTypes, ClassResults } from "../actions/types";

export interface ResultsState {
  classResults?: ClassResults
  ShortName?: string
}

const initialState: ResultsState = {
  classResults: undefined,
  ShortName: undefined,
}

export function resultsReducer (
  state = initialState,
  action: ResultsActionTypes)
  : ResultsState {
  switch (action.type) {
    case CLASS_RESULTS_RECEIVED:
      return {
          ...state,
          classResults: action.results,
          ShortName: action.results.ShortName
      };
    default:
      return state
  }
}

//export const getResults:MyObj = state => state.classResults;
