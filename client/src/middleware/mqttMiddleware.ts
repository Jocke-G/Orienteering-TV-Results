import { MiddlewareAPI, AnyAction, Dispatch } from 'redux';
import { Client, Message } from 'paho-mqtt';
import { Configuration } from '../store/configuration/reducers';
import { CONFIGURATION_RECEIVED } from '../store/configuration/actions';
import { ClassResults } from '../store/results/reducers';
import { classResultsReceived, } from '../store/results/actions';
import { setMqttStatus, reportMessageReceived, PUBLISH_MQTT, SUBSCRIBE_CLASS, mqttSubscriptionTopics, UNSUBSCRIBE_CLASS } from '../store/mqtt/actions';
import { RootState } from '../reducers/rootReducer';
import { getSubscriptions } from '../store/mqtt/reducers';

export const reduxMqttMiddleware = () => ({dispatch, getState }: MiddlewareAPI<Dispatch<AnyAction>, Readonly<RootState>>) => {
  let client :Client;

  let createClient = (config:Configuration) => {
    let clientId = "TV_" + Math.random().toString(16).substr(2, 8);
    client = new Client(config.mqtt_host, Number(config.mqtt_port), clientId);

    client.onMessageArrived = (msg) => {
      try {
        handleMessage(msg)
      } catch (exception) {
        console.log("Failed to handle message " + exception)
      }
    };

    client.onConnectionLost = (error) => {
      setReconnectTimer();
      dispatch(setMqttStatus(`Failed: ${error.errorMessage}`));
    }  
  }

  let connect = () => {
    dispatch(setMqttStatus("Connecting"));
    client.connect({onSuccess: () => {
      dispatch(setMqttStatus("Connected"));
      startSubscriptions();
    }, onFailure: (error) => {
      dispatch(setMqttStatus(`Failed: ${error.errorMessage}`));
      setReconnectTimer();
    }});
  };

  let createClassTopic = (className:string) => {
    return `Results/Class/${className}`;
  }

  let addSubscription = (className:string) => {
    let topic = createClassTopic(className);
    if(client !== undefined && client.isConnected()) {
      client.subscribe(topic);
    }
    let topics = [...getSubscriptions(getState()), topic];
    dispatch(mqttSubscriptionTopics(topics))
  }

  let removeSubscription = (className:string) => {
    let topic = createClassTopic(className);
    if(client !== undefined && client.isConnected()) {
      client.unsubscribe(topic);
    }
    let topics = getSubscriptions(getState()).filter(element => element !== topic);
    dispatch(mqttSubscriptionTopics(topics))
  }

  let startSubscriptions = () => {
    client.subscribe(`Clients/TV1`);
    let topics = getSubscriptions(getState());
    topics.forEach((topic:string) => {
      client.subscribe(topic);
    })
  }

  let setReconnectTimer = () => {
    setTimeout(() => {
      connect();
    }, 5 * 1000)
  }

  let handleMessage = (msg:Message) => {
    dispatch(reportMessageReceived(msg));
    var gi = /^Results\/Class\/[^/]+$/gi;
    if (msg.destinationName.match(gi)) {
      let obj: ClassResults = JSON.parse(msg.payloadString);
      dispatch(classResultsReceived(obj))
    } else {
      console.log(`Message received on unhandled topic: ${msg.destinationName}`)
    }
  }

  return (next: (arg0: any) => void) => (action: any) => {
    try {
      switch(action.type) {
        case CONFIGURATION_RECEIVED:
          createClient(action.configuration);
          connect();
          return next(action);
        case SUBSCRIBE_CLASS:
          addSubscription(action.className);
          return next(action);
        case UNSUBSCRIBE_CLASS:
          removeSubscription(action.className);
          return next(action);
        case PUBLISH_MQTT:
          let message = new Message(action.message);
          message.destinationName = action.topic;
          client.send(message);
          return next(action)
        default:
          return next(action)
      }
    } catch(error) {
      console.error(error);
    }
  };
}
