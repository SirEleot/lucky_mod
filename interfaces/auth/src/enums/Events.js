class ClienCefEventsEnum {
    constructor() {
        this.restToken = "cef:auth:token:reset";
        this.updateToken = "cef:auth:token:update";
        this.selectCharacter = "cef:auth:character:select";
        this.createCharacter = "cef:auth:character:create";
    }
}

export const ClienCefEvents = new ClienCefEventsEnum();

class ServerProcsEnum{
    constructor() {
        this.checkToken = "auth:player:token:check";
    }
}

export const ServerProcs = new ServerProcsEnum();