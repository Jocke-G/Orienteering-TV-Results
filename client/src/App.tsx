import React, { Component, Fragment } from 'react'
import { Dispatch } from "redux";
import { connect } from "react-redux";
import { RouteComponentProps, withRouter, Route, match as Match } from "react-router";
import { ThunkDispatch } from 'redux-thunk';
import Mousetrap from 'mousetrap';

import './App.css';

import { requestConfiguration } from './store/configuration/actions';
import { Action } from './store/results/actions';
import { hasConfiguration } from './store/configuration/reducers';
import InitModal from './components/InitModal';
import LayoutRoot from './components/Layout/LayoutRoot';
import ClassResultView from './components/ClassResultView';

export interface OwnProps {
}

interface StateProps {
  hasConfiguration: boolean,
}

interface DispatchProps {
  requestConfiguration: () => Promise<void>;
}

type Props = RouteComponentProps<{}> & StateProps & DispatchProps & OwnProps

interface State {
  modalVisible: boolean,
}

class App extends Component<Props, State> {
  constructor(props:Props){
    super(props);

    const location = this.props.location['pathname'];
    this.state = {
      modalVisible: location === "/",
    };
    this.props.requestConfiguration();
  }

  componentDidMount() {
    Mousetrap.bind('c', () => {
      this.toggleModal();
    })
  }

  toggleModal() {
    this.setState((state:Readonly<State>) => ({
      modalVisible: !state.modalVisible,
    }));
  }

  selectLayout = (layoutName:string) => {
    this.props.history.push(`/layout/${layoutName}`)
  }

  selectClass = (className:string) => {
    this.props.history.push(`/class/${className}`)
  }

  renderClassResults = (match:Match<any>|null) => {
    if(match == null) {
      return(null);
    }
    return(<ClassResultView class={ match.params.className }/>);
  }

  renderLayout = (match:Match<any>|null) => {
    if(match == null) {
      return(null);
    }
    return(<LayoutRoot layoutName={ match.params.layoutName }/>);
  }

  render() {
    return(
    <Fragment>
      <InitModal selectLayout={this.selectLayout} selectClass={this.selectClass} show={this.state.modalVisible} />
      {!this.props.hasConfiguration?
      <p><i>Hämtar konfiguration</i></p>
      :
      <Fragment>
        <Route exact path="/layout/:layoutName" children={({ match }) => this.renderLayout(match)} />
        <Route exact path="/class/:className" children={({ match }) => this.renderClassResults(match)}/>
      </Fragment>
    }
    </Fragment>
    );
  }
}

const mapStateToProps = (state: any, ownProps: OwnProps): StateProps => {
  return {
    hasConfiguration: hasConfiguration(state),
  }
}

const mapDispatchToProps = (dispatch: ThunkDispatch<{}, {}, any> & Dispatch<Action>, ownProps: OwnProps): DispatchProps => {
  return {
    requestConfiguration: async () => {dispatch(requestConfiguration())},
  }
}

export default withRouter(
  connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(App)
);
