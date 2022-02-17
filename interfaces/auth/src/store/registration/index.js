export default {
    namespaced: true,
    state: {
        login: "",
        email: "",
        password: "",
        congirmPassword: "",
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
        doRegistration({state, dispatch}){

        }
    },
    modules: {
    }
}