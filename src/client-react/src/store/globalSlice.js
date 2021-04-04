import { createSlice } from '@reduxjs/toolkit'

const initialState = {
    isFetching: false,
}

const globalSlice = createSlice({
    name: 'global',
    initialState,
    reducers: {
        setIsFetching(state, action) {
            state.isFetching = action.payload;
        }
    }
})

export const selectIsFetching = state => state.global.isFetching;
export const { setIsFetching } = globalSlice.actions;

export default globalSlice.reducer;