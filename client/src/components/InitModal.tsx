import React, { Component, Dispatch, CSSProperties } from 'react';
import { connect } from "react-redux";
import { ThunkDispatch } from 'redux-thunk';

import { Action, } from '../store/results/actions';
import IntegrationConfigurationPanel from './InitModalItems/IntegrationConfigurationPanel';
import ClassPanel from './InitModalItems/ClassPanel';
import MqttStatusPanel from './InitModalItems/MqttStatusPanel';
import LayoutPanel from './InitModalItems/LayoutPanel';

export interface OwnProps {
  show: boolean,
  selectClass: (className:string) => void;
  selectFinish: () => void;
  selectLayout: (layoutName:string) => void;
}

type StateProps = {
}

interface DispatchProps {
}

type Props = StateProps & DispatchProps & OwnProps

interface State {
}

class InitModal extends Component<Props, State> {

  selectClass = (className:string) => {
    this.props.selectClass(className);
  }

  selectFinish = () => {
    this.props.selectFinish();
  }

  selectLayout = (layoutName:string) => {
    this.props.selectLayout(layoutName);
  }

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
          <IntegrationConfigurationPanel />
          <hr />
          <LayoutPanel selectLayout={this.selectLayout} />
          <hr />
          <ClassPanel selectClass={this.selectClass} selectFinish={this.selectFinish} />
          <hr />
          <MqttStatusPanel />
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
