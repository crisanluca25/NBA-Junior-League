namespace lab10.domain;

public class ActivePlayer : Entity<long>
{
    private long _playerId;
    private long _matchId;
    private long _points;
    private PlayerType _type;

    public ActivePlayer(long playerId, long matchId, long points, PlayerType type)
    {
        _playerId = playerId;
        _matchId = matchId;
        _points = points;
        _type = type;
    }
    
    public long GetPlayerId => _playerId;
    
    public long GetMatchId => _matchId;
    
    public long GetPoints => _points;
    
    public PlayerType GetType => _type;
    
    public void SetPlayerId(long playerId) => _playerId = playerId;
    
    public void SetMatchId(long matchId) => _matchId = matchId;
    
    public void SetPoints(long points) => _points = points;
    
    public void SetType(PlayerType type) => _type = type;
}