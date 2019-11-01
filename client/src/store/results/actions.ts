import { ThunkAction, ThunkDispatch } from 'redux-thunk';
import { ClassResults, IndependentResult } from "./reducers";
import { RootState } from "../../reducers/rootReducer";
import { getConfiguration } from '../configuration/reducers';

export const CLASS_RESULTS_RECEIVED = 'CLASS_RESULTS_RECEIVED';
export const FINISH_RESULTS_RECEIVED = 'FINISH_RESULTS_RECEIVED';

export interface classResultsReceivedAction {
  type: typeof CLASS_RESULTS_RECEIVED,
  results: ClassResults,
}

export interface finishResultsReceivedAction {
  type: typeof FINISH_RESULTS_RECEIVED,
  results: IndependentResult[],
}

export type Action = classResultsReceivedAction | finishResultsReceivedAction;

export const fetchClass = (className:string): ThunkAction<Promise<void>, RootState, {}, Action> => (dispatch: ThunkDispatch<RootState, {}, Action>, getState:any): any => {
  const state:RootState = getState();

  const conf = getConfiguration(state);
  const classUrl = `http://${conf.results_rest_host}:${conf.results_rest_port}/classes/${ className }`;
  fetch(classUrl, {
      method: "GET",
      headers: {}
    })
    .then(r => r.text())
    .then(resp => {
        let obj: ClassResults = JSON.parse(resp);
        dispatch(classResultsReceived(obj))
    })
    .catch((error) => console.error(error) )
}

export function classResultsReceived(results: ClassResults) : classResultsReceivedAction {
  return {
    type: CLASS_RESULTS_RECEIVED,
    results: results,
  };
}

export const fetchFinish = (limit:number): ThunkAction<Promise<void>, RootState, {}, Action> => (dispatch: ThunkDispatch<RootState, {}, Action>, getState:any): any => {
  const state:RootState = getState();

  const conf = getConfiguration(state);
  const url = `http://${conf.results_rest_host}:${conf.results_rest_port}/finish/${ limit }`;
  fetch(url, {
      method: "GET",
      headers: {}
    })
    .then(r => r.text())
    .then(resp => {
        let obj: IndependentResult[] = JSON.parse(resp);
        dispatch(finishResultsReceived(obj))
    })
    .catch((error) => console.error(error) )
}

export function finishResultsReceived(results: IndependentResult[]) : finishResultsReceivedAction {
  return {
    type: FINISH_RESULTS_RECEIVED,
    results: results,
  };
}
