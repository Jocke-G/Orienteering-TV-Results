import { SELECT_CLASS, selectClassAction } from "./types";

export function selectClass(className: string) : selectClassAction {
  return {
    type: SELECT_CLASS,
    className: className
  };
}
