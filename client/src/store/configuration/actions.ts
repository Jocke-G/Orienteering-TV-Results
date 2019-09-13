import { ThunkAction, ThunkDispatch } from 'redux-thunk';
import { Configuration } from "./reducers";

export const CONFIGURATION_RECEIVED = 'CONFIGURATION_RECEIVED';
export const REQUEST_CONFIGURATION = 'REQUEST_CONFIGURATION';

export interface requestConfigurationAction {
  type: typeof REQUEST_CONFIGURATION,
}

export interface configurationReceivedAction {
  type: typeof CONFIGURATION_RECEIVED,
  configuration: Configuration
}

export type Action = requestConfigurationAction | configurationReceivedAction;

export const requestConfiguration = (): ThunkAction<Promise<void>, {}, {}, Action> => (dispatch: ThunkDispatch<{}, {}, Action>): any => {
  return new Promise((resolve, reject) => {
    console.log("Thunk: requestConfiguration");
    fetch('/config.json')
      .then(response => {
        if (!response.ok) {
          reject();
          throw new Error(response.statusText)
        }
        response
          .json()
          .then(data => {
            dispatch(configurationReceived(data));
            resolve();
        });
    });
  });
}

export function configurationReceived(configuration: Configuration) : configurationReceivedAction {
  return {
    type: CONFIGURATION_RECEIVED,
    configuration: configuration
  };
}
