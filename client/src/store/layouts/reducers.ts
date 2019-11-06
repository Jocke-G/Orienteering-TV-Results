import { RootState } from '../../reducers/rootReducer';
import { Action,
  REQUESTING_LAYOUTS, LAYOUTS_RECEIVED, REQUEST_LAYOUTS_ERROR,
  REQUESTING_LAYOUT, LAYOUT_RECEIVED, REQUEST_LAYOUT_ERROR,
  UPDATING_LAYOUT, LAYOUT_UPDATED, UPDATE_LAYOUT_ERROR,
} from './actions';

export interface Layout {
  Name: string,
  Rows: LayoutRow[],
}

export interface LayoutRow {
  Cells: LayoutCell[],
}

export interface LayoutCell {
  CellType: string,
  ClassName: string,
  Options?: LayoutCellOptions;
}

export interface LayoutCellOptions {
  ShowStartTime: boolean,
}

export interface State {
  layouts?: Layout[],
  requestingLayouts: boolean,
  requestLayoutsError: Error|null;

  layout?: Layout,
  requestingLayout: boolean;
  requestLayoutError: Error|null;

  updatingLayout: boolean;
  updateLayoutError: Error|null;
}

const initialState: State = {
  requestingLayouts: false,
  requestLayoutsError: null,
  requestingLayout: false,
  requestLayoutError: null,
  updatingLayout: false,
  updateLayoutError: null,
}

const layout = (state : State = initialState, action: Action): State => {
  switch (action.type) {
    case REQUESTING_LAYOUTS:
      return {
        ...state,
        requestingLayouts: true,
        requestLayoutsError: null,
      }
    case LAYOUTS_RECEIVED:
      return {
        ...state,
        requestingLayouts: false,
        layouts: action.layouts,
        requestLayoutsError: null,
      }
    case REQUEST_LAYOUTS_ERROR:
      return {
        ...state,
        requestingLayouts: false,
        requestLayoutsError: action.error,
      }

    case REQUESTING_LAYOUT:
      return {
        ...state,
        requestingLayout: true,
        requestLayoutError: null,
      }
    case LAYOUT_RECEIVED:
      return {
        ...state,
        requestingLayout: false,
        layout: action.layout,
        requestLayoutError: null,
      }
    case REQUEST_LAYOUT_ERROR:
      return {
        ...state,
        requestingLayout: false,
        requestLayoutError: action.error,
      }

    case UPDATING_LAYOUT:
      return {
        ...state,
        updatingLayout: true,
        updateLayoutError: null,
      }
    case LAYOUT_UPDATED:
      return {
        ...state,
        updatingLayout: false,
        layout: action.layout,
        updateLayoutError: null,
      }
    case UPDATE_LAYOUT_ERROR:
      return {
        ...state,
        updatingLayout: false,
        updateLayoutError: action.error,
      }

    default:
      return state
  }
}

export default layout;

export const isRequestingLayouts = (state:RootState):boolean => state.layout.requestingLayouts;
export const getLayouts = (state:RootState):Layout[]|undefined => state.layout.layouts;
export const getRequestLayoutsError = (state:RootState):Error|null => state.layout.requestLayoutsError;

export const isRequestingLayout = (state:RootState):boolean => state.layout.requestingLayout;
export const getLayout = (state:RootState):Layout|undefined => state.layout.layout;
export const getRequestLayoutError = (state:RootState):Error|null => state.layout.requestLayoutError;

export const isUpdatingLayout = (state:RootState):boolean => state.layout.updatingLayout;
export const getUpdateLayoutError = (state:RootState):Error|null => state.layout.updateLayoutError;
