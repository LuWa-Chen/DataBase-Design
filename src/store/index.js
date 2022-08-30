import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    userID:'',
    userName:'',
    isUser:'',
    logo_type:''
  },
  getters: {
  },
  mutations: {
    changeValue_Name(state,newVal){
      state.userName = newVal
    },
    changeValue_ID(state,newVal){
      state.userID = newVal
    },
    change_flag(state,newVal){
      state.isUser = newVal
      if(newVal=='User'){
        state.logo_type = 'ProfilePhoto.jpg'
      }
      else if(newVal=="Publisher"){
        state.logo_type = 'LOGO.jpg'
      }
      else{
        state.logo_type = ''
      }
    },
  },
  actions: {
  },
  modules: {
  }
})