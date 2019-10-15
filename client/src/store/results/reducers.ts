import { RootState } from '../../reducers/rootReducer';
import { Action, CLASS_RESULTS_RECEIVED, } from '../results/actions';

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

export interface SplitTime {
  Time: number,
  Ordinal: number,
}

export interface ClassResultDictionary {
  [index:string]: ClassResults
}

export interface State {
  results: ClassResultDictionary,
}

const initialState:State = {
  results: {},
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
        default:
            return state
        }
      }

export default results;

export const getResults = (state:RootState, className: string):ClassResults|undefined => state.results.results[className];
