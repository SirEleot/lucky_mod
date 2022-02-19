import auth from './modules/auth'
import msg from './modules/msg'
import {Locales} from '../enums/I18n'

const localization = {
    auth,
    msg
}


export function getI18nOptions(){
    const options = {};
    options.locale = Locales.ru;
    options.fallbackLocale = Locales.en;
    options.messages = {};    
    const keys = Object.values(Locales)
    keys.forEach(key => {
        options.messages[key] = loadLocal(key);
    });
    
    return options;
}

function loadLocal(local){
    const result= {};
    for (const moduleKey in localization) {        
        const module = localization[moduleKey];
        result[moduleKey] = {};
        for (const messageKey in module) {
            const message = module[messageKey];
            if(message[local])
                result[moduleKey][messageKey] = message[local];
            else 
                result[moduleKey][messageKey] = message[0];
        }        
    }
    return result;
}