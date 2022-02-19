<template>
    <div class="selectlang" :style="{width, height}">
        <div class="selectlang_lang" v-for="(lang, index) in $i18n.availableLocales" :key="index" :class="{'selectlang_selected': isSelected(lang)}" @click="selectLang(lang)">
            <img :src="`/images/langs/${lang}.png`" :alt="lang">
        </div>
    </div>
</template>

<script>
    import {Locales} from '../enums/I18n'
    export default {
        name: 'App',
        props: {
           width: {
               type: String,
               default: "100%"
           },
           height:{
               type: String,
               default: "25px"
           }
        },
        computed: {
            
        },
        data() {
            return {
                locales: Locales
            }
        },
        methods: {
            selectLang(lang){
                this.$i18n.locale = lang;
                this.$emit("onselectlang", lang);
            },
            isSelected(lang){
                return lang === this.$i18n.locale;
            }
        },
        components: {

        },
        mounted(){
            console.log(this.$i18n.locale)
        }
    }
</script>

<style lang="scss">
    .selectlang {
        display: flex;
        align-items: center;
        position: relative;
        margin: 5px;
        &_lang{
            height: 100%;
            min-height: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
            position: relative;
            border: 1px solid #000;
            border-radius: 3px;
            margin: 0 5px;
            img{
                height: 80%;
                margin: 0 15px;
            }
        }
        &_selected{
            border-color: #0F0;
            transform: scale(1.04);
        }
    }
</style>