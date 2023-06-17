namespace ETradeBackend.Application.Features.Queries.AppUser.GetRolesToUser;

public class GetRolesToUserQueryResponse
{
    public GetRolesToUserQueryResponse()
    {
        Roles = new();
    }
    public List<string> Roles { get; set; }
}