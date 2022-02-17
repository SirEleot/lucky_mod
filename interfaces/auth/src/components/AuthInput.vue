<template>
    <div class="auth_input">
        <input :type="type" :value="value" :name="name">
    </div>
</template>

<script>
    export default {
        name: 'App',
        props: {
            type: String,
            value: String,
            name: String
        },
        computed: {

        },
        data() {
            return {

            }
        },
        methods: {

        },
        components: {

        },
    }
</script>

<style lang="scss">
    .auth_input {
        border: 1px solid #000;
        color: #000;
    }
</style>