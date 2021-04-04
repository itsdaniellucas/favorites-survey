import { configureStore } from '@reduxjs/toolkit'

import globalReducer from './store/globalSlice'
import alertReducer from './store/alertSlice'
import resourceReducer from './store/resourceSlice'
import surveyReducer from './store/surveySlice'


const store = configureStore({
    reducer: {
        resource: resourceReducer,
        survey: surveyReducer,
        global: globalReducer,
        alert: alertReducer,
    }
})

export default store