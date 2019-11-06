import React, { Component } from 'react';

import { IndependentResult } from '../../store/results/reducers';
import CompetitorResultComponent from './CompetitorResultComponent';

export interface Props {
  id: string,
  results: IndependentResult[],
}

class IndependentResultsTable extends Component<Props> {
  render() {
    return (
      <table id={this.props.id} className="result">
	      <colgroup>
          <col className="name" />
	        <col className="club" />
          <col className="class" />
		      <col className="time" />
          <col className="ordinal" />
		      <col className="time" />
        </colgroup>
        {this.props.id === 'header'?
        
        <thead>
          <tr className="thead_1">
	          <th colSpan={3}>Senaste målgångar</th>
            <th colSpan={3}>Mål</th>
          </tr>
          <tr className="thead_2">
            <th>Namn</th>
            <th>Klubb</th>
            <th>Klass</th>
            <th>Passertid</th>
            <th>#</th>
            <th>Tid</th>
          </tr>
        </thead>
        :null}
        <tbody>
          {this.props.results.length > 0? this.props.results.map((item, key) =>
            <CompetitorResultComponent index={key} key={key} result={item} />
          ):
          <tr>
            <td colSpan={6}>
              <i>Inga stämplingar ännu</i>
            </td>
          </tr>
          }
        </tbody>
      </table>
    );
  }
}

export default IndependentResultsTable;
