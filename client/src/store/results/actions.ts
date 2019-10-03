import { ThunkAction, ThunkDispatch } from 'redux-thunk';
import { ClassResults } from "./reducers";
import { RootState } from "../../reducers/rootReducer";
import { getConfiguration } from '../configuration/reducers';

export const CLASS_RESULTS_RECEIVED = 'CLASS_RESULTS_RECEIVED';

export interface classResultsReceivedAction {
  type: typeof CLASS_RESULTS_RECEIVED,
  results: ClassResults,
}

export type Action = classResultsReceivedAction;

export const fetchClass = (className:string): ThunkAction<Promise<void>, RootState, {}, Action> => (dispatch: ThunkDispatch<RootState, {}, Action>, getState:any): any => {
  const state:RootState = getState();
  let conf = getConfiguration(state);
  let host = conf.rest_host;
  let port = conf.rest_port;
  let classUrl = `http://${host}:${port}/api/classes/${ className }`;
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
