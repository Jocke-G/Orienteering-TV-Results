import React, { Component, Fragment } from 'react';

import { ClassResult } from '../../store/results/reducers';

type Props = {
  result: ClassResult
  index: number;
}

class ClassCompetitorResultComponent extends Component<Props> {

  getStartTimeString = ():string => {
    let dateObj = new Date(this.props.result.StartTime);
    return `${dateObj.getHours().toString().padStart(2, '0')}:${dateObj.getMinutes().toString().padStart(2, '0')}:${dateObj.getSeconds().toString().padStart(2, '0')}`;
  }

  toSplitTime = (value:any):string => {
    if(value == null)
      return "";

    return value.replace(/^00:/,'');
  }

  render() {
    return (
      <tr className= { this.props.index % 2 === 0 ? "trDark" : ""}>
        <td>{ this.props.result.FirstName } { this.props.result.LastName }</td>
        <td>{ this.props.result.Club }</td>
        <td>{ this.getStartTimeString() }</td>
        {this.props.result.SplitTimes.map((item, key) => 
          <Fragment key={key}>
            <td>{item.Ordinal}</td>
            <td>{this.toSplitTime(item.Time)}</td>
          </Fragment>
        )}
          {(() => {
          switch(this.props.result.Status) {
            case 'Passed':
              return (
                <Fragment>
                  <td align='right'>{ this.props.result.Ordinal }</td>
                  <td align='right'>{ this.toSplitTime(this.props.result.TotalTime) }</td>
                </Fragment>
              );
            case 'Finished':
              return (
                <Fragment>
                  <td align='right'>({ this.props.result.Ordinal })</td>
                  <td align='right'>{ this.toSplitTime(this.props.result.TotalTime) }</td>
                </Fragment>
              );
            case 'NotFinishedYet':
              return <td colSpan={2} align='right'></td>;
            case 'NotPassed':
              return <td colSpan={2} align='right'><i>Ej Godkänd</i></td>;
            case 'NotStarted':
              return <td colSpan={2} align='right'><i>Ej start</i></td>;
            default:
              return <td colSpan={2} align='right'><i>{ this.props.result.Status }</i></td>
              }
        })()}
      </tr>
    )
  }
}

export default ClassCompetitorResultComponent;
