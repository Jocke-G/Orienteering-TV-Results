import { ThunkAction, ThunkDispatch } from 'redux-thunk';
import { RootState } from "../../reducers/rootReducer";
import { Layout } from './reducers';
import { getConfiguration } from '../configuration/reducers';

export const REQUESTING_LAYOUTS = 'REQUESTING_LAYOUTS';
export const LAYOUTS_RECEIVED = 'LAYOUTS_RECEIVED';
export const REQUEST_LAYOUTS_ERROR = 'REQUEST_LAYOUTS_ERROR';

export const REQUESTING_LAYOUT = 'REQUESTING_LAYOUT';
export const LAYOUT_RECEIVED = 'LAYOUT_RECEIVED';
export const REQUEST_LAYOUT_ERROR = 'REQUEST_LAYOUT_ERROR';

export const UPDATING_LAYOUT = 'UPDATING_LAYOUT';
export const LAYOUT_UPDATED = 'LAYOUT_UPDATED';
export const UPDATE_LAYOUT_ERROR = 'UPDATE_LAYOUT_ERROR';

export interface requestingLayoutsAction {
  type: typeof REQUESTING_LAYOUTS,
}

export interface layoutsReceivedAction {
  type: typeof LAYOUTS_RECEIVED,
  layouts: Layout[],
}

export interface requestLayoutsErrorAction {
  type: typeof REQUEST_LAYOUTS_ERROR,
  error: Error,
}


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


export interface updatingLayoutAction {
  type: typeof UPDATING_LAYOUT,
}

export interface layoutUpdatedAction {
  type: typeof LAYOUT_UPDATED,
  layout: Layout,
}

export interface updateLayoutErrorAction {
  type: typeof UPDATE_LAYOUT_ERROR,
  error: Error,
}


export type Action = requestingLayoutsAction | layoutsReceivedAction | requestLayoutsErrorAction
  | requestingLayoutAction | layoutReceivedAction | requestLayoutErrorAction
  | updatingLayoutAction | layoutUpdatedAction | updateLayoutErrorAction;


  export const requestLayouts = (): ThunkAction<Promise<void>, RootState, {}, Action> => (dispatch: ThunkDispatch<RootState, {}, Action>, getState:any): any => {
  const state:RootState = getState();

  const conf = getConfiguration(state);
  const url = `http://${conf.layouts_rest_host}:${conf.layouts_rest_port}/layouts`;
  dispatch(requestingLayouts());
  fetch(url, {
      method: "GET",
      headers: {
        'Accept': 'application/json',
      },
    })
    .then(r => r.text())
    .then(resp => {
        let obj: Layout[] = JSON.parse(resp);
        dispatch(layoutsReceived(obj))
    })
    .catch((error:Error) => dispatch(requestLayoutsError(error)) )
}

export function requestingLayouts(): requestingLayoutsAction {
  return {
    type: REQUESTING_LAYOUTS,
  };
}

export function layoutsReceived(layouts: Layout[]): layoutsReceivedAction {
  return {
    type: LAYOUTS_RECEIVED,
    layouts: layouts,
  };
}

export function requestLayoutsError(error: Error): requestLayoutsErrorAction {
  return{
    type: REQUEST_LAYOUTS_ERROR,
    error: error,
  }
}


export const requestLayout = (layoutName:String): ThunkAction<Promise<void>, RootState, {}, Action> => (dispatch: ThunkDispatch<RootState, {}, Action>, getState:any): any => {
  const state:RootState = getState();

  const conf = getConfiguration(state);
  const url = `http://${conf.layouts_rest_host}:${conf.layouts_rest_port}/layouts/${layoutName}`;
  dispatch(requestingLayout());
  fetch(url, {
      method: "GET",
      headers: {
        'Accept': 'application/json',
      },
    })
    .then(r => r.text())
    .then(resp => {
        let obj: Layout = JSON.parse(resp);
        dispatch(layoutReceived(obj))
    })
    .catch((error:Error) => dispatch(requestLayoutError(error)) )
}

export function requestingLayout(): requestingLayoutAction {
  return {
    type: REQUESTING_LAYOUT,
  };
}

export function layoutReceived(layout: Layout): layoutReceivedAction {
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


export const updateLayout = (layout:Layout): ThunkAction<Promise<void>, RootState, {}, Action> => (dispatch: ThunkDispatch<RootState, {}, Action>, getState:any): any => {
  const state:RootState = getState();

  const conf = getConfiguration(state);
  const url = `http://${conf.layouts_rest_host}:${conf.layouts_rest_port}/layouts/${layout.Name}`;
  dispatch(updatingLayout());
  fetch(url, {
      method: "PUT",
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(layout),
    })
    .then(r => r.text())
    .then(resp => {
        let obj: Layout = JSON.parse(resp);
        dispatch(layoutUpdated(obj))
    })
    .catch((error:Error) => dispatch(updateLayoutError(error)) )
}

export function updatingLayout(): updatingLayoutAction {
  return {
    type: UPDATING_LAYOUT,
  };
}

export function layoutUpdated(layout: Layout): layoutUpdatedAction {
  return {
    type: LAYOUT_UPDATED,
    layout: layout,
  };
}

export function updateLayoutError(error: Error): updateLayoutErrorAction {
  return{
    type: UPDATE_LAYOUT_ERROR,
    error: error,
  }
}
