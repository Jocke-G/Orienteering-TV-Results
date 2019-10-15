import { ThunkAction, ThunkDispatch } from 'redux-thunk';
import { Class } from "./reducers";
import { RootState } from "../../reducers/rootReducer";
import { getConfiguration } from '../configuration/reducers';

export const REQUESTING_CLASSES = 'REQUESTING_CLASSES';
export const CLASSES_RECEIVED = 'CLASSES_RECEIVED';
export const REQUEST_CLASSES_ERROR = 'REQUEST_CLASSES_ERROR';

export interface requestingClassesAction {
  type: typeof REQUESTING_CLASSES,
}

export interface classesReceivedAction {
  type: typeof CLASSES_RECEIVED,
  classes: Class[],
}

export interface requestClassesErrorAction {
  type: typeof REQUEST_CLASSES_ERROR,
  error: Error,
}

export type Action = requestingClassesAction | classesReceivedAction | requestClassesErrorAction;

export const fetchClasses = (): ThunkAction<Promise<void>, RootState, {}, Action> => (dispatch: ThunkDispatch<RootState, {}, Action>, getState:any): any => {
  const state:RootState = getState();

  const conf = getConfiguration(state);
  const classUrl = `http://${conf.results_rest_host}:${conf.results_rest_port}/api/classes`;
  dispatch(requestingClasses());
  fetch(classUrl, {
      method: "GET",
      headers: {}
    })
    .then(r => r.text())
    .then(resp => {
        let obj: Class[] = JSON.parse(resp);
        dispatch(classesReceived(obj))
    })
    .catch((error:Error) => dispatch(requestClassesError(error)) )
}

export function requestingClasses() : requestingClassesAction {
  return {
    type: REQUESTING_CLASSES,
  };
}

export function classesReceived(classes: Class[]) : classesReceivedAction {
  return {
    type: CLASSES_RECEIVED,
    classes: classes,
  };
}

export function requestClassesError(error: Error): requestClassesErrorAction {
  return{
    type: REQUEST_CLASSES_ERROR,
    error: error,
  }
}
