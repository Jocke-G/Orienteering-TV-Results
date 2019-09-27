import { Action, SET_MQTT_STATUS, MESSAGE_RECEIVED, MQTT_SUBSCRIPTIONS } from "./actions"
import { RootState } from "../../reducers/rootReducer"
import { Message } from "paho-mqtt"

export interface State {
  status: string,
  latestMessage?: Message,
  subscriptions: string[],
}

const initialState: State = {
  status: "",
  subscriptions: [],
}

const mqtt = (state : State = initialState, action: Action): State => {
  switch (action.type) {
    case SET_MQTT_STATUS:
      return {
        ...state,
        status: action.status,
      }
    case MQTT_SUBSCRIPTIONS:
      return {
        ...state,
        subscriptions: action.topics,
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
export const getLatestMessage = (state:RootState):Message|undefined => state.mqtt.latestMessage;
export const getSubscriptions = (state:RootState):string[] => state.mqtt.subscriptions;
