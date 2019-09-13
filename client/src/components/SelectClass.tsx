import React, { Dispatch } from "react"
import { RouteComponentProps, withRouter } from "react-router"
import { ThunkDispatch } from "redux-thunk"
import { Action, fetchClasses } from "../store/results/actions"
import { connect } from "react-redux"
import { RootState } from "../reducers/rootReducer"
import { getClasses, ClassResults } from "../store/results/reducers"
import { Link } from "react-router-dom"

export interface OwnProps {
}
  
interface StateProps {
  classes: ClassResults[],
}
  
interface DispatchProps {
  fetchClasses: () => void;
}
  
type Props = RouteComponentProps<{}> & StateProps & DispatchProps & OwnProps
  
interface State {
}
  
class SelectClass extends React.Component<Props, State> {
  constructor(props:Props){
    super(props);
    this.props.fetchClasses();
  }
  render() {
    return(
      <div>
        {this.props.classes.length > 0 ? this.props.classes.map((item, key) =>
         <div key={key}><Link to={`/?Class=${item.ShortName}`}>{item.ShortName}</Link></div>
        ):<div>Inga klasser</div>
        }
      </div>
    )
  }
}

const mapStateToProps = (state: any, ownProps: OwnProps): StateProps => {
  return {
    classes: getClasses(state),
  }
}

const mapDispatchToProps = (dispatch: ThunkDispatch<RootState, {}, any> & Dispatch<Action>, ownProps: OwnProps): DispatchProps => {
  return {
    fetchClasses: () => dispatch(fetchClasses()),
  }
}

export default withRouter(
  connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(SelectClass)
);
