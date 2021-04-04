import React from 'react'
import { Route } from 'react-router-dom'

export default function CustomRoute({ component: Component, meta, ...rest }) {
    let body = <Component meta={meta} />;

    return (
        <Route { ...rest }>
            {body}
        </Route>
    )
}