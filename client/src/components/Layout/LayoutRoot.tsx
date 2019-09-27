import React, { Component, Dispatch, Fragment, CSSProperties } from 'react';
import { connect } from "react-redux";
import { RouteComponentProps, withRouter } from 'react-router';
import { ThunkDispatch } from 'redux-thunk';
import { Action } from '../../store/layouts/actions';
import { Layout, getLayout } from '../../store/layouts/reducers';
import ClassResultView from '../ClassResultView';

export interface OwnProps {
}

type StateProps = {
  layout: Layout|null|undefined,
}

interface DispatchProps {
}

type Props = RouteComponentProps<{}> & StateProps & DispatchProps & OwnProps

class LayoutRoot extends Component<Props> {
  render() {
    let props = this.props
    if(!props.layout) {
      return <div>No layout</div>
    }

    var wrapperStyle:CSSProperties = {
      display: "grid",
    } 

    return (
      <div style={wrapperStyle} className='wrapper'>
        {props.layout.Rows.map((row, rowKey) => 
        <Fragment key={rowKey}>
          {row.Columns.map((cell, colKey) => {
              var style:CSSProperties = {
                gridColumn: colKey + 1,
                gridRow: rowKey + 1,
                minHeight: 200,
              }
            return(
              <div style={style} key={rowKey + "-" +colKey}><ClassResultView class={cell.Class} /></div>
            )
          }
          )}
        </Fragment>
        )}
      </div>
    );
  }
}

const mapStateToProps = (state: any, ownProps: OwnProps): StateProps => {
  return {
    layout: getLayout(state),
  }
}

const mapDispatchToProps = (dispatch: ThunkDispatch<{}, {}, any> & Dispatch<Action>, ownProps: OwnProps): DispatchProps => {
  return {
  }
}

export default withRouter(
  connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(LayoutRoot)
);
