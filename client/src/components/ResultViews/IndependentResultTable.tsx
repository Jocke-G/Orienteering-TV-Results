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
		      <col className="time" />
          <col className="ordinal" />
        </colgroup>
        {this.props.id === 'header'?
        <thead>
          <tr>
            <th>Namn</th>
            <th>Klubb</th>
            <th>Klass</th>
            <th>Passertid</th>
            <th>Måltid</th>
            <th>Prel. plac.</th>
          </tr>
        </thead>
        :null}
        <tbody>
          {this.props.results.length > 0? this.props.results.map((item, key) =>
            <CompetitorResultComponent index={key} key={key} result={item} />
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

export default IndependentResultsTable;
