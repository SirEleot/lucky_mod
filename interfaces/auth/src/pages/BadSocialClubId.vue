<template>
    <div class="auth-badsocial fullscreen flex_jc_ac">
        <div class="auth-badsocial_body">
            {{$t(text)}}
        </div>
    </div>
</template>

<script>
    export default {
        name: 'App',
        props: {

        },
        computed: {

        },
        data() {
            return {
                text: "auth.badsocial"
            }
        },
        methods: {

        },
        components: {

        },
    }
</script>

<style lang="scss">
    .auth-badsocial {
        background-color: #000;
        &_body{

        }
    }
</style>