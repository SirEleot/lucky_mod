export default {
    namespaced: true,
    state: {
        title: "confirm.title",
        message: "",
        callback: "",
        visible: false,
    },
    mutations: {
    },
    actions: {
        show({state}, {message, callback}){
            state.message = message;
            state.callback = callback;
            state.visible = true;
        },
        close({state, dispatch}, result){            
            if(result)
                dispatch(state.callback, null, {root: true})
            state.message = "";
            state.callback = "";
            state.visible = false;
        }
    },
    modules: {
    }
}