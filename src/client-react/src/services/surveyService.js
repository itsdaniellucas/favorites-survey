import ajaxService from './ajaxService'

const controller = 'Survey'

function getResponseStats() {
    return ajaxService.getSimple(`/${controller}/Stats`);
}

function submitSurvey(model) {
    return ajaxService.post({
        url: `/${controller}/Submit`,
        data: model,
    })
}

const surveyService = {
    getResponseStats: getResponseStats,
    submitSurvey: submitSurvey,
}

export default surveyService