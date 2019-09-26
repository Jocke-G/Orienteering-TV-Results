import * as actions from './actions'
import { Configuration } from './reducers';

describe('actions', () => {
  it('should create an action to request configuration', () => {
    const expectedAction:actions.requestingConfigurationAction = {
      type: actions.REQUESTING_CONFIGURATION,
    }
    expect(actions.requestingConfiguration()).toEqual(expectedAction);
  })

  it('should create an action to request configuration', () => {
    const text = "Something failed";
    const expectedAction:actions.requestConfigurationErrorAction = {
      type: actions.REQUEST_CONFIGURATION_ERROR,
      error: text,
    }
    expect(actions.requestConfigurationError(text)).toEqual(expectedAction);
  })

  it('should create an action to request configuration', () => {
    const configuration:Configuration = {
      mqtt_host: "testmqtt",
      mqtt_port: 123,
      rest_host: "testrest",
      rest_port: 321,
    };
    const expectedAction:actions.configurationReceivedAction = {
      type: actions.CONFIGURATION_RECEIVED,
      configuration: configuration,
    }
    expect(actions.configurationReceived(configuration)).toEqual(expectedAction);
  })
});
