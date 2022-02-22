<template>
    <div class="confirm fullscreen flex_ac_jc">
        <div class="confirm_body">
            <div class="confirm_title">{{$t(title)}}</div>
            <div class="confirm_message">{{$t(message)}}</div>
            <div class="confirm-btns flex_ac_jc">
                <div class="confirm-btns_btn flex_ac_jc" @click="close(true)">{{$t("confirm.yes")}}</div>
                <div class="confirm-btns_btn flex_ac_jc" @click="close(false)">{{$t("confirm.no")}}</div>
            </div>
        </div>
    </div>
</template>

<script>
import { mapActions, mapState } from 'vuex'
    export default {
        name: 'App',
        props: {

        },
        computed: {
            ...mapState("confirmationDialog", ["title", "message"])
        },
        data() {
            return {

            }
        },
        methods: {
            ...mapActions("confirmationDialog", ["close"])
        },
        components: {

        },
    }
</script>

<style lang="scss">
    .confirm {
        background-color: rgba(#000, .5);
        z-index: 500;
        &_body{
            width: 30%;
            height: 25%;
            border: 1px solid #000;
            position: relative;
        }
        &_title{
            margin-top: 15px;
            font-size: 22px;
        }
        &_message{
            margin-top: 30px;
        }
        &-btns{
            position: absolute;
            bottom: 20px;
            width: 100%;
            &_btn{
                width: 150px;
                height: 40px;
                margin: 0 20px;
                border: 1px solid #000;
                &:hover{
                    background-color: rgba(#fff, .3);
                }
            }            
        }
    }
</style>