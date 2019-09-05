import { SEND_MESSAGE } from "./actionTypes";

export function sendMessage(newMessage: string) {
  return {
    type: SEND_MESSAGE,
    message: newMessage
  };
}
