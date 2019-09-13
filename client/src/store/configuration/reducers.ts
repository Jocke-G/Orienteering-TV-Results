import { combineReducers } from 'redux'
import { Action, CONFIGURATION_RECEIVED } from "./actions"

export interface Configuration {
  mqtt_host: string;
  mqtt_port: number;
  rest_host: string;
  rest_port: number;
}

export interface State {
  configuration: Configuration
}

const initialState = {
  mqtt_host : "",
  mqtt_port : 0,
  rest_host : "",
  rest_port : 0
}

const configuration = (state : Configuration = initialState, action: Action): Configuration => {
  switch (action.type) {
    case CONFIGURATION_RECEIVED:
      return {
        ...state,
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
