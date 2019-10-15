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

class IntegrationConfigurationPanel extends Component<Props> {
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
            <p>Layouts REST: {this.props.configuration.layouts_rest_host}:{this.props.configuration.layouts_rest_port} <a href={ `http://${this.props.configuration.layouts_rest_host}:${this.props.configuration.layouts_rest_port}/swagger-ui.html` } rel="noopener noreferrer" target="_blank">Swagger UI</a></p>
            <p>Results REST: {this.props.configuration.results_rest_host}:{this.props.configuration.results_rest_port} <a href={ `http://${this.props.configuration.results_rest_host}:${this.props.configuration.results_rest_port}/swagger/index.html` } rel="noopener noreferrer" target="_blank">Swagger UI</a></p>
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
    
export default connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(IntegrationConfigurationPanel);
