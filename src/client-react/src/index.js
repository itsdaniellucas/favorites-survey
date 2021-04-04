import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import 'c3/c3.min.css'
import App from './App';  
import reportWebVitals from './reportWebVitals';
import { BrowserRouter } from 'react-router-dom';

import store from './store'
import { Provider } from 'react-redux'
import { register } from './storeExternal'

import axios from 'axios'
import qs from 'qs'

axios.interceptors.request.use((cfg) => {
  cfg.paramsSerializer = params => {
    return qs.stringify(params, {
      encode: false
    });
  }

  return cfg;
}, (err) => Promise.reject(err));

register(store);

ReactDOM.render(
  <BrowserRouter>
    <Provider store={store}> 
      <App />
    </Provider>
  </BrowserRouter>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
