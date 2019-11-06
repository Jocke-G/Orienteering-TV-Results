import React, { Component, Dispatch, Fragment, FormEvent } from 'react';
import { connect } from "react-redux";
import { ThunkDispatch } from 'redux-thunk';
import {
  Layout, LayoutRow,
  isRequestingLayouts, getLayouts, getRequestLayoutsError,
  isRequestingLayout, getLayout, getRequestLayoutError,
  isUpdatingLayout, getUpdateLayoutError,
 } from '../../store/layouts/reducers';
import { Action, requestLayouts, requestLayout, updateLayout, } from '../../store/layouts/actions';
import BusyButton from '../BusyButton';
import { Class, getClasses } from '../../store/classes/reducers';

export interface OwnProps {
  selectLayout: (layoutName:string) => void,
}

type StateProps = {
  isRequestingLayouts: boolean,
  layouts?: Layout[],
  requestLayoutsError: Error|null,

  isRequestingLayout: boolean,
  layout?: Layout,
  requestLayoutError: Error|null,

  isUpdatingLayout: boolean,
  updateLayoutError: Error|null,

  classes?: Class[],
}

interface DispatchProps {
  requestLayouts: () => void;
  requestLayout: (layoutName:string) => void;
  updateLayout: (layout:Layout) => void;
}

type Props = StateProps & DispatchProps & OwnProps

interface State {
  layout?: Layout,
  cellTypes: string[],
}

class LayoutsPanel extends Component<Props, State> {
  constructor(props:Props) {
    super(props);
    this.state = {
      cellTypes: [
        "Class",
        "Finish"
      ],
    };
    if(props.layouts == null && !props.isRequestingLayouts)
    {
      props.requestLayouts();
    }
  }

  componentDidUpdate() {
    if(!this.state.layout && this.props.layouts) {
      this.setState((state:Readonly<State>, props: Readonly<Props>) => ({
        layout: props.layouts ? props.layouts[0] : undefined,
      }));
    }
  }

  handleChange = (event:FormEvent<HTMLSelectElement>) => {
    if(this.props.layouts === undefined)
      return;

    this.setState({layout: this.props.layouts[Number(event.currentTarget.value)]});
  }

  onSelectLayout = (event:FormEvent<HTMLButtonElement>) => {
    if(this.state.layout === undefined)
      return;

    this.props.requestLayout(this.state.layout.Name);
    this.props.selectLayout(this.state.layout.Name);
  }

  handleCellTypeChange = (event:FormEvent<HTMLSelectElement>) => {
    
    if(!this.state.cellTypes)
      return;

    let index:number = Number(event.currentTarget.value);
    let selectedCellType:string = this.state.cellTypes[index];
    let arrKey = event.currentTarget.id.split("-");
    let layout = this.props.layout;
    if(layout != null){
      layout.Rows[parseInt(arrKey[1])].Cells[parseInt(arrKey[2])].CellType = selectedCellType
      this.props.updateLayout(layout);
    }
  }

  handleClassChange = (event:FormEvent<HTMLSelectElement>) => {
    if(!this.state.cellTypes)
      return;

    let selectedClassName:string = event.currentTarget.value;
    if(this.props.classes == null)
      return;
    
    let arrKey = event.currentTarget.id.split("-");
    let layout = this.props.layout;
    if(layout != null){
      layout.Rows[parseInt(arrKey[1])].Cells[parseInt(arrKey[2])].ClassName = selectedClassName
      this.props.updateLayout(layout);
    }
  }

  removeRow = (event:FormEvent<HTMLButtonElement>) => {
    let layout = this.props.layout;
    if(layout == null)
      return;

    let index:number = Number(event.currentTarget.id.split("-")[1]);
    layout.Rows.splice(index, 1);
    this.props.updateLayout(layout);
  }

  addRow = (event:FormEvent<HTMLButtonElement>) => {
    let layout = this.props.layout;
    if(layout == null)
      return;

    let row:LayoutRow = {
      Cells: [
      ]
    };
    if(layout.Rows.length > 0) {
      for(let i = 0; i < layout.Rows[0].Cells.length; i++)
      {
        row.Cells.push(
          {
            CellType:"Class",
            ClassName: this.props.classes && this.props.classes.length > 0? this.props.classes[0].ShortName : "",
          });
        }
      }
    layout.Rows.push(row);
    this.props.updateLayout(layout);
  }

  addColumn = (event:FormEvent<HTMLButtonElement>) => {
    let layout = this.props.layout;
    if(layout == null)
      return;
    
    layout.Rows.forEach((row) => {
      row.Cells.push({
        CellType:"Class",
        ClassName: this.props.classes && this.props.classes.length > 0? this.props.classes[0].ShortName : "",
      });
    });
    this.props.updateLayout(layout);
  }

  removeColumn = (event:FormEvent<HTMLButtonElement>) => {
    let layout = this.props.layout;
    if(layout == null)
      return;

    let index:number = Number(event.currentTarget.id.split("-")[1]);
    layout.Rows.forEach(row => {
      row.Cells.splice(index, 1);
    })
    this.props.updateLayout(layout);
  }

