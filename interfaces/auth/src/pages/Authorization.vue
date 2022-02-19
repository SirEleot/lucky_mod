<template>
    <div class="auth-auth flex_jc_ac fullscreen">
        <form class="auth-auth_form" @submit.prevent="doAuthorization">
            <AuthInput v-model="loginInput" type="text" name="login"/>
            <AuthInput v-model="passwordInput" type="password" name="password"/>
            <AuthInput v-model="appKey" type="hidden" name="appKey"/>
            <button>{{$t(loginButtonText)}}</button>
            <div class="auth-auth_toreg" @click="toRegistration">{{$t(toRegButtonText)}}</div>
        </form>
    </div>
</template>

<script>
    import AuthInput from '../components/AuthInput.vue'
    import { mapActions, mapMutations, mapState } from 'vuex';
    export default {
        name: 'Authorization',
        props: {

        },
        computed: {
            ...mapState("authorization",["login", "password"]),
            ...mapState(["appKey"]),
            loginInput:{
                get(){
                    return this.login;
                },
                set(value){
                    this.setLogin(value);
                }
            },
            passwordInput:{
                get(){
                    return this.password;
                },
                set(value){
                    this.setPassword(value);
                }
            }
        },
        data() {
            return {
                loginButtonText: "auth.dologin",
                toRegButtonText: "auth.toreg"
            }
        },
        methods: {
            ...mapMutations("authorization",["setLogin","setPassword"]),
            ...mapActions("authorization", ["doAuthorization", "toRegistration"])            
        },
        components: {
            AuthInput
        }
    }
</script>

<style lang="scss">
    .auth-auth {
        
        &_form{
            height: 30%;
        }
        &_toreg{
            color: #000;
        }
    }
</style>