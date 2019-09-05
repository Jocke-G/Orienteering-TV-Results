import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import { composeWithDevTools } from 'redux-devtools-extension';

import rootReducer from './reducers/rootReducer';
import {reduxMqttMiddleware} from './middleware/mqttMiddleware';

export type AppState = ReturnType<typeof rootReducer>;

export default function configureStore(initialState={}) {
  const middlewares = [
    thunk,
    reduxMqttMiddleware()
  ];

  return createStore(
    rootReducer,
    initialState,
    composeWithDevTools(
      applyMiddleware(...middlewares)
    )
  );
}
