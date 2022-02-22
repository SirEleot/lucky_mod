<template>
    <div class="chselect fullscreen">
        <div class="chselect-list">
            <Character class="chselect-list_character" v-for="(character, index) in characterList" :key="index" :character="character" @onselect="doCharacterSelect(index)"/>
        </div>
    </div>
</template>

<script>
import { mapActions, mapState } from 'vuex'
import Character from '../components/CharacterSelectItem.vue'

    export default {
        name: 'App',
        props: {

        },
        computed: {
            ...mapState("characterSelect", ["characters"]),
            characterList(){
                const list = [...this.characters];
                if(list.length < this.minLength){
                    for (let index = list.length; index < this.minLength; index++) {
                        list.push(null);                        
                    }
                }
                return list;
            }
        },
        data() {
            return {
                minLength: 5
            }
        },
        methods: {
            ...mapActions("characterSelect", ["doCharacterSelect"])            
        },
        components: {
            Character
        }
    }
</script>

<style lang="scss">
    .chselect {
        padding-left: 1vw;
        &-list{
            width: 15vw;
            height: 100vh;
        }
    }
</style>