import { ThunkAction, ThunkDispatch } from 'redux-thunk';
import { Configuration } from "./reducers";
import { RootState } from '../../reducers/rootReducer';

export const REQUESTING_CONFIGURATION = 'REQUESTING_CONFIGURATION';
export const CONFIGURATION_RECEIVED = 'CONFIGURATION_RECEIVED';
export const REQUEST_CONFIGURATION_ERROR = 'REQUEST_CONFIGURATION_ERROR';

export interface requestingConfigurationAction {
  type: typeof REQUESTING_CONFIGURATION,
}

export interface configurationReceivedAction {
  type: typeof CONFIGURATION_RECEIVED,
  configuration: Configuration,
}

export interface requestConfigurationErrorAction {
  type: typeof REQUEST_CONFIGURATION_ERROR,
  error: Error,
}

export type Action = requestingConfigurationAction | configurationReceivedAction | requestConfigurationErrorAction;

export const requestConfiguration = (): ThunkAction<Promise<void>, RootState, {}, Action> => (dispatch: ThunkDispatch<{}, {}, Action>): any => {
  return new Promise((resolve, reject) => {
    console.log("Thunk: requestConfiguration");
    dispatch(requestingConfiguration());
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
          })
          .catch((error:Error) => {
            dispatch(requestConfigurationError(error))
            console.log("Mitt JsonFAil")
          });
    })
    .catch((error:Error) => {
      dispatch(requestConfigurationError(error))
    });
  });
}

export function requestingConfiguration() : requestingConfigurationAction {
  return {
    type: REQUESTING_CONFIGURATION,
  };
}

export function configurationReceived(configuration: Configuration) : configurationReceivedAction {
  return {
    type: CONFIGURATION_RECEIVED,
    configuration: configuration,
  };
}

export function requestConfigurationError(error: Error): requestConfigurationErrorAction {
  return{
    type: REQUEST_CONFIGURATION_ERROR,
    error: error,
  }
}
