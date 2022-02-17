<template>
    <div class="auth-reg flex_jc_ac fullscreen">
        <form ref="authform"  class="auth-reg_form" @submit.prevent="authorization">
            <AuthInput :value="loginInput" type="text" name="login"/>
            <AuthInput :value="passwordInput" type="password" name="password"/>
            <AuthInput :value="appKey" type="hidden" name="appKey"/>
            <button>{{$t(loginButtonText)}}</button>
        </form>
    </div>
</template>

<script>
    import AuthInput from '../components/AuthInput.vue'
    import { mapActions, mapMutations, mapState } from 'vuex';
    export default {
        name: 'App',
        props: {

        },
        computed: {
            ...mapState("authorization",["login", "password"]),
            ...mapState(["apiUrl", "appKey"]),
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
                loginButtonText: "auth.dologin"
            }
        },
        methods: {
            ...mapMutations("authorization",["setLogin","setPassword"]),
            ...mapActions("authorization", ["doAuthorization"]),
            authorization(){
                this.doAuthorization(this.$refs.authform)
            }
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
    }
</style>