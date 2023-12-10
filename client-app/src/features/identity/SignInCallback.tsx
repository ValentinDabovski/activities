import React, { useEffect } from 'react';
import { userManager } from '../../app/identity/userManagerStore';
import { useNavigate } from 'react-router-dom';
import { useStore } from '../../app/stores/store';

const SignInCallback = () => {
    const navigate = useNavigate();

    const { userStore } = useStore()
    
    useEffect(() => {
        userManager.signinRedirectCallback()
            .then(() => {
                userStore.setIsUserLoggedIn(true)
                navigate('/');
            })
            .catch(error => {
                console.error('Problem with sign-in callback:', error);
            });
    }, [navigate, userManager]);

    // Optionally, render a loading indicator or a message
    return <div>Signing in, please wait...</div>;
};

export default SignInCallback;