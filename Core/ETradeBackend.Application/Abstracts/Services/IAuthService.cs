using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services.Authhentications;

namespace ETradeBackend.Application.Abstracts.Services
{
    public interface IAuthService : IInternalAuthentication, IExternalAuthentication
    {

    }
}
