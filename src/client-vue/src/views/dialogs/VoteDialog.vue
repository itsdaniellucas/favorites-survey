<template>
    <v-dialog v-model="visible"
                persistent
                max-width="700">
        <template v-slot:activator="{ on, attrs }">
            <v-btn fab
                    small
                    :color="'primary'"
                    v-on="on"
                    v-bind="attrs">
                <v-icon dark>
                    mdi-vote
                </v-icon>
            </v-btn>
        </template>
        <v-card v-if="questions.length > 0 && answers.length > 0 && response.length > 0">
            <v-card-title class="headline">
                <span>Vote</span>
            </v-card-title>
            <v-card-text class="mb-0 pb-0">
                <v-divider />
                <v-row class="mt-2">
                    <v-col cols="10">
                        <v-progress-linear :value="progress" color="#00695c" class="mt-3"></v-progress-linear>
                    </v-col>
                    <v-col cols="2">
                        <v-chip color="#00695c"
                                label
                                text-color="white">
                            <v-icon left>
                                mdi-check-bold
                            </v-icon>
                            {{ current }} / {{ questionsLength }}
                        </v-chip>
                    </v-col>
                </v-row>
                <v-row class="mt-2">
                    <v-col cols="12">
                        <v-radio-group v-model="response[current - 1].AnswerId">
                            <template v-slot:label>
                                <h3>{{ questionsMap[current].Name }}</h3>
                            </template>
                            <v-radio v-for="i in questionAnswersMap[current]" :value="i.Id" :key="i.Id">
                                <template v-slot:label>
                                    {{ i.Name }}
                                </template>
                            </v-radio>
                        </v-radio-group>
                    </v-col>
                </v-row>
                <v-row class="mb-5">
                    <v-spacer></v-spacer>
                    <v-btn class="mx-2" fab small :disabled="onFirstQuestion" @click="previous"><v-icon>mdi-arrow-left</v-icon></v-btn>
                    <v-btn class="mx-2" fab small :disabled="onLastQuestion" @click="next"><v-icon>mdi-arrow-right</v-icon></v-btn>
                </v-row>
            </v-card-text>
            <v-card-actions class="mt-0 pt-0">
                <v-spacer></v-spacer>
                <v-btn color="error darken-1"
                        text
                        @click="onClose">
                    Cancel
                </v-btn>
                <v-btn color="success darken-1"
                        text
                        @click="onSave"
                        :disabled="!onLastQuestion">
                    Save
                </v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script>
    import modules from '@/store/types/moduleTypes'
    import { mapState, mapGetters } from 'vuex'

    export default {
        name: 'VoteDialog',

        data: () => ({
            current: 1,
            visible: false,
            response: [],
        }),

        computed: {
            ...mapState(modules.Resource, [
                'questions',
                'answers',
            ]),
            ...mapGetters(modules.Resource, [
                'questionsMap',
                'answersMap',
                'questionAnswersMap',
            ]),
            questionsLength() {
                return this.questions.length;
            },
            onFirstQuestion() {
                return this.current == 1;
            },
            onLastQuestion() {
                return this.current == this.questionsLength;
            },
            progress() {
                return Math.round((this.current / this.questionsLength) * 100);
            },
        },

        watch: {
            visible() {
                this.initialize();
            },
        },

        methods: {
            next() {
                if(!this.onLastQuestion) {
                    this.current++;
                }
            },

            previous() {
                if(!this.onFirstQuestion) {
                    this.current--;
                }
            },

            onSave() {
                this.$emit('on-save', {
                    vote: this.response,
                });
                this.onClose();
            },

            onClose() {
                this.visible = false;
            },

            initialize() {
                this.current = 1;
                this.response = [];
                if(this.questions && this.answers && this.answers.length > 0 && this.questions.length > 0) {
                    this.response = this.questions.map(i => {
                        let firstAnswerId = 0;
                        if(this.questionAnswersMap[i.Id][0]) {
                            firstAnswerId = this.questionAnswersMap[i.Id][0].Id;
                        }

                        return {
                            QuestionId: i.Id,
                            AnswerId: firstAnswerId,
                        }
                    });
                }
            },
        },

        mounted() {
            this.initialize();
        },
    }
</script>

<style scoped>

</style>