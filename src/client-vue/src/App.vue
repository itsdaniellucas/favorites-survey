<template>
  <v-app>
    <v-app-bar app
              color="#00695c"
              fixed
              dark>
      <div class="d-flex align-center">
        <h2 class="ml-5">Favorites Survey</h2>
      </div>

      <v-spacer></v-spacer>
      <VoteDialog @on-save="onSaveVote" />
    </v-app-bar>

    <v-main>
      <v-progress-linear indeterminate color="cyan" v-show="isFetching" />
      <v-container>
        <router-view />
        <AlertBox />
      </v-container>
    </v-main>
  </v-app>
</template>

<script>

import AlertBox from '@/views/content/AlertBox'
import VoteDialog from '@/views/dialogs/VoteDialog'
import signalRService from '@/services/signalRService'
import { mapState } from 'vuex'
import modules from '@/store/types/moduleTypes'
import actions from '@/store/types/actionTypes'

export default {
  name: 'App',

  components: {
    AlertBox,
    VoteDialog
  },

  data: () => ({
    //
  }),

  computed: {
    ...mapState(['isFetching']),
    ...mapState(modules.Resource, [
      'questions',
      'answers'
    ]),
    ...mapState(modules.Survey, [
      'stats'
    ]),
  },

  methods: {
    onSaveVote($event) {
      this.$store.dispatch(`${modules.Survey}/${actions.Vote}`, $event.vote);
    },
  },

  created() {
    signalRService.initialize();

    if(this.questions.length == 0) {
      this.$store.dispatch(`${modules.Resource}/${actions.GetQuestions}`);
    }

    if(this.answers.length == 0) {
      this.$store.dispatch(`${modules.Resource}/${actions.GetAnswers}`);
    }

    if(this.stats.length == 0) {
      this.$store.dispatch(`${modules.Survey}/${actions.GetStats}`);
    }
  },
};
</script>

<style>
html {
  margin: 0;
  padding: 0;
  overflow-y: auto;
}
</style>
