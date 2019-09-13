import { ThunkAction, ThunkDispatch } from 'redux-thunk';
import { ClassResults } from "./reducers";
import { RootState } from "../../reducers/rootReducer";

export const CLASSES_RECEIVED = 'CLASSES_RECEIVED';
export const CLASS_RESULTS_RECEIVED = 'CLASS_RESULTS_RECEIVED';
export const SELECT_CLASS = 'SELECT_CLASS';


export interface classesReceivedAction {
  type: typeof CLASSES_RECEIVED,
  classes: ClassResults[],
}

export interface classResultsReceivedAction {
  type: typeof CLASS_RESULTS_RECEIVED,
  results: ClassResults,
}

export interface selectClassAction {
  type: typeof SELECT_CLASS,
  className: string,
}

export type Action = classesReceivedAction | classResultsReceivedAction | selectClassAction;

export const fetchClasses = (): ThunkAction<Promise<void>, RootState, {}, Action> => (dispatch: ThunkDispatch<RootState, {}, Action>, getState:any): any => {
  const state:RootState = getState();
  let host = state.configuration.configuration.rest_host;
  let port = state.configuration.configuration.rest_port;
  let classUrl = `http://${host}:${port}/api/classes`;
  console.log(`Fetching classes from ${classUrl}`); 
  fetch(classUrl, {
      method: "GET",
      headers: {}
    })
    .then(r => r.text())
    .then(resp => {
       console.log("Received text: " + resp);
        let obj: ClassResults[] = JSON.parse(resp);
        dispatch(classesReceived(obj))
    })
    .catch((error) => console.error(error) )
}

export function classesReceived(classes: ClassResults[]) : classesReceivedAction {
  return {
    type: CLASSES_RECEIVED,
    classes: classes,
  };
}

export const fetchClass = (className:string): ThunkAction<Promise<void>, RootState, {}, Action> => (dispatch: ThunkDispatch<RootState, {}, Action>, getState:any): any => {
  const state:RootState = getState();
  let host = state.configuration.configuration.rest_host;
  let port = state.configuration.configuration.rest_port;
  let classUrl = `http://${host}:${port}/api/classes/${ className }`;
  console.log(`Fetching initial class from ${classUrl}`); 
  fetch(classUrl, {
      method: "GET",
      headers: {}
    })
    .then(r => r.text())
    .then(resp => {
       console.log("Received text: " + resp);
        let obj: ClassResults = JSON.parse(resp);
        dispatch(classResultsReceived(obj))
    })
    .catch((error) => console.error(error) )
}

export function selectClass(className: string) : selectClassAction {
  return {
    type: SELECT_CLASS,
    className: className,
  };
}

export function classResultsReceived(results: ClassResults) : classResultsReceivedAction {
  return {
    type: CLASS_RESULTS_RECEIVED,
    results: results,
  };
}
