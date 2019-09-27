import React, { Component, Dispatch, CSSProperties } from 'react';
import { connect } from "react-redux";
import { ThunkDispatch } from 'redux-thunk';
import { Action, } from '../store/results/actions';
import IntegrationConfigurationPane from './InitModalItems/IntegrationConfigurationPane';
import ClassSelectorPane from './InitModalItems/ClassSelectorPane';
import MqttStatusPane from './InitModalItems/MqttStatusPane';

export interface OwnProps {
  show: boolean,
}

type StateProps = {
}

interface DispatchProps {
}

type Props = StateProps & DispatchProps & OwnProps

class InitModal extends Component<Props> {
  render() {
    if(!this.props.show) {
      return null;
    }

    // The gray background
    const backdropStyle:CSSProperties = {
      position: 'fixed',
      zIndex: 1,
      top: 0,
      bottom: 0,
      left: 0,
      right: 0,
      backgroundColor: 'rgba(0,0,0,0.3)',
      padding: 50
    };

    // The modal "window"
    const modalStyle:CSSProperties = {
      backgroundColor: '#fff',
      borderRadius: 5,
      maxWidth: 500,
      minHeight: 300,
      margin: '0 auto',
      padding: 30
    };

    return(
      <div className="backdrop" style={backdropStyle}>
        <div className="modal" style={modalStyle}>
          <IntegrationConfigurationPane />
          <hr />
          <ClassSelectorPane />
          <hr />
          <MqttStatusPane />
        </div>
      </div>
    )
  }
}

const mapStateToProps = (state: any, ownProps: OwnProps): StateProps => {
  return {
  }
}
  
const mapDispatchToProps = (dispatch: ThunkDispatch<{}, {}, any> & Dispatch<Action>, ownProps: OwnProps): DispatchProps => {
  return {
  }
}
  
export default connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(InitModal);
