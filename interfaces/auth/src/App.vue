<template>
  <div>
    <component :is="page" />
    <Message :message="message"  @onnext="nextMessage" class="auth-massage" v-show="message" />
    <SelectLang class="auth-selectlang" :width="'200px'" :height="'30px'"/>
    <ConfirmationDialog v-if="visible"/>
  </div>
    
</template>

<script>
/**
 * pages
 */
import Authorization from './pages/Authorization.vue'
import Loading from './pages/Loading.vue'
import Registration from './pages/Registration.vue'
import BadSocialClubId from './pages/BadSocialClubId.vue'
import EmailConfirm from './pages/EmailConfirm.vue'
import CharacterSelect from './pages/CharacterSelect.vue'

/**
 * components
 */
import Message from './components/Message.vue'
import SelectLang from './components/SelectLang.vue'
import ConfirmationDialog from './components/ConfirmationDialog.vue'

/**
 * other required
 */
import { mapActions, mapGetters, mapMutations, mapState } from 'vuex';
import {MessageTypes} from './enums/Message'
import MessageClass from './classes/Message'

export default {
  name: 'App',
  computed:{
    ...mapState(["page"]),   
    ...mapGetters(["message"]),
    ...mapState("confirmationDialog",["visible"])
  },
  data() {
    return {
      testMessageIndex: 0,
    }
  }, 
  methods: {
    ...mapActions(["setToken","setAppKey"]),
    ...mapMutations(["showMessage", "nextMessage"]),
    dispatch(action, data){
      this.$store.dispatch(action, data)
    },    
    addTestMessage(){
      this.showMessage(new MessageClass("msg.testmsgmsg", MessageTypes.success, {index: this.testMessageIndex}, {index: this.testMessageIndex}));
      this.testMessageIndex++;
    }
  },  
  components: {
    Authorization,
    Loading,
    BadSocialClubId,
    Registration,
    EmailConfirm,
    Message,
    SelectLang,
    CharacterSelect,
    ConfirmationDialog
  },
  mounted(){
    window.dispatch = this.dispatch;
    if(process.env.NODE_ENV == 'development'){
      this.setAppKey("test")
      this.setToken("eyJBY2Nlc3NUb2tlbiI6eyJJZCI6MCwiTG9naW4iOiJzaXJlbGVvdCIsIkVtYWlsIjoic2lyZWxlb3RAZ21haWwuY29tIiwiQXBwS2V5IjoidGVzdCIsIkV4cGlyaWVkIjoiMjAyMi0wMi0xNlQwNzoxMjo1NS4yMzQ5MjY1WiIsIlNpZ24iOiJhOTUxNDM1ODMwYmFhZTgxYjYyOWIzMTBjMjE1ODE1MGEyZjRiY2RkNzkwYTQ0ZWJjYjc2OTAzNTc4YzU4YjQxIn0sIlJlZnJlc2hUb2tlbiI6eyJJZCI6NCwiTG9naW4iOiJzaXJlbGVvdCIsIkVtYWlsIjoic2lyZWxlb3RAZ21haWwuY29tIiwiQXBwS2V5IjoidGVzdCIsIkV4cGlyaWVkIjoiMjAyMi0wMi0xNlQwNzoxMjo1NS4yMzUwMzAzWiIsIlNpZ24iOiI1MzgyZjhiMWNlODE4ZmQyZGUxMTM2MjUxODlkMDM5MzcwYTQ1MzFjOTBhZmFiZDUxZjczOTkwM2I5NDUyNmNlIn19")
      //setInterval(this.addTestMessage, 4000);
    }
  }
}
</script>

<style lang="scss">
*{
  box-sizing: border-box;
  
}
#app {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #fff;
  border: 1px solid #000;
  overflow: hidden;
}

.fullscreen{
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  overflow: hidden;
}

.flex{
  &_jc_ac,&_ac_jc{
    display: flex;
    justify-content: center;
    align-items: center; 
  }
   &_jc{
    display: flex;
    justify-content: center;
  }
   &_ac{
    display: flex;
    justify-content: center;
  }
}
.addtestmessage{
  color: #000;
  width: 150px;
  height: 30px;
  border: 1px solid #000;
  background: grey;
  cursor: pointer;
  z-index: 100;
  position: absolute;
  left: 0;
  top: 0;
}
.auth-massage{
  margin: 2vh auto;
  width: 25vw;
  height: 12vh;
}
.auth-selectlang{
  //border: 1px solid #000;
  position: absolute;
  right: 20px;
  top: 10px;
}
</style>
