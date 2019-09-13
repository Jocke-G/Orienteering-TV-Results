import { ClassResults } from "./types"
import { classResultsReceived } from "./classResultsReceived";

export const fetchClass = (className:string) => {

    return (dispatch: any) => {
        fetch(`http://localhost:5000/api/classes/${ className }`, {
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
