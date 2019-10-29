import React, { Component, Fragment } from 'react';

import { ClassResults, ClassResult } from '../../store/results/reducers';
import ClassCompetitorResultComponent from './ClassCompetitorResultComponent';

export interface Props {
  id: string,
  class: ClassResults,
  results: ClassResult[],
}

class ClassResultsTable extends Component<Props> {
  render() {
    return (
      <table id={this.props.id} className="result">
	      <colgroup>
          <col className="name" />
	        <col className="club" />
		      <col className="time" />
          {this.props.class? this.props.class.SplitControls.map((item, key) => 
          <Fragment key={key}>
            <col className="ordinal" />
            {/* <col class=""time""> */}
            <col className="splitTime" />
          </Fragment>
            ):<Fragment />
            }
	        <col className="ordinal" />
		      <col className="time" />
        </colgroup>
        {this.props.id === 'header'?
        <thead>
          <tr className="thead_1">
	          <th colSpan={3}>Preliminära Liveresultat { this.props.class.ShortName }</th>
            {this.props.class.SplitControls.map((item, key) => 
              <th key={key} colSpan={2}>{item.Name}</th>
            )}
            <th colSpan={2}>Mål</th>
          </tr>
          <tr className="thead_2">
            <th align="left">Namn</th>
            <th align="left">Klubb</th>
            <th align="left">Starttid</th>
            {this.props.class.SplitControls.map((item, key) =>
              <Fragment key={key}>
                <th>#</th>
                <th>Tid</th>
              </Fragment>
            )}
            <th align="right">#</th>
            <th align="right">Tid</th>
          </tr>
        </thead>
        :
        null
        }
        <tbody>
          {this.props.results.length > 0? this.props.results.map((item, key) =>
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

export default ClassResultsTable;
