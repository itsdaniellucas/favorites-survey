import Vue from 'vue'
import AjaxService from '@/services/ajaxService'

const surveyService = new Vue({
    data: () => ({
        controller: 'Survey'
    }),

    methods: {
        getResponseStats() {
            return AjaxService.getSimple(`/${this.controller}/Stats`);
        },
        submitSurvey(model) {
            return AjaxService.post({
                url: `/${this.controller}/Submit`,
                data: model,
            });
        },
    }
})

export default surveyService