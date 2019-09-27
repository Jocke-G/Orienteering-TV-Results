import { ThunkAction, ThunkDispatch } from 'redux-thunk';
import { RootState } from "../../reducers/rootReducer";
import { Layout } from './reducers';

export const REQUESTING_LAYOUT = 'REQUESTING_LAYOUT';
export const LAYOUT_RECEIVED = 'LAYOUT_RECEIVED';
export const REQUEST_LAYOUT_ERROR = 'REQUEST_LAYOUT_ERROR';

export interface requestingLayoutAction {
  type: typeof REQUESTING_LAYOUT,
}

export interface layoutReceivedAction {
  type: typeof LAYOUT_RECEIVED,
  layout: Layout,
}

export interface requestLayoutErrorAction {
  type: typeof REQUEST_LAYOUT_ERROR,
  error: Error,
}

export type Action = requestingLayoutAction | layoutReceivedAction | requestLayoutErrorAction;

export const fetchLayout = (layoutName:String): ThunkAction<Promise<void>, RootState, {}, Action> => (dispatch: ThunkDispatch<RootState, {}, Action>, getState:any): any => {

  let url = `/api/layouts/${layoutName}.json`;
  dispatch(requestingLayout());
  fetch(url, {
      method: "GET",
      headers: {}
    })
    .then(r => r.text())
    .then(resp => {
        let obj: Layout = JSON.parse(resp);
        dispatch(layoutReceived(obj))
    })
    .catch((error:Error) => dispatch(requestLayoutError(error)) )
}

export function requestingLayout() : requestingLayoutAction {
  return {
    type: REQUESTING_LAYOUT,
  };
}

export function layoutReceived(layout: Layout) : layoutReceivedAction {
  return {
    type: LAYOUT_RECEIVED,
    layout: layout,
  };
}

export function requestLayoutError(error: Error): requestLayoutErrorAction {
  return{
    type: REQUEST_LAYOUT_ERROR,
    error: error,
  }
}
