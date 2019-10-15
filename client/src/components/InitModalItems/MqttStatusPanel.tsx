import React, { Component, Dispatch } from 'react';
import { connect } from "react-redux";
import { ThunkDispatch } from 'redux-thunk';
import { Action, } from '../../store/results/actions';
import { getStatus, getLatestMessage, getSubscriptions } from '../../store/mqtt/reducers';
import { Message } from 'paho-mqtt';
import { trigResendResults } from '../../store/mqtt/actions';

export interface OwnProps {
}

type StateProps = {
  status: string,
  latestMessage?: Message,
  subscriptions: string[],
}

interface DispatchProps {
  trigResendResults: () => void,
}

type Props = StateProps & DispatchProps & OwnProps

interface State {
}

class MqttStatusPanel extends Component<Props, State> {
  trigResendResults = () => {
    this.props.trigResendResults();
  }

  render() {
    let props = this.props;
    return(
    <div>
      <p><b>MQTT</b></p>
      <p>Status: {props.status}</p>
      <p>Senaste meddelande: {props.latestMessage?props.latestMessage.destinationName:""}</p>
      <p>Subscriptions: </p>
      <ul>
        {props.subscriptions.map((item, key) =>
          <li key={key}>{item}</li>
        )}
      </ul>
      <button onClick={this.trigResendResults}>Trigga omskick av resultat</button>
    </div>
    )
  }
}

const mapStateToProps = (state: any, ownProps: OwnProps): StateProps => {
  return {
    status: getStatus(state),
    latestMessage: getLatestMessage(state),
    subscriptions: getSubscriptions(state),
  }
}
    
const mapDispatchToProps = (dispatch: ThunkDispatch<State, Props, any> & Dispatch<Action>, ownProps: OwnProps): DispatchProps => {
  return {
    trigResendResults: () => dispatch(trigResendResults()),
  }
}
    
export default connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(MqttStatusPanel);
