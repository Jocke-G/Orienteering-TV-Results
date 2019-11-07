import React, { Component, Fragment } from 'react';

import { ClassResults, ClassResult } from '../../store/results/reducers';
import ClassCompetitorResultComponent from './ClassCompetitorResultComponent';
import { LayoutCellOptions } from '../../store/layouts/reducers';

export interface Props {
  id: string,
  class: ClassResults,
  results: ClassResult[],
  options?: LayoutCellOptions,
}

class ClassResultsTable extends Component<Props> {
  render() {
    return (
      <table id={this.props.id} className="result">
	      <colgroup>
          <col className="name" />
          {this.props.options && this.props.options.ShowStartTime?
  	        <col className="club" />
          :null}
		      <col className="time" />
          {this.props.class.SplitControls? this.props.class.SplitControls.map((item, key) => 
          <Fragment key={key}>
            <col className="ordinal" />
            <col className="splitTime" />
          </Fragment>
            ):null}
	        <col className="ordinal" />
		      <col className="time" />
        </colgroup>
        {this.props.id === 'header'?
        <thead>
          <tr className="thead_1">
	          <th colSpan={2+Number(this.props.options&&this.props.options.ShowStartTime)}>Preliminära Liveresultat { this.props.class.ShortName }</th>
            {this.props.class.SplitControls? this.props.class.SplitControls.map((item, key) => 
              <th key={key} colSpan={2}>{item.Name}</th>
              ):null}
            <th colSpan={2}>Mål</th>
          </tr>
          <tr className="thead_2">
            <th align="left">Namn</th>
            <th align="left">Klubb</th>
            {this.props.options && this.props.options.ShowStartTime?
            <th align="left">Starttid</th>
            :null}
            {this.props.class.SplitControls? this.props.class.SplitControls.map((item, key) => 
              <Fragment key={key}>
                <th>#</th>
                <th>Tid</th>
              </Fragment>
            ):null}
            <th align="right">#</th>
            <th align="right">Tid</th>
          </tr>
        </thead>
        :
        null
        }
        <tbody>
          {this.props.results.length > 0? this.props.results.map((item, key) =>
            <ClassCompetitorResultComponent index={key} key={key} result={item} options={this.props.options} />
          ):
          <tr>
            <td colSpan={4+Number(this.props.options&&this.props.options.ShowStartTime)+(this.props.class.SplitControls?this.props.class.SplitControls.length*2:0)}>
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
