import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import { composeWithDevTools } from 'redux-devtools-extension';
import { routerMiddleware as createRouterMiddleware } from "react-router-redux";
import { createBrowserHistory } from "history";

import rootReducer from './reducers/rootReducer';
import {reduxMqttMiddleware} from './middleware/mqttMiddleware';

export type AppState = ReturnType<typeof rootReducer>;

export const history = createBrowserHistory();
export const routerMiddleware = createRouterMiddleware(history);

export default function configureStore(initialState={}) {
  const middlewares = [
    thunk,
    reduxMqttMiddleware(),
    routerMiddleware
  ];

  return createStore(
    rootReducer,
    initialState,
    composeWithDevTools(
      applyMiddleware(...middlewares)
    )
  );
}
