import React, { Component, Dispatch, Fragment } from 'react';
import { connect } from "react-redux";
import { ThunkDispatch } from 'redux-thunk';

import { Action, fetchFinish } from '../../store/results/actions';
import { IndependentResult, getFinishResults } from '../../store/results/reducers';
import { subscribeFinish, unsubscribeFinish } from '../../store/mqtt/actions';
import IndependentResultsTable from './IndependentResultTable';

export interface OwnProps {
}
  
type StateProps = {
  results?: IndependentResult[],
}
  
interface DispatchProps {
  subscribeFinish: () => void;
  fetchFinish: (limit:number) => void;
  unsubscribeFinish: () => void;
}
  
type Props = StateProps & DispatchProps & OwnProps
  
class FinishView extends Component<Props> {

  constructor(props:Props) {
    super(props);
    props.subscribeFinish();
    props.fetchFinish(50);
  }

  componentWillUnmount() {
    this.props.unsubscribeFinish();
  }
  
  render() {
    if(this.props.results === undefined) {
      return (<p><i>Väntar på målstämplingar</i></p>)
    }

    return (
      <Fragment>
        <IndependentResultsTable id={"header"} results={this.props.results.slice(0, 5)} />
        {this.props.results.length >= 5?
          <IndependentResultsTable id={"scroll"} results={this.props.results.slice(5)} />
        :null}
      </Fragment>
    );
  }
}

const mapStateToProps = (state: any, ownProps: OwnProps): StateProps => {
    return {
      results: getFinishResults(state),
    }
}

const mapDispatchToProps = (dispatch: ThunkDispatch<{}, {}, any> & Dispatch<Action>, ownProps: OwnProps): DispatchProps => {
  return {
    subscribeFinish: () => dispatch(subscribeFinish()),
    fetchFinish: (limit:number) => dispatch(fetchFinish(limit)),
    unsubscribeFinish: () => dispatch(unsubscribeFinish()),
  }
}

export default connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(FinishView);
