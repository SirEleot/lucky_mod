<template>
    <div class="chselect-item" @click="select">
        <template v-if="character">
           <div class="chselect-item_id">{{character.Id}}</div>
           <div class="chselect-item_fname">{{character.FirstName}}</div>
           <div class="chselect-item_lname">{{character.LastName}}</div>
           <div class="chselect-item_cache">{{character.Cache}}</div>
           <div class="chselect-item_bank">{{character.Bank}}</div>
        </template>
         <template v-else>
           <div class="chselect-item_new flec_ac_jc">new character</div>
        </template>
        
    </div>
</template>

<script>
    export default {
        name: 'App',
        props: {
            character: Object,
            default: null
        },
        computed: {

        },
        data() {
            return {

            }
        },
        methods: {
            select(){
                this.$emit("onselect")
            }
        },
        components: {

        }
    }
</script>

<style lang="scss">
    .chselect-item {
        color: #000;
        width: 100%;
        height: 18vh;
        border: 1px solid #000;
        margin: 1vh 0;
        position: relative;
        &_new{
            display: flex;
            width: 100%;
            height: 100%;
            align-items: center;
            justify-content: center;
        }
        &:hover{
            background-color: #aaa;
            cursor: pointer;
        }
    }
</style>