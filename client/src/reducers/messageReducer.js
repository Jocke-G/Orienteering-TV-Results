//import { MESSAGES_RECEIVED } from "../actions/actionTypes";

const initialState = {
  messages: [],
}

export default (state = initialState, action) => {
  switch (action.type) {
//    case MESSAGES_RECEIVED:
//      return {
//          ...state,
//          messages: action.messages
//      };
    default:
      return state
  }
}

export const getMessages = state => state.messages;
