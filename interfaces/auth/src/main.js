import { createApp } from 'vue'
import App from './App.vue'
import store from './store'
import { createI18n } from 'vue-i18n'
import {getI18nOptions} from './localization/localizationLoader'

if(window.mp == undefined){
    window.mp = {
      trigger(...args){
        window.console.log(`Event name: ${args[0]}`)
        let params = "";
        for (let index = 1; index < args.length; index++) {
          params += `${args[index]},`;        
        }
        window.console.log(`Params: ${params}`);
      },
      invoke(...args){
        window.console.log(`Event name: ${args[0]}`)
        let params = "";
        for (let index = 1; index < args.length; index++) {
          params += `${args[index]},`;        
        }
        window.console.log(`Params: ${params}`);
      }
    }
  }
  
  window.mp.triggerServer = (...args)=>{
    window.mp.trigger('efwd', ...args);
  };

const i18n = createI18n(getI18nOptions());
const app = createApp(App);
app.use(i18n);
app.use(store);
app.mount('#app');
