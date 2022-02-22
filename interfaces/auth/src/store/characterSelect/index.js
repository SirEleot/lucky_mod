import {Pages} from '../../enums/Pages'
import {ClienCefEvents} from '../../enums/Events'

export default {
    namespaced: true,
    state: {
        characters: [],
        selectedIndex: 0
    },
    mutations: {
        setCharacters(state, characters){
            state.characters = characters;
        },
        setSelectedCharacterIndex(state, selectedIndex){
            state.selectedIndex = selectedIndex;
        }
    },
    actions: {
        open({dispatch, commit}, characters){
            commit("setCharacters", characters);
            dispatch("setPage", Pages.characterSelect, {root: true});
        },
        doCharacterSelect({commit, state, dispatch}, characterIndex){ 
            commit("setSelectedCharacterIndex", characterIndex)
            const character = state.characters[characterIndex];
            if(character)
                window.mp.trigger(ClienCefEvents.selectCharacter, character.Id);
            else
                dispatch("confirmationDialog/show", {message: "confirm.chcreate", callback: "characterSelect/doCharacterCreate"}, {root: true})
        },
        doCharacterCreate({dispatch}){            
            dispatch("setPage", Pages.loading, {root: true});
            window.mp.trigger(ClienCefEvents.createCharacter)
        }
    },
    modules: {
    }
}