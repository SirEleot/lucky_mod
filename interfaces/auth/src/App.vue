<template>
  <component :is="page" />
</template>

<script>
import Authorization from './pages/Authorization.vue'
import Loading from './pages/Loading.vue'
import Registration from './pages/Registration.vue'
import BadSocialClubId from './pages/BadSocialClubId.vue'
import EmailConfirm from './pages/EmailConfirm.vue'
import { mapActions, mapState } from 'vuex';

export default {
  name: 'App',
  computed:{
    ...mapState(["page"])
  },
  data() {
    return {
      
    }
  },
  methods: {
    ...mapActions(["setToken","setAppKey"]),
    dispatch(action, data){
      this.$store.dispatch(action, data)
    }
  },  
  components: {
    Authorization,
    Loading,
    BadSocialClubId,
    Registration,
    EmailConfirm
  },
  mounted(){
    window.dispatch = this.dispatch;
    if(process.env.NODE_ENV == 'development'){
      this.setAppKey("test")
      this.setToken("eyJBY2Nlc3NUb2tlbiI6eyJJZCI6MCwiTG9naW4iOiJzaXJlbGVvdCIsIkVtYWlsIjoic2lyZWxlb3RAZ21haWwuY29tIiwiQXBwS2V5IjoidGVzdCIsIkV4cGlyaWVkIjoiMjAyMi0wMi0xNlQwNzoxMjo1NS4yMzQ5MjY1WiIsIlNpZ24iOiJhOTUxNDM1ODMwYmFhZTgxYjYyOWIzMTBjMjE1ODE1MGEyZjRiY2RkNzkwYTQ0ZWJjYjc2OTAzNTc4YzU4YjQxIn0sIlJlZnJlc2hUb2tlbiI6eyJJZCI6NCwiTG9naW4iOiJzaXJlbGVvdCIsIkVtYWlsIjoic2lyZWxlb3RAZ21haWwuY29tIiwiQXBwS2V5IjoidGVzdCIsIkV4cGlyaWVkIjoiMjAyMi0wMi0xNlQwNzoxMjo1NS4yMzUwMzAzWiIsIlNpZ24iOiI1MzgyZjhiMWNlODE4ZmQyZGUxMTM2MjUxODlkMDM5MzcwYTQ1MzFjOTBhZmFiZDUxZjczOTkwM2I5NDUyNmNlIn19")
    }
  }
}
</script>

<style lang="scss">
#app {
  position: absolute;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #fff;
}

.fullscreen{
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
}

.flex{
  &_jc_ac{
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
</style>
