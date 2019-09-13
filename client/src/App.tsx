import * as React from 'react'
import { Dispatch } from "redux";
import { connect } from "react-redux";
import { RouteComponentProps, withRouter } from "react-router";
import queryString from 'query-string';
import { ThunkDispatch } from 'redux-thunk';

import './App.css';

import ClassResult from './components/ClassResultView';
import { requestConfiguration } from './store/configuration/actions';
import { ClassResults, getResults } from './store/results/reducers';
import { fetchClass, selectClass, Action } from './store/results/actions';
import SelectClass from './components/SelectClass';
import { hasConfiguration } from './store/configuration/reducers';

export interface OwnProps {
//  propFromParent: number
}

interface StateProps {
  results?: ClassResults,
  selectedClass: string|null,
  hasConfiguration: boolean,
}

interface DispatchProps {
  requestConfiguration: () => Promise<void>;
  selectClass: (className:string) => void;
  fetchClass: (className:string) => void;
}

type Props = RouteComponentProps<{}> & StateProps & DispatchProps & OwnProps

interface State {
  //internalComponentStateField: string
}

class App extends React.Component<Props, State> {
  constructor(props:Props){
    super(props);
    this.props.requestConfiguration().then(() => {
      console.log("[UI] Conf received");
      this.updateSelectedClass();
    });
  }

  componentDidUpdate(props:Props) {
    if(this.props.hasConfiguration) {
      this.updateSelectedClass();
    }
  }

  updateSelectedClass() {
    console.log("[UI] updateSelectedClass");
    let parsed = queryString.parse(this.props.location.search);
    let className = parsed['Class'];
    console.log(`[UI] className ${className}`);
    if(typeof className === "string" && this.props.selectedClass !== className) {
      console.log(`[UI] selecting class: ${className}`);
      this.props.selectClass(className);
      this.props.fetchClass(className);
    }
  }

  render() {
    if(this.props.hasConfiguration){
      if(this.props.selectedClass !== null) {
        console.log(`[UI/App] render ClassResult: ${this.props.selectedClass}`);
        return (
          <ClassResult />
        );
      } else {
        console.log(`[UI/App] render SelectClass`);
        return (
          <SelectClass />
        );
      }
    } else {
      console.log(`[UI/App] Rendering Loader`);
      return (<div>Fetching configuration</div>)
    }
  }
}

const mapStateToProps = (state: any, ownProps: OwnProps): StateProps => {
  return {
    hasConfiguration: hasConfiguration(state),
    results: getResults(state),
    selectedClass: state.results.selectedClass,
  }
}

const mapDispatchToProps = (dispatch: ThunkDispatch<{}, {}, any> & Dispatch<Action>, ownProps: OwnProps): DispatchProps => {
  return {
    requestConfiguration: async () => {await dispatch(requestConfiguration())},
    selectClass: (className: string) => dispatch(selectClass(className)),
    fetchClass: async (className) => dispatch(fetchClass(className))
  }
}

export default withRouter(
  connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(App)
);
