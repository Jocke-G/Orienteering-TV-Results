import { RootState } from '../../reducers/rootReducer';
import { Action, CLASS_RESULTS_RECEIVED, FINISH_RESULTS_RECEIVED, } from '../results/actions';

export interface ClassResults {
  Id: number,
  ShortName: string,
  SplitControls: SplitControl[],
  Results: ClassResult[],
}

export interface SplitControl {
  Name: string,
}

export interface ClassResult {
  FirstName: string,
  LastName: string,
  Club: string,
  StartTime: Date,
  Status: string,
  TotalTime: Date,
  Ordinal: number,
  SplitTimes: SplitTime[],
}

export interface IndependentResult {
  FirstName: string,
  LastName: string,
  Club: string,
  ClassName: string,
  FinishTime: Date,
  Status: string,
  TotalTime: Date,
  Ordinal: number,
}

export interface SplitTime {
  Time: number,
  Ordinal: number,
}

export interface ClassResultDictionary {
  [index:string]: ClassResults
}

export interface State {
  results: ClassResultDictionary,
  finishResults?: IndependentResult[],
}

const initialState:State = {
  results: {},
  finishResults: undefined,
}

const results = (state: State = initialState, action: Action): State => {
  switch (action.type) {
    case CLASS_RESULTS_RECEIVED:
      const results = {...state.results};
      const shortName:string = action.results.ShortName;
      results[shortName] = action.results;
      return {
        ...state,
        results: results,
      };
    case FINISH_RESULTS_RECEIVED:
      return {
        ...state,
        finishResults: action.results,
      }
    default:
      return state
  }
}

export default results;

export const getResults = (state:RootState, className: string):ClassResults|undefined => state.results.results[className];
export const getFinishResults = (state:RootState):IndependentResult[]|undefined => state.results.finishResults;
