import React, { Component, Dispatch, Fragment, FormEvent } from 'react';
import { connect } from "react-redux";
import { ThunkDispatch } from 'redux-thunk';
import { Action } from '../../store/results/actions';
import { getClasses, Class, getError } from '../../store/classes/reducers';
import { fetchClasses } from '../../store/classes/actions';

export interface OwnProps {
  selectClass: (className:string) => void;
  selectFinish: () => void;
}

type StateProps = {
  classes?: Class[],
  error: Error|null,
}

interface DispatchProps {
  fetch: () => void;
}

type Props = StateProps & DispatchProps & OwnProps

interface State {
  selectedClass?: Class,
}

class ClassPanel extends Component<Props, State> {
  
  constructor(props:Props) {
    super(props);
    this.state = {};
  }

  componentDidUpdate() {
    if(!this.state.selectedClass && this.props.classes) {
      this.setState((state:Readonly<State>, props: Readonly<Props>) => ({
        selectedClass: props.classes ? props.classes[0] : undefined,
      }));
    }
  }

  handleChange = (event:FormEvent<HTMLSelectElement>) => {
    if(!this.props.classes)
      return;

    let index:number = Number(event.currentTarget.value)
    let selectedClass:Class = this.props.classes[index]
    this.setState({selectedClass: selectedClass});
  }

  onSelectClass = () => {
    if(this.state.selectedClass)
      this.props.selectClass(this.state.selectedClass.ShortName);
  }

  onSelectFinish = () => {
    this.props.selectFinish();
  }

  render() {
    let error = this.props.error;
    return(
    <div>
      <p><b>Välj klass</b></p>
        <button onClick={this.props.fetch}>Hämta klasser</button><br />
        {error?
          <p style={{color: "red"}}>{ error.message }</p>
        :
          <Fragment>
            { this.props.classes ?
            <Fragment>
              <select
                onChange={this.handleChange}
                defaultValue={""}
                >
                { this.props.classes.map((item, key) => 
                  <option key={key} value={key}>{item.ShortName}</option>
                )}
              </select>
              <button onClick={this.onSelectClass}>Välj klass</button>
            </Fragment>
            :
            <Fragment></Fragment>
            }
            <button onClick={this.onSelectFinish}>Välj målgångar</button>
        </Fragment>
        }
      </div>
    )
  }
}

const mapStateToProps = (state: any, ownProps: OwnProps): StateProps => {
  return {
    classes: getClasses(state),
    error: getError(state),
  }
}
   
const mapDispatchToProps = (dispatch: ThunkDispatch<State, {}, any> & Dispatch<Action>, ownProps: OwnProps): DispatchProps => {
  return {
    fetch: () => dispatch(fetchClasses()),
  }
}

export default connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(ClassPanel);
