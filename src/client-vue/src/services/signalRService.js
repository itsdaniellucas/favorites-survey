import Vue from 'vue'
import * as signalr from '@microsoft/signalr'
import appConfig from '@/appConfig'
import store from '@/store'
import modules from '@/store/types/moduleTypes'
import actions from '@/store/types/actionTypes'

const signalRService = new Vue({
    data: () => ({
        baseUrl: `${appConfig.baseUrl}/SignalR`,
        connection: null,
    }),

    methods: {
        notifySurveyChanges() {
            this.connection.invoke('SendSurveyChanges').catch(_ => _);
        },
        onNotifiedSurveyChanges() {
            store.dispatch(`${modules.Survey}/${actions.GetStats}`);
        },
        initialize() {
            this.connection = new signalr.HubConnectionBuilder()
                                            .withUrl(this.baseUrl)
                                            .build();

            this.connection.on('ReceiveSurveyChanges', this.onNotifiedSurveyChanges);

            this.connection.start().catch(_ => _);
        },
    },
})

export default signalRService