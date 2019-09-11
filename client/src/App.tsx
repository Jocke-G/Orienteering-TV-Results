import * as React from 'react'
import { Dispatch } from "redux";
import { connect } from "react-redux";
import { RouteComponentProps, withRouter } from "react-router";
import queryString from 'query-string'

import './App.css';

import ClassResult from './components/ClassResultView';
import { ResultsActionTypes, ClassResults } from './actions/types';
import { selectClass } from './actions/selectClass';

export interface OwnProps {
//  propFromParent: number
}

interface StateProps {
  results?: ClassResults,
  selectedClass?: string
}

interface DispatchProps {
  selectClass: (className:string) => void
}

type Props = RouteComponentProps<{}> & StateProps & DispatchProps & OwnProps

interface State {
  //internalComponentStateField: string
}

class App extends React.Component<Props, State> {

  constructor(props:Props) {
    super(props);
    console.log("Search: " + this.props.location.search)
    let parsed = queryString.parse(this.props.location.search);
    let className = parsed['Class'];
    console.log("Class: " + className);
    if(typeof className === "string") {
      console.log("dispatching selectClass");
      this.props.selectClass(className);
    }
  }

  render() {
    console.log("selectedClass:" + this.props.selectedClass)
    if(this.props.selectedClass !== undefined) {
      return (
        <div className="App">
          <ClassResult />
        </div>
      );
    } else {
      return (
        <div className="App">
          <div>Select class</div>
          </div>
      );
    }
  }
}

const mapStateToProps = (state:any /* RootState? State? Nothing works */, ownProps: OwnProps): StateProps => {
  return {
    results: state.resultsReducer.classResults,
    selectedClass: state.resultsReducer.selectedClass,
  }
}

const mapDispatchToProps = (dispatch: Dispatch<ResultsActionTypes>): DispatchProps => {
   return {
    selectClass: (className: string) => dispatch(selectClass(className)),
  }
}

export default withRouter(connect<StateProps, DispatchProps, OwnProps>
  (mapStateToProps, mapDispatchToProps)(App));
