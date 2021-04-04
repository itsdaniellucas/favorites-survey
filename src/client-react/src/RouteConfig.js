import React, { useEffect } from 'react'
import { Switch, Redirect, useLocation } from 'react-router-dom'
import Home from './views/routes/Home'
import CustomRoute from './CustomRoute'
import Error from './views/Error'
import appConfig from './appConfig'

export default function RouteConfig(props) {
    const loc = useLocation();

    const errorMetas = {
        ['401']: {
            imageSrc: appConfig.errorImages[401],
            text: '401',
            subTitle: 'Unauthorized',
            content: 'You are unauthorized to access this resource, please make sure to login first.',
            redirectRoute: '/',
            redirectText: 'Home',
        },
        ['403']: {
            imageSrc: appConfig.errorImages[403],
            text: '403',
            subTitle: 'Forbidden',
            content: 'You do not have permission to access this resource.',
            redirectRoute: '/',
            redirectText: 'Home',
        },
        ['404']: {
            imageSrc: appConfig.errorImages[404],
            text: '404',
            subTitle: 'Not Found',
            content: 'The page or resource that you are trying to access does not exist.',
            redirectRoute: '/',
            redirectText: 'Home',
        },
        ['500']: {
            imageSrc: appConfig.errorImages[500],
            text: '500',
            subTitle: 'Internal Server Error',
            content: 'Oops! Something went wrong.',
            redirectRoute: '/',
            redirectText: 'Home',
        }
    }

    useEffect(() => {
        const routeName = {
            ['/']: 'Dashboard',
            ['/stats']: 'Statistics',
            ['/error/401']: 'Unauthorized',
            ['/error/403']: 'Forbidden',
            ['/error/404']: 'Not Found',
            ['/error/500']: 'Internal Server Error'
        };

        document.title = `${routeName[loc.pathname]} | Favorites Survey`;
    });

    return (
        <Switch>
            <CustomRoute exact path="/" component={Home} />
            <CustomRoute path="/error/401" component={Error} meta={errorMetas[401]} />
            <CustomRoute path="/error/403" component={Error} meta={errorMetas[403]} />
            <CustomRoute path="/error/404" component={Error} meta={errorMetas[404]} />
            <CustomRoute path="/error/500" component={Error} meta={errorMetas[500]} />
            <Redirect to="/error/404" />
        </Switch>
    )
}