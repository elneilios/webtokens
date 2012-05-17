using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Two10.Swt
{
    public enum SimpleWebTokenValidationResult
    {
        Valid,
        TokenExpired,
        InvalidSignature,
        IssuerNotTrusted,
        UnexpectedAudience
    }
}
