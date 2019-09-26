import { MiddlewareAPI } from 'redux';
import { Client, Message } from 'paho-mqtt';
import { Configuration } from '../store/configuration/reducers';
import { CONFIGURATION_RECEIVED } from '../store/configuration/actions';
import { ClassResults } from '../store/results/reducers';
import { classResultsReceived, } from '../store/results/actions';
import { selectClass, SELECT_CLASS } from '../store/classes/actions';

export const reduxMqttMiddleware = () => ({dispatch}: MiddlewareAPI) => {
  let client :Client;
  let selectedClass: string;

  let createClient = (config:Configuration) => {
    let clientId = "TV_" + Math.random().toString(16).substr(2, 8);
    console.log(`Creating MQTT client with id: '${ clientId }' to host '${ config.mqtt_host }' with port ${ config.mqtt_port }`)
    client = new Client(config.mqtt_host, Number(config.mqtt_port), clientId);

    client.onMessageArrived = (msg) => {
      try {
        handleMessage(msg)
      } catch (exception) {
        console.log("Failed to handle message " + exception)
      }
    };

    client.onConnectionLost = (error) => {
      console.log("Connection lost, code: '" + error.errorCode + "' message: '" + error.errorMessage + "'");
      setReconnectTimer();
    }  
  }

  let connect = () => {
    console.log("Connecting MQTT");
    client.connect({onSuccess: () => {
      console.log("MQTT Connected");
      startSubscriptions();
    }, onFailure: (error) => {
      console.log("MQTT Connect failed: " + error.errorMessage);
      setReconnectTimer();
    }});
  };

  let startSubscriptions = () => {
    client.subscribe(`Clients/TV1`);

    if(selectedClass !== undefined){
      let topic = `Results/Class/${selectedClass}`;
      console.log(`Subscribing to topic: '${topic}'`)
      client.subscribe(`Results/Class/${selectedClass}`);
    }
  }

  let updateSelectedClass = (className:string) => {
    if(client !== undefined && client.isConnected()) {
      if(selectedClass !== undefined) {
        let oldTopic = `Results/Class/${selectedClass}`;
        console.log(`Unsubscribing to topic: '${oldTopic}'`);
        client.unsubscribe(oldTopic);
      }
      let newTopic = `Results/Class/${className}`;
      console.log(`Subscribing to topic: '${newTopic}'`)
      client.subscribe(newTopic);
    }
    selectedClass = className;
  }

  let setReconnectTimer = () => {
    setTimeout(() => {
      console.log("Reconnecting MQTT...");
      connect();
    }, 5 * 1000)
  }

  let handleMessage = (msg:Message) => {
    var gi = /^Results\/Class\/[^/]+$/gi;
    if (msg.destinationName.match(gi)) {
      let obj: ClassResults = JSON.parse(msg.payloadString);
      dispatch(classResultsReceived(obj))
    } else if(msg.destinationName === `Clients/TV1`) {
      dispatch(selectClass(msg.payloadString));
    } else {
      console.log(`Message received on unhandled topic: ${msg.destinationName}`)
    }
  }

  return (next: (arg0: any) => void) => (action: any) => {
    try {
      switch(action.type) {
        case CONFIGURATION_RECEIVED:
          console.log("MQTT: Conf received");
          createClient(action.configuration);
          connect();
          return next(action);
        case SELECT_CLASS:
            console.log("MQTT: Class selected");
          updateSelectedClass(action.className);
          return next(action);
        default:
          return next(action)
      }
    } catch(error) {
      console.error(error);
    }
  };
}
