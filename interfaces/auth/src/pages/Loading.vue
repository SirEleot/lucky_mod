<template>
    <div class="auth-loading flex_jc_ac fullscreen">
        <div class="auth-loading_text">{{login}} {{textOut}}</div>
    </div>
</template>

<script>
    import { mapState } from 'vuex';
    export default {
        name: 'App',
        props: {

        },
        computed: {
            ...mapState(["login"])
        },
        data() {
            return {
                textIn: "",
                textOut: "",
                cursorPosition: 0,
                dotCount: 5,
                interval: undefined,
                delay: 150
            }
        },
        methods: {
            tick(){
                if(this.cursorPosition < this.textIn.length)
                    this.cursorPosition++;
                else
                    this.cursorPosition = this.textIn.length - this.dotCount;
                this.textOut = this.textIn.substring(0,this.cursorPosition);
            }
        },
        components: {

        },
        mounted() {
            this.textIn = this.$t("auth.loading") + ".".repeat(this.dotCount)
            this.interval = setInterval(this.tick, this.delay)
        },
        beforeUnmount() {
            clearInterval(this.interval)
        }
    }
</script>

<style lang="scss">
    .auth-loading {       
        background-color: #000;       
        &_text{
            text-align-last: left;
            color: #fff;
            width: 300px;
        }
    }
</style>