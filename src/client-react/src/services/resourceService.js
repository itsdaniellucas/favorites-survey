import ajaxService from './ajaxService'

const controller = 'Resource'

function getQuestions() {
    return ajaxService.getSimple(`/${controller}/Questions`);
}

function getAnswers() {
    return ajaxService.getSimple(`/${controller}/Answers`);
}

const resourceService = {
    getQuestions: getQuestions,
    getAnswers: getAnswers,
}

export default resourceService