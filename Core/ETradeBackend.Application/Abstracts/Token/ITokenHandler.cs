using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Domain.Entities.Identity;

namespace ETradeBackend.Application.Abstracts.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateAccessToken(AppUser appUser, int tokenExpirationMinute = 30);
        string CreateRefreshToken();
    }
}
