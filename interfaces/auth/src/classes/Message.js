import {MessageTypes} from '../enums/Message'

export default class Message{
    constructor(message, type, titleParams = null, mesageParams = null) {
        switch (type) {
            case MessageTypes.success:
                this.title = "msg.sueccess"
                break;
            case MessageTypes.error:
                this.title = "msg.error"                
                break;        
            default:
                this.title = "msg.warning"  
                break;
        }
        this.message = message;
        this.type = type;
        this.titleParams = titleParams;
        this.mesageParams = mesageParams;
    }   
}