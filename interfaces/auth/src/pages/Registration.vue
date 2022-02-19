<template>
    <div class="auth-reg flex_jc_ac fullscreen">
        <form ref="regform"  class="auth-reg_form" @submit.prevent="doRegistration">
            <AuthInput v-model="loginInput" type="text"/>
            <AuthInput v-model="emailInput" type="email"/>
            <AuthInput v-model="passwordInput" type="password"/>
            <AuthInput v-model="confirmPasswordInput" type="password"/>
            <AuthInput v-model="appKey" type="hidden" name="appKey"/>
            <button>{{$t(regButtonText)}}</button>
            <div class="auth-reg_toauth" @click="toAuthorization">{{$t(toAuthButtonText)}}</div>
        </form>
    </div>
</template>

<script>
    import AuthInput from '../components/AuthInput.vue'
    import { mapActions, mapMutations, mapState } from 'vuex';
    export default {
        name: 'Registration',
        props: {

        },
        computed: {
            ...mapState("registration",["login", "password", "email", "confirmPassword"]),
            ...mapState(["appKey"]),
            loginInput:{
                get(){
                    return this.login;
                },
                set(value){
                    this.setLogin(value);
                }
            },
            emailInput:{
                get(){
                    return this.email;
                },
                set(value){
                    this.setEmail(value);
                }
            },
            passwordInput:{
                get(){
                    return this.password;
                },
                set(value){
                    this.setPassword(value);
                }
            },
            confirmPasswordInput:{
                get(){
                    return this.confirmPassword;
                },
                set(value){
                    this.setConfirmPassword(value);
                }
            }
        },
        data() {
            return {
                regButtonText: "auth.doreg",
                toAuthButtonText: "auth.toauth"
            }
        },
        methods: {
            ...mapMutations("registration",["setLogin", "setPassword", "setEmail", "setConfirmPassword"]),
            ...mapActions("registration", ["doRegistration", "toAuthorization"])           
        },
        components: {
            AuthInput
        }
    }
</script>

<style lang="scss">
    .auth-reg {
        
        &_form{

        }
        &_toauth{
            color: #000;
        }
    }
</style>