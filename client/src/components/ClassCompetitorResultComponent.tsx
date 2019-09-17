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
            case 'notValid':
              return <td colSpan={2} align='right'><i>Ej Godkänd</i></td>;
            case 'disqualified':
              return <td colSpan={2} align='right'><i>Diskvalificerad</i></td>;
            case 'notStarted':
              return <td colSpan={2} align='right'><i>Ej start</i></td>;

            case 'passed':
              return (
                <Fragment>
                  <td align='right'>{ this.props.result.Ordinal }</td>
                  <td align='right'>{ this.props.result.TotalTime }</td>
                </Fragment>
              );
            case 'resultPrel':
              return (
                <Fragment>
                  <td align='right'>({ this.props.result.Ordinal })</td>
                  <td align='right'>{ this.props.result.TotalTime }</td>
                </Fragment>
              );
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