  handleShowStartTimeChange = (event: FormEvent<HTMLInputElement>) => {
    let layout = this.props.layout;
    if(layout == null)
      return;

      let arrKey = event.currentTarget.id.split("-");
      let cell = layout.Rows[parseInt(arrKey[1])].Cells[parseInt(arrKey[2])];
      let options = cell.Options;
      options = {
        ...options,
        ShowStartTime:event.currentTarget.checked,
      }
      cell.Options = options;
      this.props.updateLayout(layout);
    }

  render() {
    let props = this.props;
    let error = this.props.requestLayoutError;
    return(
    <div>
      <p><b>Välj layout</b></p>
      <BusyButton onClick={this.props.requestLayouts} isBusy={this.props.isRequestingLayouts}>Hämta layouter</BusyButton><br />
      { this.props.layouts ?
      <Fragment>
      <select
        defaultValue={ this.state.layout ? this.state.layout.Name : undefined}
        onChange={this.handleChange}
      >
      { this.props.layouts.map((item, key) => 
        <option key={key} value={key}>{item.Name}</option>
      )}
      </select>
      <BusyButton onClick={this.onSelectLayout} isBusy={props.isRequestingLayout}>Välj layout</BusyButton>
      </Fragment>
      :null}
      {error?
          <p style={{color: "red"}}>{ error.message }</p>
        :
        <Fragment>
          {props.layout?
          <div>
          <p>Layout: </p>
          <table>
            <tbody>
            {props.layout.Rows? props.layout.Rows.map((row, rowKey) => 
            <tr key={rowKey}>
              {row.Cells.map((cell, cellKey) =>
                <td key={cellKey}>

                  <select
                    onChange={this.handleCellTypeChange}
                    value={this.state.cellTypes.indexOf(cell.CellType)}
                    id={`selectType-${rowKey}-${cellKey}`}
                  >
                    { this.state.cellTypes.map((item, key) => 
                      <option key={key} value={key}>{item}</option>
                    )}
                  </select>
                  {
                    cell.CellType === "Class" && this.props.classes?
                  <Fragment>
                    <br />
                    <select
                      onChange={this.handleClassChange}
                      value={cell.ClassName}
                      id={`selectType-${rowKey}-${cellKey}`}
                    >
                    { this.props.classes.map((item, key) => 
                      <option key={key} value={item.ShortName}>{item.ShortName}</option>
                    )}
                    </select><br />
                    <input
                      type={"checkbox"}
                      id={`showStartTime-${rowKey}-${cellKey}`}
                      checked={cell.Options && cell.Options.ShowStartTime}
                      onChange={this.handleShowStartTimeChange}
                    />
                    Starttid
                  </Fragment>
                  :null}
                </td>
              )}
              <td>
                {props.layout?
                <button
                id={`removeRow-${props.layout.Rows.indexOf(row)}`}
                onClick={this.removeRow}
              >
                Remove row
              </button>
              :null}
              </td>
            </tr>
            ):null}
            <tr>
              {props.layout.Rows != null && props.layout.Rows.length > 0 ? props.layout.Rows[0].Cells.map((cell, key) =>
                <td
                  key={key}
                >
                  {props.layout?
                  <button
                    id={`removeColumn-${key}`}
                    onClick={this.removeColumn}
                  >
                    Remove column
                  </button>
                :null}
              </td>
              ):null}
              <td>
                <button
                  onClick={this.addRow}
                >
                  Add row
                </button>
                <br />
                <button
                  onClick={this.addColumn}
                >
                  Add column
                </button>
              </td>
              </tr>
            </tbody>
          </table>
        </div>
        :
        null
        }
        </Fragment>
      }
    </div>
    )
  }
}

const mapStateToProps = (state: any, ownProps: OwnProps): StateProps => {
  return {
    isRequestingLayouts: isRequestingLayouts(state),
    layouts: getLayouts(state),
    requestLayoutsError: getRequestLayoutsError(state),
  
    isRequestingLayout: isRequestingLayout(state),
    layout: getLayout(state),
    requestLayoutError: getRequestLayoutError(state),
  
    isUpdatingLayout: isUpdatingLayout(state),
    updateLayoutError: getUpdateLayoutError(state),
  
    classes: getClasses(state),
  }
}
   
const mapDispatchToProps = (dispatch: ThunkDispatch<State, {}, any> & Dispatch<Action>, ownProps: OwnProps): DispatchProps => {
  return {
    requestLayouts: () => dispatch(requestLayouts()),
    requestLayout: (layoutName:string) => dispatch(requestLayout(layoutName)),
    updateLayout: (layout:Layout) => dispatch(updateLayout(layout)),
  }
}

export default connect<StateProps, DispatchProps, OwnProps>(mapStateToProps, mapDispatchToProps)(LayoutsPanel);
