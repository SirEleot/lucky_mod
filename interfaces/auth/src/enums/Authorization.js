class AuthStatusesEnum{
    constructor() {
        this.ok = "ok";
        this.badPassword= "badPassword";
        this.userNotFound = "userNotFound";
        this.emailNotConfirmed = "emailNotConfirmed";
        this.badApplication = "badApplication";
        this.passwordConfirmError = "passwordConfirmError";
        this.userAlredyExists = "userAlredyExists";
        this.emailAlreadyExists = "emailAlreadyExists";
        this.badEmailCode = "badEmailCode";
        this.badEntryData = "badEntryData";
        this.accessDined = "accessDined";
    }
}

export const AuthStatuses = new AuthStatusesEnum();