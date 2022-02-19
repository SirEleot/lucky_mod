import { createStore } from 'vuex'
import {Pages} from '../enums/Pages'
import {callAyncServerProc} from '../utils/ServeProc'
import authorization from './authorization'
import registration from './registration'
import emailconfirm from './emailconfirm'
import Message from '../classes/Message'
import {MessageTypes} from '../enums/Message'
import {ClienCefEvents, ServerProcs} from '../enums/Events'
import {AuthStatuses} from '../enums/Authorization'

export default createStore({
  namespaced: true,
  state: {
    page: Pages.Loading,
    login: "",
    email: "",
    appKey: "",
    refreshToken: null,
    accessToken: null,
    badSocialId: false,
    devPage: null,
    messages: [],
    updateTokenUrl: "https://auth.meta-lucky.ru/auth/tokenupdate"
  },
  mutations: {
    setPage(state, page){
      if(state.devPage)
        state.page = state.devPage;
      else
        state.page = state.badSocialId ? Pages.badSocial : page;
    },
    showMessage(state, message){
      state.messages.push(message);      
    },
    setEmail(state, email){
      state.email = email;
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
    },
    nextMessage(state){
      state.messages.shift();
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
    async setToken({state, commit, dispatch}, tokenData){
      try {
        const encodedTokenData = Buffer.from(tokenData, 'base64').toString('UTF-8');
        const tokensPair = JSON.parse(encodedTokenData);
        
        commit("setTokens", tokensPair);
        if(state.appKey !== state.accessToken.AppKey){ 
          commit("setPage", Pages.authorization);
          commit("showMessage", new Message("msg.badappkey", MessageTypes.error));
          return;
        }
        commit("updateUserData");
        const encodedAccessToken= Buffer.from(JSON.stringify(state.accessToken)).toString('base64');
        const result = await callAyncServerProc(ServerProcs.checkToken, encodedAccessToken);
        console.log(result);
        if(result == "ok"){
          commit("setPage", Pages.Loading);
          window.mp.trigger(ClienCefEvents.updateToken, tokenData);
          commit("showMessage", new Message("msg.authorized", MessageTypes.success, null, {login: state.login}));
        }else{
          window.mp.trigger(ClienCefEvents.restToken);
          dispatch("tryUpdateToken");          
        }        
      } catch (e){
        commit("setPage", Pages.authorization)
      }      
    },
    async tryUpdateToken({state, dispatch, commit}){
      try {
        const formData = new FormData();
        formData.append("refreshToken", Buffer.from(JSON.stringify(state.refreshToken)).toString('base64'));
        formData.append("appKey", state.appKey);
        const responce = await fetch(state.updateTokenUrl, {
          method: "POST",
          body: formData
        })
        const result = await responce.json();
        switch (result.status) {
          case AuthStatuses.ok:           
            if(process.env.NODE_ENV === 'development') return;
            dispatch("setToken", result.message);
            break;        
          default:
            commit("setPage", Pages.authorization)
            break;
        }
      } catch (error) {
        commit("setPage", Pages.authorization)        
      }
    }
  },
  getters:{
    message(state){
      if(state.messages.length > 0)
        return state.messages[0];
      else return undefined
    }
  },
  modules: {
    authorization,
    registration,
    emailconfirm
  }
})
