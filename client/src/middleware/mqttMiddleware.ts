import { MiddlewareAPI } from 'redux';
import { Client, Message } from 'paho-mqtt';
import { classResultsReceived } from '../actions/classResultsReceived';
import { ClassResults, SELECT_CLASS } from '../actions/types';
import { selectClass } from '../actions/selectClass';

interface Configuration {
  mqtt_host: string;
  mqtt_port: number;
  mqtt_clientId: string;
}

export const reduxMqttMiddleware = () => ({dispatch}: MiddlewareAPI) => {
  let client :Client;
  let selectedClass: string;

  console.log("Fetching MQTT configuration");
  fetch('/config.json')
    .then(response => {
      if (!response.ok) {
        throw new Error(response.statusText)
      }
      response
        .json()
        .then(data => {
          return data as Configuration;
      })
      .then(conf => {
        console.log(conf)
        console.log("Connecting MQTT");
        createClient(conf);
        connect();
      });
  });

  let createClient = (config:Configuration) => {
    client = new Client(config.mqtt_host, Number(config.mqtt_port), config.mqtt_clientId);

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
    if(client !== undefined) {
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
        case SELECT_CLASS:
          updateSelectedClass(action.className);
          return next(action);
/*        case SEND_MESSAGE:
          client.publish("messages/add", action.message)
          break;*/
        default:
          return next(action)
      }
    } catch(error) {
      console.error(error);
    }
  };
}
