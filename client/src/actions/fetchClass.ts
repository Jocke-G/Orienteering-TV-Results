import { ClassResults } from "./types"
import { classResultsReceived } from "./classResultsReceived";
import { RootState } from "../reducers/rootReducer";

export const fetchClass = (className:string) => {
    return (dispatch: any, getState:any) => {
        const state:RootState = getState();
        let host = state.configuration.configuration.rest_host;
        let port = state.configuration.configuration.rest_port;
        let classUrl = `http://${host}:${port}/api/classes/${ className }`;
        console.log(`Fetching initial class from ${classUrl}`); 
        fetch(classUrl, {
            method: "GET",
            headers: {}
          })
        .then(r => r.text())
        .then(resp => {
            console.log("Received text: " + resp);
            let obj: ClassResults = JSON.parse(resp);
            dispatch(classResultsReceived(obj))
        })
        .catch((error) => console.error(error) )
    }
}
