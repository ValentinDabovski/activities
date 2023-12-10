import React from 'react';
import ReactDOM from 'react-dom/client';
import 'react-calendar/dist/Calendar.css'
import './app/layout/styles.css'
import 'semantic-ui-css/semantic.min.css'
import reportWebVitals from './reportWebVitals';
import { StoreContext, store } from './app/stores/store';
import { RouterProvider } from 'react-router-dom';
import { router } from './app/router/Routes';
import { WebStorageStateStore } from 'oidc-client-ts';
import { AuthProvider } from 'react-oidc-context';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

const oidcConfig = {
    authority: 'https://localhost:5002',
    client_id: 'Activities_ClientApp',
    redirect_uri: 'http://localhost:3000/signin-oidc',
    response_type: 'code',
    scope: 'openid profile activities.read activities.write manage',
    post_logout_redirect_uri: 'http://localhost:3000/signout-callback-oidc"',
    userStore: new WebStorageStateStore({ store: window.localStorage })
}
root.render(
  <StoreContext.Provider value={store}>
      <AuthProvider {...oidcConfig}>
          <RouterProvider router={router} />
      </AuthProvider>
    
  </StoreContext.Provider>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
