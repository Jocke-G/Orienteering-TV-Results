import { combineReducers } from 'redux';
import { routerReducer as router, RouterState } from "react-router-redux";
import configuration, { State as ConfigurationState } from '../store/configuration/reducers';
import layout, { State as LayoutState } from '../store/layouts/reducers';
import classes, { State as ClassState } from '../store/classes/reducers';
import mqtt, { State as MqttState } from '../store/mqtt/reducers';
import results, { State as ResultsState } from '../store/results/reducers';

export interface RootState {
  router: RouterState;
  configuration: ConfigurationState;
  layout: LayoutState;
  classes: ClassState;
  mqtt: MqttState;
  results: ResultsState;
}

export default combineReducers<RootState>({
  router,
  configuration,
  layout,
  classes,
  mqtt,
  results,
});
