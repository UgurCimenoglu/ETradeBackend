using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUser.VerifyPasswordResetToken
{
    public class VerifyPasswordResetTokenCommandRequest : IRequest<VerifyPasswordResetTokenCommandResponse>
    {
        public string ResetToken { get; set; }
        public string UserId { get; set; }
    }
}
