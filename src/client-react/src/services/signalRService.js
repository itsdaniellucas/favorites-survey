import * as signalr from '@microsoft/signalr'
import { fetch } from '../storeExternal'
import appConfig from '../appConfig'
import { getStats } from '../store/surveySlice'


const baseUrl = `${appConfig.baseUrl}/SignalR`
let connection = null

function notifySurveyChanges() {
    connection.invoke('SendSurveyChanges').catch(_ => _);
}

function onNotifiedSurveyChanges() {
    const store = fetch();
    store.dispatch(getStats())
}

function initialize() {
    connection = new signalr.HubConnectionBuilder().withUrl(baseUrl).build();

    connection.on('ReceiveSurveyChanges', onNotifiedSurveyChanges);

    connection.start().catch(_ => _);
}


const signalRService = {
    notifySurveyChanges: notifySurveyChanges,
    onNotifiedSurveyChanges: onNotifiedSurveyChanges,
    initialize: initialize,
}

export default signalRService