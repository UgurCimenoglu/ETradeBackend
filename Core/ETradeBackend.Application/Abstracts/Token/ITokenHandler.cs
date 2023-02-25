using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Application.Abstracts.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateAccessToken(int tokenExpirationMinute = 30);
    }
}
