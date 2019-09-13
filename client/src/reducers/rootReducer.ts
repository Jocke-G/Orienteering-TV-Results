import { combineReducers } from 'redux';
import { routerReducer as router, RouterState } from "react-router-redux";
import { resultsReducer, ResultsState } from './resultsReducer';
import configuration, { State as ConfigurationState } from '../store/configuration/reducers'

interface StoreEnhancerState {}

export interface RootState extends StoreEnhancerState {
  router: RouterState;
  resultsReducer: ResultsState;
  configuration: ConfigurationState;
}

export default combineReducers<RootState>({
  router,
  resultsReducer,
  configuration,
});
