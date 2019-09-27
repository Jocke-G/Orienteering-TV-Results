import { Action, CONFIGURATION_RECEIVED, REQUEST_CONFIGURATION_ERROR, REQUESTING_CONFIGURATION } from "./actions"
import { RootState } from '../../reducers/rootReducer'

export interface Configuration {
  mqtt_host: string;
  mqtt_port: number;
  rest_host: string;
  rest_port: number;
}

export interface State {
  isComplete: boolean;
  requesting: boolean;
  error: Error|null;
  configuration: Configuration|null;
}

const emptyConf: Configuration = {
  mqtt_host: "",
  mqtt_port: 0,
  rest_host: "",
  rest_port: 0,
}

const initialState: State = {
  isComplete: false,
  requesting: false,
  error: null,
  configuration: null,
}

const configuration = (state : State = initialState, action: Action): State => {
  switch (action.type) {
    case REQUESTING_CONFIGURATION:
      return {
        ...state,
        requesting: true,
        error: null,
      }
    case CONFIGURATION_RECEIVED:
      return {
        ...state,
        requesting: false,
        isComplete: true,
        configuration: action.configuration,
      }
    case REQUEST_CONFIGURATION_ERROR:
      return {
        ...state,
        requesting: false,
        error: action.error,
      }
    default:
      return state
  }
}

export default configuration;

export const isFetching = (state:RootState):boolean => state.configuration.requesting;
export const hasConfiguration = (state:RootState):boolean => state.configuration.isComplete;
export const getConfiguration = (state:RootState):Configuration => state.configuration.configuration ? state.configuration.configuration : emptyConf;
export const getError = (state:RootState):Error|null => state.configuration.error;
