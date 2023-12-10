import { UserManager, WebStorageStateStore } from 'oidc-client';

const userManagerConfig = {
    authority: 'https://localhost:5002',
    client_id: 'Activities_ClientApp',
    redirect_uri: 'http://localhost:3000/signin-oidc',
    response_type: 'code',
    scope: 'openid profile activities.read activities.write manage',
    post_logout_redirect_uri: 'http://localhost:3000/signout-callback-oidc"',
    userStore: new WebStorageStateStore({ store: window.localStorage })
};

export const userManager = new UserManager(userManagerConfig);