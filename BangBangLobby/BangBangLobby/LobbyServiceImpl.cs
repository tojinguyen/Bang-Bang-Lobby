using Grpc.Core;
using LobbyService;

public class LobbyServiceImpl : Lobby.LobbyBase
{
    private List<string> playersInLobby = new List<string>();

    public override Task<MatchResponse> FindMatch(MatchRequest request, ServerCallContext context)
    {
        var response = new MatchResponse
        {
            Success = playersInLobby.Count >= 2,
            Message = playersInLobby.Count >= 2 ? "Match found" : "Waiting for players"
        };
        if (playersInLobby.Count < 2) playersInLobby.Add(request.PlayerName);
        return Task.FromResult(response);
    }

    public override Task<TankResponse> SelectTank(TankRequest request, ServerCallContext context)
    {
        return Task.FromResult(new TankResponse
        {
            Success = true,
            Message = $"{request.PlayerName} selected {request.TankName}"
        });
    }
}