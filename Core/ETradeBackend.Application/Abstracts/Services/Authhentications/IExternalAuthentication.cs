using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Application.Abstracts.Services.Authhentications
{
    public interface IExternalAuthentication
    {
        Task<DTOs.Token> FacebookLoginAsync(string authToken);
        Task<DTOs.Token> GoogleLoginAsync(string idToken);
        Task<DTOs.Token> GoogleLoginV2Async(string code);
    }
}
