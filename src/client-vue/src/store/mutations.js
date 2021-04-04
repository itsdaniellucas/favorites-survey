import mutationTypes from '@/store/types/mutationTypes'

const mutations = {
    [mutationTypes.SetIsFetching] (state, payload) {
        state.isFetching = payload;
    }
}

export default mutations