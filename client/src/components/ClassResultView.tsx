import React, { Component, Dispatch, Fragment } from 'react';
import { connect } from "react-redux";
import { getResults, ClassResults } from '../store/results/reducers';
import ClassCompetitorResultComponent from './ClassCompetitorResultComponent';
import { RouteComponentProps, withRouter } from 'react-router';
import { ThunkDispatch } from 'redux-thunk';
import { Action, fetchClass } from '../store/results/actions';
import { subscribeClass, unsubscribeClass } from '../store/mqtt/actions';

export interface OwnProps {
  class: string,
}

type StateProps = {
  results?: ClassResults
}

interface DispatchProps {
  subscribeClass: (className:string) => void;
  fetchClass: (className:string) => void;
  unsubscribeClass: (className:string) => void;
}

type Props = RouteComponentProps<{}> & StateProps & DispatchProps & OwnProps

class ClassResultsView extends Component<Props> {
  constructor(props:Props) {
    super(props);
    props.subscribeClass(props.class);
    props.fetchClass(props.class);
  }

  componentWillUnmount() {
    this.props.unsubscribeClass(this.props.class)
  }

  render() {
    return (
      <table id="header" className="result">
	      <colgroup>
          <col className="name" />
	        <col className="club" />
		      <col className="time" />
          {this.props.results? this.props.results.SplitControls.map((item, key) => 
          <Fragment key={key}>
            <col className="place" />
            {/* <col class=""time""> */}
            <col className="splitTime" />
          </Fragment>
            ):<Fragment />
            }
	        <col className="place" />
		      <col className="time" />
        </colgroup>
        <thead>
          <tr className="thead_1">
	          <th colSpan={3}>Preliminära Liveresultat { this.props.results ? this.props.results.ShortName : '' }</th>
            {this.props.results? this.props.results.SplitControls.map((item, key) => 
              <th key={key} colSpan={2}>{item.Name}</th>
            ):
              <Fragment />
            }
            <th colSpan={2}>Mål</th>
          </tr>
          <tr className="thead_2">
            <th align="left">Namn</th>
            <th align="left">Klubb</th>
            <th align="left">Starttid</th>
            {this.props.results? this.props.results.SplitControls.map((item, key) => 
              <Fragment key={key}>
                <th>#</th>
                {/* <th align="right">Passertid</th> */}
                <th>Tid</th>
              </Fragment>
            ):
              <Fragment />
            }
            <th align="right">#</th>
            {/* <th align="right">Passertid</th> */}
            <th align="right">Tid</th>
          </tr>
        </thead>
        <tbody>
          {this.props.results? this.props.results.Results.map((item, key) =>
            <ClassCompetitorResultComponent index={key} key={key} result={item} />
          ):
          <tr>
            <td colSpan={4}>
              <i>Inga stämplingar ännu</i>
            </td>
          </tr>
          }
        </tbody>
      </table>
    );
  }
}

const mapStateToProps = (state: any, ownProps: OwnProps): StateProps => {
  return {
    results: getResults(state, ownProps.class),
  }
}

const mapDispatchToProps = (dispatch: ThunkDispatch<{}, {}, any> & Dispatch<Action>, ownProps: OwnProps): DispatchProps => {
  return {
    subscribeClass: (className:string) => dispatch(subscribeClass(className)),
    fetchClass: async (className:string) => dispatch(fetchClass(className)),
    unsubscribeClass: (className:string) => dispatch(unsubscribeClass(className)),
  }
}

export default withRouter(
  connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(ClassResultsView)
);
