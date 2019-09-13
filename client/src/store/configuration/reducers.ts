import { combineReducers } from 'redux'
import { Action, CONFIGURATION_RECEIVED } from "./actions"
import { RootState } from '../../reducers/rootReducer'

export interface Configuration {
  isComplete: boolean;
  mqtt_host: string;
  mqtt_port: number;
  rest_host: string;
  rest_port: number;
}

export interface State {
  configuration: Configuration,
}

const initialState: Configuration = {
  isComplete: false,
  mqtt_host : "",
  mqtt_port : 0,
  rest_host : "",
  rest_port : 0,
}

const configuration = (state : Configuration = initialState, action: Action): Configuration => {
  switch (action.type) {
    case CONFIGURATION_RECEIVED:
      return {
        ...state,
        isComplete: true,
        mqtt_host: action.configuration.mqtt_host,
        mqtt_port: action.configuration.mqtt_port,
        rest_host: action.configuration.rest_host,
        rest_port: action.configuration.rest_port,
      }
  }

  return state;
}

export default combineReducers<State>({
  configuration
});

export const hasConfiguration = (state:RootState):boolean => state.configuration.configuration.isComplete;
