import { ThunkAction, ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';
import { Configuration } from "./reducers";

export const CONFIGURATION_RECEIVED = 'CONFIGURATION_RECEIVED';
export const REQUEST_CONFIGURATION = 'REQUEST_CONFIGURATION';

export interface configurationReceivedAction {
  type: typeof CONFIGURATION_RECEIVED,
  configuration: Configuration
}

export interface requestConfigurationAction {
  type: typeof REQUEST_CONFIGURATION,
}

// Union Action Types
export type Action = configurationReceivedAction | requestConfigurationAction;

export function configurationReceived(configuration: Configuration) : configurationReceivedAction {
  return {
    type: CONFIGURATION_RECEIVED,
    configuration: configuration
  };
}

export const requestConfiguration = (): ThunkAction<Promise<void>, {}, {}, AnyAction> => (dispatch: ThunkDispatch<{}, {}, AnyAction>): any => {
  return new Promise((resolve, reject) => {
    console.log("Thunk: requestConfiguration");
    fetch('/config.json')
      .then(response => {
        if (!response.ok) {
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
