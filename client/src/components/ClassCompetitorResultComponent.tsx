import React, { Component, Fragment } from 'react';
import { connect , } from "react-redux";
import { ClassResult } from '../store/results/reducers';

type AppProps = {
  result: ClassResult
  index: number;
}

class ClassResultsView extends Component<AppProps> {
  render() {
    return (
      <tr className= { this.props.index % 2 === 0 ? "trDark" : ""}>
        <td>{ this.props.result.FirstName } { this.props.result.LastName }</td>
        <td>{ this.props.result.Club }</td>
        <td>{ this.props.result.StartTime }</td>
        {this.props.result.SplitTimes.map((item, key) => 
          <Fragment key={key}>
            <td>{item.Ordinal}</td>
            <td>{item.Time}</td>
          </Fragment>
        )}
          {(() => {
          switch(this.props.result.Status) {
            case 'Passed':
              return (
                <Fragment>
                  <td align='right'>{ this.props.result.Ordinal }</td>
                  <td align='right'>{ this.props.result.TotalTime }</td>
                </Fragment>
              );
            case 'Finished':
              return (
                <Fragment>
                  <td align='right'>({ this.props.result.Ordinal })</td>
                  <td align='right'>{ this.props.result.TotalTime }</td>
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

export default connect(
)(ClassResultsView);
