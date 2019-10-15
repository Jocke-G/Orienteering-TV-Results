import React, { Component, Dispatch, Fragment, FormEvent } from 'react';
import { connect } from "react-redux";
import { ThunkDispatch } from 'redux-thunk';
import { Layout, getLayout, getError, isFetching } from '../../store/layouts/reducers';
import { fetchLayout, Action } from '../../store/layouts/actions';
import BusyButton from '../BusyButton';

export interface OwnProps {
  selectLayout: (layoutName:string) => void,
}

type StateProps = {
  layout: Layout|null|undefined,
  isRequesting: boolean,
  error: Error|null|undefined,
}

interface DispatchProps {
  fetch: (layoutName:string) => void;
}

type Props = StateProps & DispatchProps & OwnProps

interface State {
  layoutName: string,
}

class LayoutsPanel extends Component<Props, State> {
  constructor(props:Props) {
    super(props);
    this.state = {
      layoutName: "TV1",
    };
  }

  handleChange = (event:FormEvent<HTMLInputElement>) => {
    this.setState({layoutName: event.currentTarget.value});
  }

  onSelectLayout = () => {
    const layoutName = this.state.layoutName;
    this.props.fetch(layoutName);
    this.props.selectLayout(layoutName)
  }

  render() {
    let props = this.props;
    let error = this.props.error;
    return(
    <div>
      <p><b>Välj layout</b></p>
      <input value={ this.state.layoutName } onChange={this.handleChange} />
      <BusyButton onClick={this.onSelectLayout} isBusy={props.isRequesting} />
      {error?
          <p style={{color: "red"}}>{ error.message }</p>
        :
        <Fragment>
          {props.layout?
          <div>
          <p>Layout: </p>
          <table>
            <tbody>
            {props.layout.Rows.map((row, rowKey) => 
            <tr key={rowKey}>
              {row.Cells.map((cell, rowKey) => 
                <td key={rowKey}>{cell.ClassName}</td>
              )}
            </tr>
            )}
            </tbody>
          </table>
        </div>
        :
        <Fragment />
        }
        </Fragment>
      }
    </div>
    )
  }
}

const mapStateToProps = (state: any, ownProps: OwnProps): StateProps => {
  return {
    layout: getLayout(state),
    error: getError(state),
    isRequesting: isFetching(state),
  }
}
   
const mapDispatchToProps = (dispatch: ThunkDispatch<State, {}, any> & Dispatch<Action>, ownProps: OwnProps): DispatchProps => {
  return {
    fetch: (layoutName:string) => dispatch(fetchLayout(layoutName)),
  }
}

export default connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(LayoutsPanel);
