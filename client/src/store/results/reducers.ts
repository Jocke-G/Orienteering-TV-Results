import { combineReducers } from 'redux'
import { RootState } from '../../reducers/rootReducer';
import { Action, CLASS_RESULTS_RECEIVED, SELECT_CLASS, CLASSES_RECEIVED } from '../results/actions';

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

export interface State {
  results: ClassResults,
  classes: ClassResults[],
  selectedClass: string|null,
}

const initialState: ClassResults = {
  Id: 0,
  ShortName: "",
  Results: [],
}

const results = (state : ClassResults = initialState, action: Action): ClassResults => {
  switch (action.type) {
    case CLASS_RESULTS_RECEIVED:
      return {
          ...state,
          Results: action.results.Results,
          ShortName: action.results.ShortName
        };
        default:
            return state
        }
      }

const selectedClass = (state : string|null = null, action: Action): string|null => {
  switch (action.type) {
    case SELECT_CLASS:
      return action.className;
  default:
      return state
  }
}

const classes = (state: ClassResults[] = [], action: Action) : ClassResults[] => {
  switch (action.type) {
    case CLASSES_RECEIVED:
      return action.classes
    default:
      return state
  }
}

export default combineReducers<State>({
  results,
  selectedClass,
  classes
});

export const getResults = (state:RootState):ClassResults|undefined => state.results.results;
export const getClasses = (state:RootState):ClassResults[] => state.results.classes;
