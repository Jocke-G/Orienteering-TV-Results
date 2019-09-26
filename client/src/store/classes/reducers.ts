import { RootState } from '../../reducers/rootReducer';
import { Action, SELECT_CLASS, CLASSES_RECEIVED, REQUEST_CLASSES_ERROR, REQUESTING_CLASSES } from '../classes/actions';

export interface Class {
  Id: number,
  ShortName: string,
}

export interface State {
  classes?: Class[],
  selectedClass?: string,
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
    case SELECT_CLASS:
      return {
        ...state,
        selectedClass: action.className,
      }
    default:
      return state
  }
}

export default classes;

export const getClasses = (state:RootState):Class[]|undefined => state.classes.classes;
export const getError = (state:RootState):Error|null => state.classes.error;
export const getSelectedClass = (state:RootState):string|undefined => state.classes.selectedClass;
