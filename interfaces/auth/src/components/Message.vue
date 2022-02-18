<template>
    <Transition>
        <div  class="auth-message"  :style="{'border': `1px solid ${color}`}" v-show="message">
            <div class="auth-message_title" :style="{'color': color}">{{message? $t(message.title, message.titleParams): ''}}</div>
            <div class="auth-message_message">{{message? $t(message.message, message.mesageParams) : ''}}</div>
        </div>        
    </Transition >    
</template>

<script>
    import Message from '../classes/Message'
    import {MessageTypes} from '../enums/Message'
    export default {
        name: 'Message',
        props:{
            message: Message
        },
        computed: {
            color(){
                if(!this.message) return '';
                switch (this.message.type) {
                    case MessageTypes.error:                        
                        return 'rgb(212, 43, 31)';    
                    case MessageTypes.success:
                        return 'rgb(17, 170, 37)';                  
                    default:
                        return 'rgb(214, 155, 28)';
                }
            }
        },
        data() {
            return {
                messaVisibleTime: 3000,
                interval: 0,
                bodyStyle: {}
            }
        },
        methods: {
            emitNext(){
                this.$emit("onnext");
            },
        },
        components: {

        },
        watch:{
            message(newMessage){
                if(newMessage)
                    setTimeout(this.emitNext, this.messaVisibleTime)
            }
        }
    }
</script>

<style lang="scss">
    .auth-message {
        padding: 1vh 0;
        &_title{
            font-size: 26px;
        }
        &_message{
            font-size: 16px;
            margin-top: 2vh;
            color: #000;
        }
    }
    .v-enter-active,
    .v-leave-active {
        transition: all 1.5s ease;
    }
    .v-enter-from,
    .v-leave-to {
        opacity: 0;
        transition: all .5s ease;
        transform: translateY(-10vh);
    }
    
</style>