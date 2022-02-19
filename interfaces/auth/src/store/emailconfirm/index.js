import {AuthStatuses} from '../../enums/Authorization'
import Message from '../../classes/Message'
import {MessageTypes} from '../../enums/Message'

export default {
    namespaced: true,
    state: {
        code: "",
        confirmUrl: "https://auth.meta-lucky.ru/auth/emailconfirm",
        spamProtection: 0,
        spamDelay: 1000
    },
    mutations: {
        setCode(state, code){
          state.code = code;
        },
        setPassword(state, password){
          state.password = password;
        },
        updateSpamProtection(state){
            state.spamProtection = Date.now() + state.spamDelay;
        }
    },
    actions: {        
        async doEmailConfirm({state, dispatch, commit, rootState}){
            try {
                if(state.spamProtection > Date.now()) return;
                commit("updateSpamProtection")
                const data = {
                    "AppKey": rootState.appKey,
                    "Code": state.code,
                    "Email": rootState.email
                };
                const responce = await fetch(state.confirmUrl,{
                    method: "POST",
                    headers:{
                      "Content-Type": "application/json"
                    },
                    body: JSON.stringify(data)    
                })
                const result = await responce.json();
                switch (result.status) {
                    case AuthStatuses.ok:
                        dispatch("setToken", result.message, {root: true})
                        break;
                    case AuthStatuses.badEmailCode:
                        commit("showMessage", new Message("msg.bademailcode", MessageTypes.warning), {root: true})
                        break;                    
                    default:
                        break;
                }               
            } catch (e) {
                console.log(e)
            }
        }
    }
}