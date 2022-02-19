<template>
    <div class="auth-emailc fullscreen flex_jc_ac">
        <form ref="emailcform" class="auth-emailc_form" @submit.prevent="doEmailConfirm" >            
            <AuthInput v-model="codeInput" type="text"/>
            <AuthInput v-model="appKey" type="hidden" />
            <AuthInput v-model="email" type="hidden" />
            <button>{{$t(emailcButtoText)}}</button>
        </form>
    </div>
</template>

<script>
import AuthInput from '../components/AuthInput.vue'
import { mapActions, mapMutations, mapState } from 'vuex'
    export default {
        name: 'EmailConfirm',
        props: {
           
        },
        computed: {
            ...mapState("emailconfirm",["code"]),
            ...mapState(["appKey", "email"]),
            codeInput:{
                get(){
                    return this.code;
                },
                set(value){
                    this.setCode(value);
                }
            },
        },
        data() {
            return {
                emailcButtoText: "auth.emailcbutton"
            }
        },
        methods: {
            ...mapActions("emailconfirm", ["doEmailConfirm"]),
            ...mapMutations("emailconfirm", ["setCode"]),
        },
        components: {
            AuthInput
        },
    }
</script>

<style lang="scss">
    .auth-emailc {

    }
</style>