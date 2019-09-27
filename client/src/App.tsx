import * as React from 'react'
import { Dispatch } from "redux";
import { connect } from "react-redux";
import { RouteComponentProps, withRouter } from "react-router";
import queryString from 'query-string';
import { ThunkDispatch } from 'redux-thunk';

import './App.css';

import ClassResult from './components/ClassResultView';
import { requestConfiguration } from './store/configuration/actions';
import { fetchClass, Action } from './store/results/actions';
import { hasConfiguration } from './store/configuration/reducers';
import InitModal from './components/InitModal';
import { selectClass } from './store/classes/actions';
import Mousetrap from 'mousetrap';
import { getSelectedClass } from './store/classes/reducers';
import { Layout, getLayout } from './store/layouts/reducers';
import LayoutRoot from './components/Layout/LayoutRoot';

export interface OwnProps {
}

interface StateProps {
  layout: Layout|null|undefined,
  selectedClass?: string,
  hasConfiguration: boolean,
}

interface DispatchProps {
  requestConfiguration: () => Promise<void>;
  selectClass: (className:string) => void;
  fetchClass: (className:string) => void;
}

type Props = RouteComponentProps<{}> & StateProps & DispatchProps & OwnProps

interface State {
  modalVisible: boolean,
}

class App extends React.Component<Props, State> {
  constructor(props:Props){
    super(props);
    this.state = {
      modalVisible: true,
    };
    this.init();
  }

  init() {
    console.log("[UI] Initiating");
    this.props.requestConfiguration()
    .then(() => {
      console.log("[UI] Conf received");
      this.updateSelectedClass();
    });
  }

  componentDidMount() {
    Mousetrap.bind('c', () => {
      this.toggleModal();
    })
  }

  toggleModal() {
    console.log("toggleModal");
    this.setState((state:Readonly<State>) => ({
      modalVisible: !state.modalVisible,
    }));
  }

  componentDidUpdate(props:Props) {
  }

  updateSelectedClass() {
    console.log("[UI] updateSelectedClass");
    let parsed = queryString.parse(this.props.location.search);
    let className = parsed['Class'];
    console.log(`[UI] QueryString Class: ${className}, Selected class: ${this.props.selectedClass}`);
    if(typeof className === "string" && this.props.selectedClass !== className) {
      console.log(`[UI] selecting class: ${className}`);
      this.props.selectClass(className);
      this.props.fetchClass(className);
    }
  }

  render() {
    return(

    <React.Fragment>
      <InitModal show={this.state.modalVisible} />
      {this.props.layout?
      <LayoutRoot />
      : this.props.selectedClass?
        <ClassResult class={this.props.selectedClass} />
        : null
      }
    </React.Fragment>
    );
  }
}

const mapStateToProps = (state: any, ownProps: OwnProps): StateProps => {
  return {
    layout: getLayout(state),
    hasConfiguration: hasConfiguration(state),
    selectedClass: getSelectedClass(state),
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
