import { Action, SET_MQTT_STATUS, MESSAGE_RECEIVED } from "./actions"
import { RootState } from "../../reducers/rootReducer"
import { Message } from "paho-mqtt"

export interface State {
  status: string,
  latestMessage?: Message,
}

const initialState: State = {
  status: "",
}

const mqtt = (state : State = initialState, action: Action): State => {
  switch (action.type) {
    case SET_MQTT_STATUS:
      return {
        ...state,
        status: action.status,
      }
    case MESSAGE_RECEIVED:
      return {
        ...state,
        latestMessage: action.message,
      }
    default:
      return state
  }
}

export default mqtt;

export const getStatus = (state:RootState):string => state.mqtt.status;
export const getLatestMessage = (state:RootState):Message|undefined => state.mqtt.latestMessage
