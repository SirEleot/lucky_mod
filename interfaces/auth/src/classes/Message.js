export default class Message{
    constructor(title, message, type, titleParams = null, mesageParams = null) {
        this.title = title;
        this.message = message;
        this.type = type;
        this.titleParams = titleParams;
        this.mesageParams = mesageParams;
    }   
}