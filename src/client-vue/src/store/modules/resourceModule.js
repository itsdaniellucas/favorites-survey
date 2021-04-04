import actionTypes from '@/store/types/actionTypes'
import mutationTypes from '@/store/types/mutationTypes'
import resourceService from '@/services/resourceService'
import utility from '@/utility'

const resourceModule = {
    namespaced: true,
    state: {
        questions: [],
        answers: [],
    },
    getters: {
        questionsMap: state => {
            return utility.toObjectMap(state.questions);
        },
        answersMap: state => {
            return utility.toObjectMap(state.answers);
        },
        questionAnswersMap: state => {
            return utility.toObjectMap(state.answers, 'QuestionId', true);
        },
    },
    mutations: {
        [mutationTypes.SetQuestions] (state, payload) {
            state.questions = [...payload];
        },

        [mutationTypes.SetAnswers] (state, payload) {
            state.answers = [...payload];
        }
    },
    actions: {
        [actionTypes.GetQuestions] ({ commit }) {
            return resourceService.getQuestions().then(data => {
                commit(mutationTypes.SetQuestions, data);
            });
        },
        [actionTypes.GetAnswers] ({ commit }) {
            return resourceService.getAnswers().then(data => {
                commit(mutationTypes.SetAnswers, data);
            });
        }
    }
}

export default resourceModule