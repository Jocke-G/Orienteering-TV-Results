import { ThunkAction, ThunkDispatch } from 'redux-thunk';
import { Class } from "./reducers";
import { RootState } from "../../reducers/rootReducer";
import { getConfiguration } from '../configuration/reducers';

export const REQUESTING_CLASSES = 'REQUESTING_CLASSES';
export const CLASSES_RECEIVED = 'CLASSES_RECEIVED';
export const REQUEST_CLASSES_ERROR = 'REQUEST_CLASSES_ERROR';
export const SELECT_CLASS = 'SELECT_CLASS';

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

export interface selectClassAction {
  type: typeof SELECT_CLASS,
  className: string,
}

export type Action = requestingClassesAction | classesReceivedAction | requestClassesErrorAction |  selectClassAction;

export const fetchClasses = (): ThunkAction<Promise<void>, RootState, {}, Action> => (dispatch: ThunkDispatch<RootState, {}, Action>, getState:any): any => {
  const state:RootState = getState();

  let conf = getConfiguration(state);
  let host = conf.rest_host;
  let port = conf.rest_port;
  let classUrl = `http://${host}:${port}/api/classes`;
  dispatch(requestingClasses());
  fetch(classUrl, {
      method: "GET",
      headers: {}
    })
    .then(r => r.text())
    .then(resp => {
       console.log("Received text: " + resp);
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
  console.log("requestClassesError");
  return{
    type: REQUEST_CLASSES_ERROR,
    error: error,
  }
}

export function selectClass(className: string) : selectClassAction {
  return {
    type: SELECT_CLASS,
    className: className,
  };
}
