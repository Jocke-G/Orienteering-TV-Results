import * as React from 'react'
import { Dispatch } from "redux";
import { connect } from "react-redux";
import { RouteComponentProps, withRouter } from "react-router";
import queryString from 'query-string'
import { ThunkDispatch } from 'redux-thunk'

import './App.css';

import ClassResult from './components/ClassResultView';
import { ResultsActionTypes, ClassResults } from './actions/types';
import { selectClass } from './actions/selectClass';
import { fetchClass } from './actions/fetchClass';

export interface OwnProps {
//  propFromParent: number
}

interface StateProps {
  results?: ClassResults,
  selectedClass?: string
}

interface DispatchProps {
  selectClass: (className:string) => void
  fetchClass: (className:string) => void
}

type Props = RouteComponentProps<{}> & StateProps & DispatchProps & OwnProps

interface State {
  //internalComponentStateField: string
}

class App extends React.Component<Props, State> {

  constructor(props:Props) {
    super(props);
    let parsed = queryString.parse(this.props.location.search);
    let className = parsed['Class'];
    if(typeof className === "string") {
      this.props.fetchClass(className);
      this.props.selectClass(className);
    }
  }

  render() {
    console.log("selectedClass:" + this.props.selectedClass)
    if(this.props.selectedClass !== undefined) {
      return (
        <ClassResult />
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

const mapDispatchToProps = (dispatch: ThunkDispatch<{}, {}, any> & Dispatch<ResultsActionTypes>, ownProps: OwnProps): DispatchProps => {
   return {
    selectClass: (className: string) => dispatch(selectClass(className)),
    fetchClass: async (className) => {
      await dispatch(fetchClass(className))
    }
  }
}

export default withRouter(
  connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(App)
);
