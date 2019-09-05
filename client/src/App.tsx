import React from 'react';
import { connect } from "react-redux";
import { AppState } from "./store";
import './App.css';

import { sendMessage } from "./actions/sendMessage";

interface AppProps {
  sendMessage: typeof sendMessage;
}

class App extends React.Component<AppProps> {
  state = {
    message: ""
  };

  handleAddMessage = () => {
    this.props.sendMessage("Sample message");
    this.setState({ input: "" });
  };

  sendMessage = (message: string) => {
    this.props.sendMessage(message,);
    this.setState({ message: "" });
  };

  render() {
    return (
      <div className="App">
      </div>
    );
  }
}

const mapStateToProps = (state: AppState) => ({
});

export default connect(
  mapStateToProps,
  {sendMessage}
)(App);
