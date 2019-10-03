import React, { Component, Dispatch, Fragment, CSSProperties, RefObject } from 'react';
import { connect } from "react-redux";
import { RouteComponentProps, withRouter } from 'react-router';
import { ThunkDispatch } from 'redux-thunk';
import { Action, fetchLayout } from '../../store/layouts/actions';
import { Layout, getLayout } from '../../store/layouts/reducers';
import ClassResultView from '../ClassResultView';

export interface OwnProps {
  layoutName: string,
}

type StateProps = {
  layout: Layout|null|undefined,
}

interface DispatchProps {
  fetchLayout: (layoutName:string) => void;
}

type Props = RouteComponentProps<{}> & StateProps & DispatchProps & OwnProps

interface State {
  cells: CellDictionary,
  scrollDown: boolean,
  scrollWait: number,
}

export interface CellDictionary {
  [index:string]: RefObject<HTMLDivElement>
}

class LayoutRoot extends Component<Props, State> {
  constructor(props:Props, state:State) {
    super(props);
    this.state = {
      cells: {},
      scrollDown: true,
      scrollWait: 0,
    }
    props.fetchLayout(this.props.layoutName);
  }

  componentDidMount() {
    setInterval(() => {
      let needsLongerScroll = false;
      if(this.state.scrollWait > 0) {
        this.setState({scrollWait: this.state.scrollWait - 1});
        return;
      }

      for (let cell in this.state.cells) {
        let div = this.state.cells[cell].current;
        if(div === null)
          return;

        if(this.state.scrollDown && div.scrollTop + div.clientHeight < div.scrollHeight)
        {
          needsLongerScroll = true;
          div.scrollTop += 4;
        } else if(!this.state.scrollDown && div.scrollTop > 0) {
          div.scrollTop -= 4;
          needsLongerScroll = true;
        }
      }

      if(!needsLongerScroll) {
        this.setState({
          scrollDown:!this.state.scrollDown,
          scrollWait: 10,
        });
      }
    }, 100);
  }

  render() {
    let props = this.props
    if(!props.layout) {
      return <div>No layout</div>
    }

    var wrapperStyle:CSSProperties = {
      display: "grid",
      width: "100vw",
      height: "100vh",
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
                overflow: "scroll",
              }
              let key = rowKey + "-" +colKey;
              let cells = this.state.cells;
              cells[key] = React.createRef();
              let cellDiv = <div ref={this.state.cells[key]} style={style} key={key}><ClassResultView class={cell.Class} /></div>;
                return(
              cellDiv
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
    fetchLayout: (layoutName:string) => dispatch(fetchLayout(layoutName)),
  }
}

export default withRouter(
  connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(LayoutRoot)
);
