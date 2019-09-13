import { combineReducers } from 'redux';
import { routerReducer as router, RouterState } from "react-router-redux";
import configuration, { State as ConfigurationState } from '../store/configuration/reducers'
import results, { State as ResultsState } from '../store/results/reducers'

export interface RootState {
  router: RouterState;
  results: ResultsState;
  configuration: ConfigurationState;
}

export default combineReducers<RootState>({
  router,
  results,
  configuration,
});
