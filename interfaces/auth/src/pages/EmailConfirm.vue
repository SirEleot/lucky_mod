<template>
    <div class="auth-emailc fullscreen flex_jc_ac">
        <form ref="emailcform" class="auth-emailc_form" @submit.prevent="confirmEmail" >            
            <AuthInput :value="codeInput" type="text" name="code"/>
            <AuthInput :value="appKey" type="hidden" name="appKey"/>
            <AuthInput :value="email" type="hidden" name="email"/>
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
            confirmEmail(){                
                this.doEmailConfirm(this.$refs.emailcform)
            }
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