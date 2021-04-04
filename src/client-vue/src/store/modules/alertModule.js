import actionTypes from '@/store/types/actionTypes'
import mutationTypes from '@/store/types/mutationTypes'

const alertModule = {
    namespaced: true,
    state: {
        visible: false,
        isProgress: true,
        icon: '',
        text: 'Saving changes made...',
        isWhiteText: true,
        color: 'info',
        timeout: 5000,
        timeoutFn: null,
        type: 'saving',
        autoFade: false,
    },
    mutations: {
        [mutationTypes.SetAlertVisible] (state, payload) {
            state.visible = payload;
        },

        [mutationTypes.SetTimeoutFn] (state, payload) {
            state.timeoutFn = payload;
        },
        
        [mutationTypes.SetSaving] (state, payload) {
            let defaultPayload = {
                text: '',
                autoFade: false,
            };

            let newPayload = { ...defaultPayload, ...payload };

            state.type = 'saving';
            state.visible = true;
            state.isProgress = true;
            state.icon = '';
            state.text = newPayload.text || 'Saving changes made...';
            state.isWhiteText = true;
            state.color = 'info';
            state.autoFade = newPayload.autoFade;
        },
        [mutationTypes.SetSuccess] (state, payload) {
            let defaultPayload = {
                text: '',
                autoFade: true,
            };

            let newPayload = { ...defaultPayload, ...payload };

            state.type = 'success';
            state.visible = true;
            state.isProgress = false;
            state.icon = 'mdi-check-circle-outline';
            state.text = newPayload.text || 'Changes have been saved!';
            state.isWhiteText = true;
            state.color = 'success';
            state.autoFade = newPayload.autoFade;
        },
        [mutationTypes.SetError] (state, payload) {
            let defaultPayload = {
                text: '',
                autoFade: true,
            };

            let newPayload = { ...defaultPayload, ...payload };

            state.type = 'error';
            state.visible = true;
            state.isProgress = false;
            state.icon = 'mdi-alert-circle-outline';
            state.text = newPayload.text || 'An error occured while saving!';
            state.isWhiteText = true;
            state.color = 'error';
            state.autoFade = newPayload.autoFade;
        }
    },
    actions: {
        [actionTypes.Saving] ({ commit, state }, payload) {
            if(state.timeoutFn) {
                clearTimeout(state.timeoutFn);
            }

            commit(mutationTypes.SetSaving, payload);
            
            if(state.autoFade) {
                let timeout = setTimeout(() => {
                    commit(mutationTypes.SetAlertVisible, false);
                }, state.timeout);
    
                commit(mutationTypes.SetTimeoutFn, timeout);
            }
        },
        [actionTypes.Success] ({ commit, state }, payload) {
            if(state.timeoutFn) {
                clearTimeout(state.timeoutFn);
            }

            commit(mutationTypes.SetSuccess, payload);

            if(state.autoFade) {
                let timeout = setTimeout(() => {
                    commit(mutationTypes.SetAlertVisible, false);
                }, state.timeout);
                
                commit(mutationTypes.SetTimeoutFn, timeout);
            }
        },
        [actionTypes.Error] ({ commit, state }, payload) {
            if(state.timeoutFn) {
                clearTimeout(state.timeoutFn);
            }

            commit(mutationTypes.SetError, payload);

            if(state.autoFade) {
                let timeout = setTimeout(() => {
                    commit(mutationTypes.SetAlertVisible, false);
                }, state.timeout);
                
                commit(mutationTypes.SetTimeoutFn, timeout);
            }
        }   
    },
}


export default alertModule