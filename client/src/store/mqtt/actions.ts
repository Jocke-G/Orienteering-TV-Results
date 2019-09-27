import { Message } from "paho-mqtt";

export const SET_MQTT_STATUS = 'SET_MQTT_STATUS';
export const MQTT_SUBSCRIPTIONS = 'MQTT_SUBSCRIPTIONS';
export const MESSAGE_RECEIVED = 'MESSAGE_RECEIVED';
export const SUBSCRIBE_CLASS = 'SUBSCRIBE_CLASS';
export const UNSUBSCRIBE_CLASS = 'UNSUBSCRIBE_CLASS';
export const PUBLISH_MQTT = 'PUBLISH_MQTT';

export interface setMqttStatusAction {
  type: typeof SET_MQTT_STATUS,
  status: string,
}

export interface mqttSubscriptionTopicsAction {
  type: typeof MQTT_SUBSCRIPTIONS,
  topics: string[],
}

export interface reportMessageReceivedAction {
  type: typeof MESSAGE_RECEIVED,
  message: Message,
}

export interface subscribeClassAction {
  type: typeof SUBSCRIBE_CLASS,
  className: string,
}

export interface unsubscribeClassAction {
  type: typeof UNSUBSCRIBE_CLASS,
  className: string,
}

export interface publishMessageAction {
  type: typeof PUBLISH_MQTT,
  topic: string,
  message: string,
}

export type Action = setMqttStatusAction | mqttSubscriptionTopicsAction | reportMessageReceivedAction | subscribeClassAction | unsubscribeClassAction | publishMessageAction;

export function setMqttStatus(status:string): setMqttStatusAction {
  return {
    type: SET_MQTT_STATUS,
    status: status,
  };
}

export function mqttSubscriptionTopics(topics:string[]): mqttSubscriptionTopicsAction {
  return {
    type: MQTT_SUBSCRIPTIONS,
    topics: topics,
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

export function subscribeClass(className:string): subscribeClassAction {
  return {
    type: SUBSCRIBE_CLASS,
    className: className,
  }
}

export function unsubscribeClass(className:string): unsubscribeClassAction {
  return {
    type: UNSUBSCRIBE_CLASS,
    className: className,
  }
}

export function publishMessage(topic:string, message:string): publishMessageAction {
  return {
    type: PUBLISH_MQTT,
    topic: topic,
    message: message,
  }
}
