using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace BangBangLobby;

public class LobbyServiceImpl : LobbyService.LobbyServiceBase
{
    private readonly List<string> _players = [];
    private const int MaxPlayers = 6;
    private int _turn;
    private readonly List<string> _playersPicking = [];

    public override Task<JoinLobbyResponse> JoinLobby(JoinLobbyRequest request, ServerCallContext context)
    {
        if (_players.Count >= MaxPlayers)
        {
            return Task.FromResult(new JoinLobbyResponse { Message = "Lobby is full" });
        }

        _players.Add(request.PlayerName);
        return Task.FromResult(new JoinLobbyResponse { Message = "Joined lobby successfully" });
    }

    public override Task<Empty> StartTankSelection(StartTankSelectionRequest request, ServerCallContext context)
    {
        if (_players.Count < MaxPlayers)
        {
            return Task.FromResult(new Empty());
        }

        return (Task<Empty>)Task.CompletedTask;
    }

    public override Task<TankSelectionResponse> SelectTank(TankSelectionRequest request, ServerCallContext context)
    {
        var response = new TankSelectionResponse();
        
        if (_players[_turn] == request.PlayerId)
        {
            response.IsTurn = true;
            response.Message = $"{request.PlayerId} has selected the {request.TankChoice} tank!";
            _playersPicking.Add(request.PlayerId);
            
            _turn = (_turn + 1) % _players.Count;
        }
        else
        {
            response.IsTurn = false;
            response.Message = "It's not your turn!";
        }

        response.PlayersPicking.AddRange(_playersPicking);
        return Task.FromResult(response);
    }
}