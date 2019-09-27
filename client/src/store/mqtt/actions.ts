import { Message } from "paho-mqtt";

export const SET_MQTT_STATUS = 'SET_MQTT_STATUS';
export const MESSAGE_RECEIVED = 'MESSAGE_RECEIVED';
export const PUBLISH_MQTT = 'PUBLISH_MQTT';

export interface setMqttStatusAction {
  type: typeof SET_MQTT_STATUS,
  status: string,
}

export interface reportMessageReceivedAction {
  type: typeof MESSAGE_RECEIVED,
  message: Message,
}

export interface publishMessageAction {
  type: typeof PUBLISH_MQTT,
  topic: string,
  message: string,
}

export type Action = setMqttStatusAction | reportMessageReceivedAction | publishMessageAction;

export function setMqttStatus(status:string): setMqttStatusAction {
  return {
    type: SET_MQTT_STATUS,
    status: status,
  };
}

export function reportMessageReceived(message: Message): reportMessageReceivedAction {
  return {
    type: MESSAGE_RECEIVED,
    message: message,
  }
}

export function trigResendResults(): publishMessageAction {
  return publishMessage("results/resend", "");
}

export function publishMessage(topic:string, message:string): publishMessageAction {
  return {
    type: PUBLISH_MQTT,
    topic: topic,
    message: message,
  }
}
