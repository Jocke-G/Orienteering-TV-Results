import { combineReducers } from 'redux';
import { routerReducer as router, RouterState } from "react-router-redux";
import { resultsReducer, ResultsState } from './resultsReducer';

interface StoreEnhancerState {}

export interface RootState extends StoreEnhancerState {
  router: RouterState;
  resultsReducer: ResultsState;
}

export default combineReducers<RootState>({
  router,
  resultsReducer
});
