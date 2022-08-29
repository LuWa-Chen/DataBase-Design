import Vue from 'vue'
import axios from 'axios'
import App from './App.vue'
import router from './router'
import layer from 'vue-layer';
import 'vue-layer/lib/vue-layer.css';
import store from './store'
import '../public/reset.css'
import './assets/iconslab/iconfont.css'
import './assets/font/iconfont.css';
import VueResource from 'vue-resource'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import Chat from 'jwchat';
import VueParticles from 'vue-particles'

Vue.use(VueParticles)
Vue.use(Chat)
Vue.use(VueResource)
Vue.use(ElementUI)

Vue.prototype.$axios = axios
Vue.config.productionTip = false
Vue.prototype.$layer = layer(Vue);


new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
