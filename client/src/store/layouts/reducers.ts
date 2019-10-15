import { RootState } from '../../reducers/rootReducer';
import { Action, REQUESTING_LAYOUT, LAYOUT_RECEIVED, REQUEST_LAYOUT_ERROR } from './actions';

export interface Layout {
  Rows: LayoutRow[],
}

export interface LayoutRow {
  Cells: LayoutCell[],
}

export interface LayoutCell {
  ClassName: string,
}

export interface State {
  layout?: Layout|null,
  requesting: boolean;
  error?: Error|null;
}

const initialState: State = {
  requesting: false,
}

const layout = (state : State = initialState, action: Action): State => {
  switch (action.type) {
    case REQUESTING_LAYOUT:
      return {
        ...state,
        layout: null,
        requesting: true,
        error: null,
      }
    case LAYOUT_RECEIVED:
      return {
        ...state,
        requesting: false,
        layout: action.layout,
      }
    case REQUEST_LAYOUT_ERROR:
      return {
        ...state,
        requesting: false,
        error: action.error,
      }
    default:
      return state
  }
}

export default layout;

export const isFetching = (state:RootState):boolean => state.layout.requesting;
export const getLayout = (state:RootState):Layout|null|undefined => state.layout.layout;
export const getError = (state:RootState):Error|null|undefined => state.layout.error;
