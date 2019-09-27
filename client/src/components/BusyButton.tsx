import React, { Component, FormEvent } from "react"

export interface OwnProps {
  isBusy: boolean,
}

type StateProps = {
}

interface DispatchProps {
  onClick: (event:FormEvent) => void,
}

type Props = StateProps & DispatchProps & OwnProps

class BusyButton extends Component<Props> {
  render() {
    const props = this.props;
    return(
      <button disabled={props.isBusy} onClick={props.onClick}>
        {props.isBusy ?
          <span>Hämtar...</span>
          :
          <span>Hämta</span>
        }
      </button>
    );
  }
}

export default BusyButton;
