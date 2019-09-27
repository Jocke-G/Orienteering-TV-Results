import React, { Component, Dispatch, Fragment } from 'react';
import { connect } from "react-redux";
import { ThunkDispatch } from 'redux-thunk';
import { Action, } from '../../store/results/actions';
import { requestConfiguration } from '../../store/configuration/actions';
import { Configuration, getConfiguration, getError, isFetching, State } from '../../store/configuration/reducers';
import BusyButton from '../BusyButton';

export interface OwnProps {
}

type StateProps = {
  isFetching: boolean,
  configuration: Configuration,
  error: Error|null,
}

interface DispatchProps {
  fetch: () => void;
}

type Props = StateProps & DispatchProps & OwnProps

class InitModal extends Component<Props> {
  onClick = () => {
    this.props.fetch();
  };

  render() {
    let error = this.props.error;
    let props = this.props;
    return(
    <div>
      <p><b>Integrationsinställningar</b></p>
        {error?
          <p style={{color: "red"}}>{ error.message }</p>
        :
          <Fragment>
            <p>MQTT: {this.props.configuration.mqtt_host}:{this.props.configuration.mqtt_port}</p>
            <p>REST: {this.props.configuration.rest_host}:{this.props.configuration.rest_port}</p>
          </Fragment>
        }
        <BusyButton isBusy={props.isFetching} onClick={this.onClick}></BusyButton>
      </div>
    )
  }
}

const mapStateToProps = (state: any, ownProps: OwnProps): StateProps => {
  return {
    isFetching: isFetching(state),
    configuration: getConfiguration(state),
    error: getError(state),
  }
}
    
const mapDispatchToProps = (dispatch: ThunkDispatch<State, Props, any> & Dispatch<Action>, ownProps: OwnProps): DispatchProps => {
  return {
    fetch: () => dispatch(requestConfiguration()),
  }
}
    
export default connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(InitModal);
