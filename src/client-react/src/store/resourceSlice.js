import { createSlice, createAsyncThunk, createSelector } from '@reduxjs/toolkit'
import utility from '../utility'
import resourceService from '../services/resourceService'

const initialState = {
    questions: [],
    answers: [],
}

const resourceSlice = createSlice({
    name: 'resource',
    initialState,
    reducers: {
        setQuestions(state, action) {
            state.questions = [...action.payload];
        },
        setAnswers(state, action) {
            state.answers = [...action.payload];
        },
    },
})

export const getQuestions = createAsyncThunk('resource/getQuestions', async (_, { dispatch }) => {
    const questions = await resourceService.getQuestions();
    dispatch(resourceSlice.actions.setQuestions(questions));
    return questions;
})

export const getAnswers = createAsyncThunk('resource/getAnswers', async (_, { dispatch }) => {
    const answers = await resourceService.getAnswers();
    dispatch(resourceSlice.actions.setAnswers(answers));
    return answers;
})

export const selectAnswers = state => state.resource.answers;
export const selectQuestions = state => state.resource.questions;

export const selectQuestionsMap = createSelector([selectQuestions], questions => utility.toObjectMap(questions));
export const selectAnswersMap = createSelector([selectAnswers], answers => utility.toObjectMap(answers));
export const selectQuestionAnswersMap = createSelector([selectAnswers], answers => utility.toObjectMap(answers, 'QuestionId', true));

export const { setQuestions, setAnswers } = resourceSlice.actions

export default resourceSlice.reducer;