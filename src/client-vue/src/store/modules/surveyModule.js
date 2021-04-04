import actionTypes from '@/store/types/actionTypes'
import mutationTypes from '@/store/types/mutationTypes'
import surveyService from '@/services/surveyService'
import signalRService from '@/services/signalRService'
import utility from '@/utility'

const surveyModule = {
    namespaced: true,
    state: {
        stats: [],
    },
    mutations: {
        [mutationTypes.SetStats] (state, payload) {
            state.stats = [...payload];
        }
    },
    getters: {
        statsMap: state => {
            return utility.toObjectMap(state.stats, 'QuestionId', true);
        }
    },
    actions: {
        [actionTypes.Vote] (_, payload) {
            return surveyService.submitSurvey({ Responses: payload }).then(() => {
                setTimeout(() => {
                    signalRService.notifySurveyChanges();
                }, 500); // give precomputation time to finish
            });
        },
        [actionTypes.GetStats] ({ commit }) {
            return surveyService.getResponseStats().then(data => {
                commit(mutationTypes.SetStats, data);
            });
        }
    }
}

export default surveyModule