import Vue from 'vue'
import App from './App.vue'
import router from '@/router'
import store from '@/store'
import vuetify from './plugins/vuetify';
import axios from 'axios'
import qs from 'qs'

import 'c3/c3.min.css'

Vue.config.productionTip = false

axios.interceptors.request.use((cfg) => {
  cfg.paramsSerializer = params => {
    return qs.stringify(params, {
      encode: false
    });
  }

  return cfg;
}, (err) => Promise.reject(err));

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
