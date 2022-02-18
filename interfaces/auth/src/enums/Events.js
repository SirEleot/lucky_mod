class ClienCefEventsEnum {
    constructor() {
        this.restToken = "cef:auth:token:reset";
        this.updateToken = "cef:auth:token:update";
    }
}

export const ClienCefEvents = new ClienCefEventsEnum();

class ServerProcsEnum{
    constructor() {
        this.checkToken = "auth:player:token:check";
    }
}

export const ServerProcs = new ServerProcsEnum();