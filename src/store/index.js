import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    userID:'',
    userName:''
  },
  getters: {
  },
  mutations: {
    changeValue_Name(state,newVal){
      state.userName = newVal
    },
    changeValue_ID(state,newVal){
      state.userID = newVal
    }
  },
  actions: {
  },
  modules: {
  }
})