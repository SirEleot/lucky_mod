import {AuthStatuses} from '../../enums/Authorization'
import {Pages} from '../../enums/Pages'

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
        async doAuthorization({state, dispatch, commit}, form){
            try {
                if(state.spamProtection > Date.now()) return;
                commit("updateSpamProtection")
                const formData = new FormData(form);
                const responce = await fetch(state.authUrl,{
                    method: "POST",
                    body: formData    
                })
                const result = await responce.json();
                switch (result.status) {
                    case AuthStatuses.ok:
                        dispatch("setPage", Pages.loading, {root: true})
                        break;
                    case AuthStatuses.userNotFound:
                        dispatch("setPage", Pages.registration, {root: true})
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