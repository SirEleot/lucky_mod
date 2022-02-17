import { createStore } from 'vuex'
import {Pages} from '../enums/Pages'
import {callAyncServerProc} from '../utils/ServeProc'
import authorization from './authorization'
import registration from './registration'

export default createStore({
  namespaced: true,
  state: {
    page: Pages.registration,
    login: "",
    email: "",
    emailConfirmCode: "",
    appKey: "",
    refreshToken: null,
    accessToken: null,
    badSocialId: false
  },
  mutations: {
    setPage(state, page){
      state.page = state.badSocialId ? Pages.badSocial : page;
    },   
    setAppKey(state, appkey){
      state.appKey = appkey;
    },
    setTokens(state, payload){
      state.refreshToken = payload.RefreshToken;
      state.accessToken = payload.AccessToken;
    },
    updateUserData(state){
      state.login = state.refreshToken.Login || "";
      state.email = state.refreshToken.Email  || "";
    },
    setWrongSocial(state){
      state.badSocialId = true;
    }
  },
  actions: {
    setPage({commit}, page){
      commit("setPage", page);
    },
    setAppKey({commit}, appkey){
      commit("setAppKey", appkey);
    },
    setWrongSocial({commit}){
      commit("setPage", Pages.badSocial);
      commit("setWrongSocial");
    },
    async setToken({state, commit}, tokenData){
      try {
        const encodedTokenData = Buffer.from(tokenData, 'base64').toString('UTF-8');
        const tokensPair = JSON.parse(encodedTokenData);
        
        commit("setTokens", tokensPair);
        if(state.appKey !== state.accessToken.AppKey){ 
          commit("setPage", Pages.authorization)
          return;
        }
        commit("updateUserData");
        const result = await callAyncServerProc("auth:player:token:check", Buffer.from(JSON.stringify(state.accessToken)).toString('base64'));        
        if(result == "ok"){
          commit("setPage", Pages.Loading)
        }else{          
          window.mp.trigger("cef:auth:token:reset");
          commit("setPage", Pages.authorization)
        }        
      } catch (e){
        commit("setPage", Pages.authorization)
      }      
    }
  },
  modules: {
    authorization,
    registration
  }
})
