import { combineReducers } from 'redux'
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

export interface State {
  results: ClassResults,
}

const initialState: ClassResults = {
  Id: 0,
  ShortName: "",
  Results: [],
  SplitControls: [],
}

const results = (state : ClassResults = initialState, action: Action): ClassResults => {
  switch (action.type) {
    case CLASS_RESULTS_RECEIVED:
      return {
          ...state,
          Results: action.results.Results,
          ShortName: action.results.ShortName,
          SplitControls: action.results.SplitControls,
        };
        default:
            return state
        }
      }

export default combineReducers<State>({
  results,
});

export const getResults = (state:RootState):ClassResults|undefined => state.results.results;
