import React, { Component, Dispatch, Fragment } from 'react';
import { connect } from "react-redux";
import { getResults, ClassResults } from '../store/results/reducers';
import { RouteComponentProps, withRouter } from 'react-router';
import { ThunkDispatch } from 'redux-thunk';
import { Action, fetchClass } from '../store/results/actions';
import { subscribeClass, unsubscribeClass } from '../store/mqtt/actions';
import ClassResultsTable from './ClassResultTable';

export interface OwnProps {
  class: string,
}

type StateProps = {
  results?: ClassResults
}

interface DispatchProps {
  subscribeClass: (className:string) => void;
  fetchClass: (className:string) => void;
  unsubscribeClass: (className:string) => void;
}

type Props = RouteComponentProps<{}> & StateProps & DispatchProps & OwnProps

class ClassResultsView extends Component<Props> {
  constructor(props:Props) {
    super(props);
    props.subscribeClass(props.class);
    props.fetchClass(props.class);
  }

  componentWillUnmount() {
    this.props.unsubscribeClass(this.props.class)
  }

  render() {
    if(this.props.results === undefined) {
      return (<p><i>Väntar på klassdefinition för '{this.props.class}'</i></p>)
    }
    return (
      <Fragment>
        <ClassResultsTable id={"header"} class={this.props.results} results={this.props.results.Results.slice(0, 4)} />
        {this.props.results.Results.length >= 5?
          <ClassResultsTable id={"scroll"} results={this.props.results.Results.slice(4)} />
        :null}
      </Fragment>
    );
  }
}

const mapStateToProps = (state: any, ownProps: OwnProps): StateProps => {
  return {
    results: getResults(state, ownProps.class),
  }
}

const mapDispatchToProps = (dispatch: ThunkDispatch<{}, {}, any> & Dispatch<Action>, ownProps: OwnProps): DispatchProps => {
  return {
    subscribeClass: (className:string) => dispatch(subscribeClass(className)),
    fetchClass: async (className:string) => dispatch(fetchClass(className)),
    unsubscribeClass: (className:string) => dispatch(unsubscribeClass(className)),
  }
}

export default withRouter(
  connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(ClassResultsView)
);
