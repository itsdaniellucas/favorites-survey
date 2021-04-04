using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.BLL.Misc
{
    public enum ErrorTypes
    {
        APIFailure,
        LoginFailure,
        ConcurrentOperation,
        UserRoleInvalid,
        RecordExists,
        RecordNotExists,
        RecordInvalid,
        FieldOutOfRange,
        FieldTooLong,
        FieldLessThanLimit,
        FieldGreaterThanLimit,
        FieldRequired
    }
}
