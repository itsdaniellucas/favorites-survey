import Vue from 'vue'
import AjaxService from '@/services/ajaxService'

const resourceService = new Vue({
    data: () => ({
        controller: 'Resource'
    }),

    methods: {
        getQuestions() {
            return AjaxService.getSimple(`/${this.controller}/Questions`);
        },
        getAnswers() {
            return AjaxService.getSimple(`/${this.controller}/Answers`);
        },
    }
})

export default resourceService