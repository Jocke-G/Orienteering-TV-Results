import React, { Component } from 'react';
import { connect , } from "react-redux";
import { AppState } from "../store";

import { getResults } from '../reducers/resultsReducer';
import { ClassResults } from '../actions/types';
import ClassCompetitorResultComponent from './ClassCompetitorResultComponent';

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
	        <col className="place" />
		      <col className="time" />
        </colgroup>
        <thead>
          <tr className="thead_1">
	          <th colSpan={4}>Preliminära Liveresultat { this.props.results ? this.props.results.ShortName : '' }</th>
          </tr>
          <tr className="thead_2">
            <th align="left">Namn</th>
            <th align="left">Klubb</th>
            <th align="right">#</th>
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

const mapStateToProps = (state: AppState) => ({
  results: getResults(state.resultsReducer),
});
  
export default connect(
  mapStateToProps
)(ClassResultsView);
