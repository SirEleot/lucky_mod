import {AuthStatuses} from '../../enums/Authorization'
import {Pages} from '../../enums/Pages'
import Message from '../../classes/Message'
import {MessageTypes} from '../../enums/Message'

export default {
    namespaced: true,
    state: {
        login: "",
        email: "",
        password: "",
        congirmPassword: "",
        spamProtection: 0,
        spamDelay: 1000,
        regUrl: "https://auth.meta-lucky.ru/auth/register"
    },
    mutations: {
        setLogin(state, login){
          state.login = login;
        },
        setPassword(state, password){
          state.password = password;
        },
        setEmail(state, email){
            state.email = email;
          },
        setConfirmPassword(state, congirmPassword){
          state.congirmPassword = congirmPassword;
        },
        updateSpamProtection(state){
            state.spamProtection = Date.now() + state.spamDelay;
        }
    },
    actions: {
      toAuthorization({dispatch}){
        dispatch("setPage", Pages.authorization, {root: true})
      },
      async doRegistration({state, dispatch, commit, rootState}){
        try {
          if(state.spamProtection > Date.now()) return;
            commit("updateSpamProtection")
            const data = {
              "Login": state.login,
              "Email": state.email,
              "AppKey": rootState.appKey,
              "Password": state.password,
              "ConfirmPassword": state.congirmPassword,              
              "Promocode": ""
            };
            const responce = await fetch(state.regUrl,{
              method: "POST",
              headers:{
                "Content-Type": "application/json"
              },
              body: JSON.stringify(data)    
            });
            const result = await responce.json();
            switch (result.status) {
                case AuthStatuses.ok:
                  dispatch("setToken", result.message, {root: true})
                  break;                
                case AuthStatuses.emailNotConfirmed:
                  commit("setEmail", result.message, {root: true})
                  dispatch("setPage", Pages.emailConfirm, {root: true})
                  break; 
                default:
                  commit("showMessage", new Message("auth.unknownerrormsg", MessageTypes.error), {root: true})
                  break;
            }               
        } catch (e) {
            console.log(e)
        }
      }
    },
    modules: {
    }
}