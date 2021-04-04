import { createSlice, createAsyncThunk, createSelector } from '@reduxjs/toolkit'
import utility from '../utility'
import surveyService from '../services/surveyService'
import signalRService from '../services/signalRService'

const initialState = {
    stats: []
}

const surveySlice = createSlice({
    name: 'survey',
    initialState,
    reducers: {
        setStats(state, action) {
            state.stats = [...action.payload];
        }
    }
})

export const getStats = createAsyncThunk('survey/getStats', async (_, { dispatch }) => {
    const stats = await surveyService.getResponseStats();
    dispatch(surveySlice.actions.setStats(stats));
    return stats;
})

export const vote = createAsyncThunk('survey/vote', async (payload) => {
    return surveyService.submitSurvey({ Responses: payload }).then(() => {
        setTimeout(() => {
            signalRService.notifySurveyChanges();
        }, 500);
    });
})


export const selectStats = state => state.survey.stats;
export const selectStatsMap = createSelector([selectStats], stats => utility.toObjectMap(stats, 'QuestionId', true));

export const { setStats } = surveySlice.actions;

export default surveySlice.reducer;