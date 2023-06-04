import 'bootstrap/dist/css/bootstrap.min.css';
import './index.css';
import ReactDOM from 'react-dom/client';
import React from 'react';
import App from './App';
import store from "./store/storeConfig";
import {Provider} from "react-redux";

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);

root.render(
    <Provider store={store}>
        <App/>
    </Provider>
);
