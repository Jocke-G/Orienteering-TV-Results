import { Client, Message } from 'paho-mqtt';

interface Configuration {
  mqtt_host: string;
  mqtt_port: number;
  mqtt_clientId: string;
}

export const reduxMqttMiddleware = () => () => {
  let client :Client;

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
      handleMessage(msg)
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
    client.subscribe("messages");
  }

  let setReconnectTimer = () => {
    setTimeout(() => {
      console.log("Reconnecting MQTT...");
      connect();
    }, 5 * 1000)
  }

  let handleMessage = (msg:Message) => {
    switch(msg.destinationName) { 
      case 'messages': {
        console.log(msg.payloadString);
        break;
      }
      default: {
        console.log(`Message received on unhandled topic: ${msg.destinationName}`)
      }
    }
  }

  return (next: (arg0: any) => void) => (action: any) => {
    /*
    try {
      switch(action.type) {
        case REQUEST_MESSAGES:
          client.publish("messages/getAll", "")
          break;
        case SEND_MESSAGE:
          client.publish("messages/add", action.message)
          break;
        default:
          */
         console.log('Nextaction', action);
          return next(action)
          /*
      }
    } catch(error) {
      console.error(error);
    }
    */
  };
}
