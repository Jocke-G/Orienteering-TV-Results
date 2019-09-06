import React from 'react';
import { connect } from "react-redux";
import { AppState } from "./store";
import './App.css';

import ClassResult from './components/ClassResultView';

interface AppProps {
}

class App extends React.Component<AppProps> {
  state = {
    message: ""
  };

  render() {
    return (
      <div className="App">
        <ClassResult />
      </div>
    );
  }
}

const mapStateToProps = (state: AppState) => ({
});

export default connect(
  mapStateToProps,
  {}
)(App);
