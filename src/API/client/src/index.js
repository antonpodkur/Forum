import React, {createContext} from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import userStore from "./store/UserStore";

export const Context = createContext({userStore});

ReactDOM.render(
  <React.StrictMode>
      <Context.Provider value={{userStore}}>
          <App />
      </Context.Provider>
  </React.StrictMode>,
  document.getElementById('root')
);

//TODO: check how everything works: tokens, add services (auth), check if store is working, add routes

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals