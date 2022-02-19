import {AuthStatuses} from '../../enums/Authorization'
import {Pages} from '../../enums/Pages'
import Message from '../../classes/Message'
import {MessageTypes} from '../../enums/Message'

export default {
    namespaced: true,
    state: {
        password: "",
        login: "",
        authUrl: "https://auth.meta-lucky.ru/auth/login",
        spamProtection: 0,
        spamDelay: 1000
    },
    mutations: {
        setLogin(state, login){
          state.login = login;
        },
        setPassword(state, password){
          state.password = password;
        },
        updateSpamProtection(state){
            state.spamProtection = Date.now() + state.spamDelay;
        }
    },
    actions: {
        toRegistration({dispatch}){
            dispatch("setPage", Pages.registration, {root: true});
        },
        async doAuthorization({state, dispatch, commit, rootState}){
            try {
                if(state.spamProtection > Date.now()) return;
                commit("updateSpamProtection")
                const responce = await fetch(state.authUrl,{
                    method: "POST",
                    headers:{
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        "Login": state.login,
                        "AppKey": rootState.appKey,
                        "Password": state.password
                    })
                })
                const result = await responce.json();
                switch (result.status) {
                    case AuthStatuses.ok:
                        dispatch("setToken", result.message, {root: true})
                        break;
                    case AuthStatuses.badApplication:
                        commit("showMessage", new Message("msg.badappkey", MessageTypes.error), {root: true});
                        break;
                    case AuthStatuses.userNotFound:
                        commit("showMessage", new Message("msg.usernotfound", MessageTypes.warning), {root: true});
                        //dispatch("setPage", Pages.authorization, {root: true})
                        break;
                    case AuthStatuses.emailNotConfirmed:
                        commit("setEmail", result.message, {root: true})
                        dispatch("setPage", Pages.emailConfirm, {root: true})
                        break;
                    default:
                        commit("showMessage", new Message("msg.unknownsvrerror", MessageTypes.error), {root: true});
                        break;
                }               
            } catch (e) {
                console.log(e)
            }
        }
    }
}