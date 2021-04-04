import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'

const initialState = {
    visible: false,
    isProgress: true,
    text: 'Saving changes made...',
    severity: 'info',
    timeout: 5000,
    timeoutFn: null,
    type: 'saving',
    autoFade: false,
}

const selectState = state => state.alert;

const saving = createAsyncThunk('alert/saving', (payload, { dispatch, getState }) => {
    let state = selectState(getState());

    if(state.timeoutFn) {
        clearTimeout(state.timeoutFn);
    }

    dispatch(alertSlice.actions.setSaving(payload));

    state = selectState(getState());

    if(state.autoFade) {
        let timeoutFn = setTimeout(() => {
            dispatch(alertSlice.actions.setAlertVisible(false));
        }, state.timeout);
        return timeoutFn;
    } else {
        return state.timeoutFn;
    }
})

const success = createAsyncThunk('alert/success', (payload, { dispatch, getState }) => {
    let state = selectState(getState());

    if(state.timeoutFn) {
        clearTimeout(state.timeoutFn);
    }

    dispatch(alertSlice.actions.setSuccess(payload));

    state = selectState(getState());

    if(state.autoFade) {
        let timeoutFn = setTimeout(() => {
            dispatch(alertSlice.actions.setAlertVisible(false));
        }, state.timeout);
        return timeoutFn;
    } else {
        return state.timeoutFn;
    }
})

const error = createAsyncThunk('alert/error', (payload, { dispatch, getState }) => {
    let state = selectState(getState());

    if(state.timeoutFn) {
        clearTimeout(state.timeoutFn);
    }

    dispatch(alertSlice.actions.setError(payload));

    state = selectState(getState());

    if(state.autoFade) {
        let timeoutFn = setTimeout(() => {
            dispatch(alertSlice.actions.setAlertVisible(false));
        }, state.timeout);
        return timeoutFn;
    } else {
        return state.timeoutFn;
    }
})

const alertSlice = createSlice({
    name: 'alert',
    initialState,
    reducers: {
        setAlertVisible(state, action) {
            state.visible = action.payload;
        },
        setTimeoutFn(state, action) {
            state.timeoutFn = action.payload;
        },
        setSaving(state, action) {
            let defaultPayload = {
                text: '',
                autoFade: false,
            };

            let newPayload = { ...defaultPayload, ...action.payload };

            state.type = 'saving';
            state.visible = true;
            state.isProgress = true;
            state.text = newPayload.text || 'Saving changes made...';
            state.severity = 'info';
            state.autoFade = newPayload.autoFade;
        },
        setSuccess(state, action) {
            let defaultPayload = {
                text: '',
                autoFade: true,
            };

            let newPayload = { ...defaultPayload, ...action.payload };

            state.type = 'success';
            state.visible = true;
            state.isProgress = false;
            state.text = newPayload.text || 'Changes have been saved!';
            state.severity = 'success';
            state.autoFade = newPayload.autoFade;
        },
        setError(state, action) {
            let defaultPayload = {
                text: '',
                autoFade: true,
            };

            let newPayload = { ...defaultPayload, ...action.payload };

            state.type = 'error';
            state.visible = true;
            state.isProgress = false;
            state.text = newPayload.text || 'An error occured while saving!';
            state.severity = 'error';
            state.autoFade = newPayload.autoFade;
        }
    },
    extraReducers: {
        [saving.fulfilled]: (state, action) => {
            state.timeoutFn = action.payload;
        },
        [success.fulfilled]: (state, action) => {
            state.timeoutFn = action.payload;
        },
        [error.fulfilled]: (state, action) => {
            state.timeoutFn = action.payload;
        },
    },
})

export { saving, success, error, selectState }

export const { setAlertVisible, setTimeoutFn, setSaving, setSuccess, setError } = alertSlice.actions;

export default alertSlice.reducer;