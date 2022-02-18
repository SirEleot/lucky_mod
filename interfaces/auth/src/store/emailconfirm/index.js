import {AuthStatuses} from '../../enums/Authorization'
import {Pages} from '../../enums/Pages'

export default {
    namespaced: true,
    state: {
        code: "",
        confirmUrl: "https://auth.meta-lucky.ru/auth/emailconfirm",
        spamProtection: 0,
        spamDelay: 1000
    },
    mutations: {
        setCode(state, login){
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
        async doEmailConfirm({state, dispatch, commit}, form){
            try {
                if(state.spamProtection > Date.now()) return;
                commit("updateSpamProtection")
                const formData = new FormData(form);
                const responce = await fetch(state.confirmUrl,{
                    method: "POST",
                    body: formData    
                })
                const result = await responce.json();
                console.log(result);
                switch (result.status) {
                    case AuthStatuses.ok:
                        dispatch("setPage", Pages.loading, {root: true})
                        break;
                    case AuthStatuses.badEmailCode:
                        console.log(result.status)
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