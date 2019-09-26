import React, { Component, Dispatch, Fragment, FormEvent } from 'react';
import { connect } from "react-redux";
import { ThunkDispatch } from 'redux-thunk';
import { Action, fetchClass, } from '../../store/results/actions';
import { getClasses, Class, getError, getSelectedClass } from '../../store/classes/reducers';
import { fetchClasses, selectClass } from '../../store/classes/actions';

export interface OwnProps {
}

type StateProps = {
  classes?: Class[],
  error: Error|null,
  selectedClass?: string,
}

interface DispatchProps {
  fetch: () => void;
  selectClass: (className:Class) => void;
}

type Props = StateProps & DispatchProps & OwnProps

interface State {
  selectedClass?: Class,
}

class ClassSelectorPane extends Component<Props, State> {
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
    console.log(this.state.selectedClass);

    if(this.state.selectedClass)
      this.props.selectClass(this.state.selectedClass);
  }

  render() {
    let error = this.props.error;
    return(
    <div>
      <p><b>Välj klass</b></p>
        {error?
          <p style={{color: "red"}}>{ error.message }</p>
        :
          <Fragment>
            <p>Vald klass: {this.props.selectedClass}</p>
            <button onClick={this.props.fetch}>Hämta klasser</button><br />
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
    selectedClass: getSelectedClass(state),
  }
}
   
const mapDispatchToProps = (dispatch: ThunkDispatch<{}, {}, any> & Dispatch<Action>, ownProps: OwnProps): DispatchProps => {
  return {
    fetch: () => dispatch(fetchClasses()),
    selectClass: (className: Class) => {
      dispatch(selectClass(className.ShortName));
      dispatch(fetchClass(className.ShortName));
    },
  }
}
    
  export default connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(ClassSelectorPane);
