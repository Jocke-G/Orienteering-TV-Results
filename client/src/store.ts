import { createStore, applyMiddleware, compose } from 'redux';
import thunk from 'redux-thunk';
import rootReducer from './reducers/rootReducer';
//import reduxMqttMiddleware from './mqtt/middleware';

export default function configureStore(initialState={}) {
  const composeEnhancers = window['__REDUX_DEVTOOLS_EXTENSION_COMPOSE__'] as typeof compose || compose;

const middlewares = [
  thunk,
//  reduxMqttMiddleware('ws://localhost:8000/mqtt')
];

 return createStore(
  rootReducer,
  initialState,
  composeEnhancers(
    applyMiddleware(...middlewares)
  )
 );
}
