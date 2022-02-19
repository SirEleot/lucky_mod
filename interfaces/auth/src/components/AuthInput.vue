<template>
    <div class="auth_input">
        <input :type="type" :name="name" @input="updateValue">
    </div>
</template>

<script>
    export default {
        name: 'AuthInput',
        props: {
            type: String,
            name: String,
            modelValue: String
        },       
        emits: ['update:modelValue'],
        computed: {

        },
        data() {
            return {
            }
        },
        methods: {
            updateValue(e){
                this.$emit('update:modelValue', e.target.value)
            }
        },
        components: {

        },
    }
</script>

<style lang="scss">
    .auth_input {
        input{
            border: 1px solid #000;
            color: #000;
        }        
    }
</style>