import { RootState } from '../../reducers/rootReducer';
import { Action, CLASSES_RECEIVED, REQUEST_CLASSES_ERROR, REQUESTING_CLASSES } from '../classes/actions';

export interface Class {
  Id: number,
  ShortName: string,
}

export interface State {
  classes?: Class[],
  requesting: boolean;
  error: Error|null;
}

const initialState: State = {
  requesting: false,
  error: null,
}

const classes = (state : State = initialState, action: Action): State => {
  switch (action.type) {
    case REQUESTING_CLASSES:
      return {
        ...state,
        requesting: true,
        error: null,
      }
    case CLASSES_RECEIVED:
      return {
        ...state,
        requesting: false,
        classes: action.classes,
      }
    case REQUEST_CLASSES_ERROR:
      return {
        ...state,
        error: action.error,
      }
    default:
      return state
  }
}

export default classes;

export const isFetching = (state:RootState):boolean => state.classes.requesting;
export const getClasses = (state:RootState):Class[]|undefined => state.classes.classes;
export const getError = (state:RootState):Error|null => state.classes.error;
