import { CLASS_RESULTS_RECEIVED, ResultsActionTypes, ClassResults, SELECT_CLASS } from "../actions/types";

export interface ResultsState {
  classResults?: ClassResults
  ShortName?: string
  selectedClass?: string
}

const initialState: ResultsState = {
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
    case SELECT_CLASS:
      return {
        ...state,
        selectedClass: action.className
      };
  default:
      return state
  }
}

export const getResults = (state:ResultsState):ClassResults|undefined => state.classResults;
