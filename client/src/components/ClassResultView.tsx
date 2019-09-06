import React, { Component } from 'react';
import { connect , } from "react-redux";
import { AppState } from "../store";

//import { getResults } from '../reducers/resultsReducer';
import { ClassResults } from '../actions/types';

type AppProps = {
  results?: ClassResults
}

class ClassResultsView extends Component<AppProps> {
  render() {

    return (
      <table id="header" className="result">
	      <colgroup>
          <col className="name" />
	        <col className="club" />
	        <col className="class" />
		      <col className="time" />
	        <col className="place" />
	        <col className="splitTime" />
        </colgroup>
        <thead>
          <tr className="thead_1">
	          <th colSpan={3}>Preliminära Liveresultat { this.props.results ? this.props.results.ShortName : '' }</th>
          </tr>
          <tr className="thead_2">
            <th align="left">Namn</th>
            <th align="left">Klubb</th>
            <th align="left">Klass</th>
            <th align="left">Starttid</th>
            <th align="right">Tid</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td></td>
          </tr>
        </tbody>
      </table>
    );
  }
}

const mapStateToProps = (state: AppState) => ({
  //results: getResults(state.resultsReducer),
  results: state.resultsReducer.classResults,
});
  
export default connect(
  mapStateToProps
)(ClassResultsView);
  