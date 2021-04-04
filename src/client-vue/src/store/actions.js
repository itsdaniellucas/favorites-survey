import actionTypes from '@/store/types/actionTypes'
import mutationTypes from '@/store/types/mutationTypes'

const actions = {
    [actionTypes.Fetching] ({ commit }, payload) {
        commit(mutationTypes.SetIsFetching, payload);
    }
}

export default actions