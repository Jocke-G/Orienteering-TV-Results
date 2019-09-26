import React, { Dispatch } from "react"
import { RouteComponentProps, withRouter } from "react-router"
import { ThunkDispatch } from "redux-thunk"
import { Action } from "../store/results/actions"
import { connect } from "react-redux"
import { RootState } from "../reducers/rootReducer"
import { Link } from "react-router-dom"
import { getClasses, Class } from "../store/classes/reducers"
import { fetchClasses } from "../store/classes/actions"

export interface OwnProps {
}
  
interface StateProps {
  classes?: Class[],
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
        {this.props.classes && this.props.classes.length > 0 ? this.props.classes.map((item, key) =>
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
