using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Enums
{
    public enum StatusCodes
    {
        ok,
        badPassword,
        userNotFound,
        emailNotConfirmed,
        badApplication,
        passwordConfirmError,
        userAlredyExists,
        emailAlreadyExists,
        badEmailCode,
        badEntryData,
        accessDined
    }
}
